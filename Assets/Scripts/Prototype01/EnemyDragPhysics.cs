using System.Collections;
using UnityEngine;

namespace Prototype01
{
    public class EnemyDragPhysics : MonoBehaviour
    {
        private static bool _anyEnemyBeingSelected = false;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private HitPointUi _hitPointUi;

        private Color _defaultColor;
        private Camera _camera;

        public float dropForceMultiplier = 0.2f;
        public float dragForceMultiplier = 0.2f;
        public float dragThreshold = 0.2f;
        public float linearDragWhenSelected = 3.5f;

        private bool _selected;
        private int _hitPoints = 3;
        
        private bool _takingDamage;
        private bool _recovering;

        private bool _beingThrowned;

        private void Awake()
        {
            _defaultColor = _spriteRenderer.color;
            _camera = Camera.main;
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
                yield return new WaitForSeconds(1);
                if (CanTakeDamage())
                {
                    --_hitPoints;
                    _hitPointUi.SetPointsLeft(_hitPoints);
                    if (_hitPoints == 0)
                    {
                        Destroy(gameObject);
                        _anyEnemyBeingSelected = false;
                    }
                }
                else
                {
                    yield break;
                }
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
            while (CanRecover())
            {
                yield return new WaitForSeconds(1);
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

        private void MoveEnemy()
        {
            Vector2 mousePosInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = transform.position;

            if (Vector2.Distance(mousePosInWorld, pos) > dragThreshold)
            {
                var force = (mousePosInWorld - pos).normalized;
                _rigidbody2D.AddRelativeForce(force * dragForceMultiplier, ForceMode2D.Impulse);
                    
            }
            else
            {
                StartCoroutine(TakeDamage());
            }
                
            _rigidbody2D.drag = linearDragWhenSelected;
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
            // TODO: fix when respawned
            if (_anyEnemyBeingSelected)
            {
                StartCoroutine(Recover());
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                TryToPickEnemy();
                _beingThrowned = false;
            }
            else if (_selected && Input.GetMouseButton(0))
            {
                MoveEnemy();
                _beingThrowned = false;
            }
            else if (_selected && Input.GetMouseButtonUp(0))
            {
                ReleaseEnemy();
                _beingThrowned = true;
            }
            else
            {
                StartCoroutine(Recover());
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            const string pitTag = "Pit";
            if (other.gameObject.CompareTag(pitTag) && _beingThrowned)
            {
                _beingThrowned = false;
                Pit.Instance.RespawnInTheFuture(gameObject);
            }
        }
    }
}