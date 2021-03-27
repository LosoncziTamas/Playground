using System.Collections;
using UnityEngine;

namespace Prototype01
{
    public class EnemyDragPhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private HitPointUi _hitPointUi;

        private Color _defaultColor;
        private Camera _camera;

        public float dropForceMultiplier;
        public float dragForceMultiplier;
        public float dragThreshold;

        private bool _selected;
        private int _hitPoints = 3;
        
        private bool _takingDamage;
        private bool _recovering;

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

        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
                if (_collider2D.OverlapPoint(mousePosInWorld))
                {
                    _spriteRenderer.color = Color.white;
                    _selected = true;
                }

            }
            else if (_selected && Input.GetMouseButton(0))
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
                
                _rigidbody2D.drag = 1.0f;
            }
            else if (_selected && Input.GetMouseButtonUp(0))
            {
                StartCoroutine(Recover());
                _spriteRenderer.color = _defaultColor;
                _rigidbody2D.drag = 0;
                Vector2 mousePosInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);

                Vector2 pos = transform.position;
                var force = (mousePosInWorld - pos).normalized;
                
                _selected = false;
                _rigidbody2D.AddRelativeForce(force * dropForceMultiplier, ForceMode2D.Impulse);
            }
            else
            {
                StartCoroutine(Recover());
            }
        }
    }
}