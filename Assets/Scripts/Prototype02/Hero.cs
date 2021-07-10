using System;
using System.Collections;
using UnityEngine;

namespace Prototype02
{
    public class Hero : MonoBehaviour
    {
        public static Hero Instance;
        
        private const string AttackAnimStateName = "Attack1";
        private const string HurtAnimStateName = "Hurt";
        private const string JumpAnimStateName = "Jump";
        
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int AirSpeedY = Animator.StringToHash("AirSpeedY");
        private static readonly int AnimState = Animator.StringToHash("AnimState");
        private static readonly int Attack1 = Animator.StringToHash(AttackAnimStateName);
        private static readonly int Hurt = Animator.StringToHash(HurtAnimStateName);
        private static readonly int JumpAnim = Animator.StringToHash(JumpAnimStateName);

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private CollisionSensor _groundCollisionSensor;
        [SerializeField] private CollisionSensor _rightCollisionSensor;
        [SerializeField] private GameProps _gameProps;

        private bool _grounded;
        private int _facingDirection;
        private bool _attacking;
        private bool _beingHurt;
        private bool _jumping;

        public bool IsGrounded => _grounded;

        private void Awake()
        {
            Instance = this;
            _facingDirection = 1;
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!_grounded && _groundCollisionSensor.Colliding)
            {
                _grounded = true;
                _animator.SetBool(Grounded, _grounded);
            }
            else if (_grounded && !_groundCollisionSensor.Colliding)
            {
                _grounded = false;
                _animator.SetBool(Grounded, _grounded);
            }

            if (_rightCollisionSensor.CollidingWithEnemy)
            {
                StartCoroutine(GetHurt());
            }
        }

        private void Start()
        {
            _animator.SetFloat(AirSpeedY, -1.0f);
        }

        private IEnumerator GetHurt()
        {
            if (_beingHurt)
            {
                yield break;
            }

            _beingHurt = true;
            _animator.SetTrigger(Hurt);
            while (_animator.AnimatorIsPlaying(HurtAnimStateName))
            {
                yield return null;
                _rigidbody2D.MovePosition(_rigidbody2D.position - (Vector2.right * (_facingDirection * _gameProps.playerSpeed)));
            }

            _beingHurt = false;
        }
        
        private IEnumerator Attack()
        {
            if (_attacking)
            {
                yield break;
            }
            _attacking = true;
            _animator.SetTrigger(Attack1);
            while (_animator.AnimatorIsPlaying(AttackAnimStateName))
            {
                yield return null;
            }
            _attacking = false;
        }

        private IEnumerator Jump()
        {
            if (_jumping)
            {
                yield break;
            }
            _jumping = true;
            _rigidbody2D.velocity += Vector2.up * 1.0f;
            _animator.SetTrigger(JumpAnim);
            while (_animator.AnimatorIsPlaying(JumpAnimStateName) || !_grounded)
            {
                yield return null;
            }

            _jumping = false;
        }

        private void Update()
        {
            if (_attacking)
            {
                return;
            }
            var moveHorizontally = false;
            var horizontal = Input.GetAxis("Horizontal");
            if (horizontal < 0)
            {
                if (_facingDirection > 0)
                {
                    _spriteRenderer.flipX = true;
                }
                _facingDirection = -1;
                moveHorizontally = true;
            }
            else if (horizontal > 0)
            {
                if (_facingDirection < 0)
                {
                    _spriteRenderer.flipX = false;
                }
                _facingDirection = 1;
                moveHorizontally = true;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                StartCoroutine(Jump());
            }

            if (moveHorizontally)
            {
                _animator.SetInteger(AnimState, 1);
                _rigidbody2D.MovePosition(_rigidbody2D.position + horizontal * Vector2.right * _gameProps.playerSpeed);
            }
            else
            {
                _animator.SetInteger(AnimState, 0);
            }

            var attack = Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space);

            if (attack)
            {
                StartCoroutine(Attack());
            }
        }
    }
}