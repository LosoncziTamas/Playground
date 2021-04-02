using System;
using System.Collections;
using UnityEngine;

namespace Prototype01
{
    public class EnemyDragPhysics : MonoBehaviour
    {
        public const int MaxHitPoints = 3;
        
        private static bool _anyEnemyBeingSelected;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private HitPointUi _hitPointUi;

        private Color _defaultColor;
        private Camera _camera;

        public float dropForceMultiplier = 0.2f;
        public float selectionForceMultiplier = 0.2f;
        public float minDragDistance = 0.2f;
        public float maxDragDistance = 2.0f;
        public float linearDragWhenSelected = 3.5f;
        public float recoverAHitPointDurationInSeconds = 1.0f;
        public float takeADamageDurationInSeconds = 1.0f;

        // TODO: create state machine
        private int _hitPoints = MaxHitPoints;
        private bool _selected;
        private bool _takingDamage;
        private bool _recovering;
        private bool _beingThrown;
        
        public int HitPointsLeft => _hitPoints;

        private void Awake()
        {
            _defaultColor = _spriteRenderer.color;
            _camera = Camera.main;
        }

        public void ResetInternals(float scale = 1.0f)
        {
            gameObject.transform.localScale *= scale;
            
            _hitPoints = MaxHitPoints;
            _selected = false;
            _takingDamage = false;
            _recovering = false;
            _beingThrown = false;
            
            _hitPointUi.SetPointsLeft(_hitPoints);
        }
        
        private IEnumerator TakeDamage()
        {
            if (_takingDamage)
            {
                yield break;
            }
            _takingDamage = true;
            _recovering = false;
            while (CanTakeDamage())
            {
                yield return new WaitForSeconds(takeADamageDurationInSeconds);
                if (CanTakeDamage())
                {
                    --_hitPoints;
                    _hitPointUi.SetPointsLeft(_hitPoints);
                    if (_hitPoints == 0)
                    {
                        // Destroy(gameObject);
                        _anyEnemyBeingSelected = false;
                    }
                }
                else
                {
                    yield break;
                }
            }
        }

        private bool _bobbing = false;

        public float bobbingScale = 0.5f;

        private IEnumerator DoBobbing()
        {
            if (_bobbing)
            {
                yield break;
            }

            _bobbing = true;
            var offset = Vector3.zero;
            while (_bobbing)
            {
                offset.y = Mathf.Sin(Time.time) * bobbingScale;
                Debug.Log(offset);
                transform.position += offset;
                yield return new WaitForFixedUpdate();
            }
        }

        private bool CanTakeDamage()
        {
            return _takingDamage && _hitPoints > 0;
        }

        private bool CanRecover()
        {
            return _recovering && _hitPoints < 3;
        }
        
        private IEnumerator Recover()
        {
            if (_recovering)
            {
                yield break;
            }
            _recovering = true;
            _takingDamage = false;
            _bobbing = false;
            while (CanRecover())
            {
                yield return new WaitForSeconds(recoverAHitPointDurationInSeconds);
                if (CanRecover())
                {
                    ++_hitPoints;
                    _hitPointUi.SetPointsLeft(_hitPoints);
                }
                else
                {
                    yield break;
                }
            }
        }

        private void TryToPickEnemy()
        {
            Vector2 mousePosInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (_collider2D.OverlapPoint(mousePosInWorld))
            {
                _spriteRenderer.color = Color.white;
                _selected = true;
                _anyEnemyBeingSelected = true;
            }
        }

        private void OnGUI()
        {
            if (_takingDamage)
            {
                GUILayout.Label("Taking damage");
            }

            if (_beingThrown)
            {
                GUILayout.Label("Being thrown");
            }

            if (_recovering)
            {
                GUILayout.Label("Recovering");
            }
            
            if (_selected)
            {
                GUILayout.Label("Selected");
            }
        }

        private void MoveEnemy()
        {
            Vector2 mousePosInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = transform.position;
            var distance = Vector2.Distance(mousePosInWorld, pos);
            
            if (distance > minDragDistance && distance < maxDragDistance)
            {
                var force = (mousePosInWorld - pos).normalized;
                _rigidbody2D.AddRelativeForce(force * selectionForceMultiplier, ForceMode2D.Impulse);
                _takingDamage = false;
                _bobbing = false;
                _rigidbody2D.drag = linearDragWhenSelected;
            }
            else if (distance < minDragDistance)
            {
                StartCoroutine(DoBobbing());
                _rigidbody2D.drag = linearDragWhenSelected;
            }
            else if (distance > maxDragDistance)
            {
                ReleaseEnemy();
                _beingThrown = true;
            }
                
        }

        private void ReleaseEnemy()
        {
            StartCoroutine(Recover());
            _spriteRenderer.color = _defaultColor;
            _rigidbody2D.drag = 0;
            Vector2 mousePosInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);

            Vector2 pos = transform.position;
            var force = (mousePosInWorld - pos).normalized;
                
            _selected = false;
            _anyEnemyBeingSelected = false;
            _rigidbody2D.AddRelativeForce(force * dropForceMultiplier, ForceMode2D.Impulse);
        }
        
        private void Update()
        {
            if (!_anyEnemyBeingSelected && Input.GetMouseButtonDown(0))
            {
                // TODO: Z-order fix
                TryToPickEnemy();
                _beingThrown = false;
            }
            else if (_selected && Input.GetMouseButton(0))
            {
                MoveEnemy();
                _beingThrown = false;
            }
            else if (_selected && Input.GetMouseButtonUp(0))
            {
                ReleaseEnemy();
                _beingThrown = true;
            }
            else
            {
                StartCoroutine(Recover());
            }
        }

        private void ThrowToPit(Collider2D other)
        {
            const string pitTag = "Pit";
            if (other.gameObject.CompareTag(pitTag) && _beingThrown)
            {
                _beingThrown = false;
                Pit.Instance.RespawnInTheFuture(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ThrowToPit(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            ThrowToPit(other);
        }
    }
}