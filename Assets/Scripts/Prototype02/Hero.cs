using System;
using System.Collections;
using UnityEngine;

namespace Prototype02
{
    public class Hero : MonoBehaviour
    {
        private const string AttackAnimStateName = "Attack1";
        
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int AirSpeedY = Animator.StringToHash("AirSpeedY");
        private static readonly int AnimState = Animator.StringToHash("AnimState");
        private static readonly int Attack1 = Animator.StringToHash("Attack1");

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private CollisionSensor _groundCollisionSensor;
        [SerializeField] private GameProps _gameProps;

        private bool _grounded;
        private int _facingDirection;

        private void Awake()
        {
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
        }

        private void Start()
        {
            _animator.SetFloat(AirSpeedY, -1.0f);
        }

        private bool _attacking;

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

        private void Update()
        {
            if (_attacking)
            {
                return;
            }
            var move = false;
            var horizontal = Input.GetAxis("Horizontal");
            if (horizontal < 0)
            {
                if (_facingDirection > 0)
                {
                    _spriteRenderer.flipX = true;
                }
                _facingDirection = -1;
                move = true;
            }
            else if (horizontal > 0)
            {
                if (_facingDirection < 0)
                {
                    _spriteRenderer.flipX = false;
                }
                _facingDirection = 1;
                move = true;
            }

            if (move)
            {
                _animator.SetInteger(AnimState, 1);
                _rigidbody2D.MovePosition(_rigidbody2D.position + horizontal * Vector2.right * _gameProps.playerSpeed);
            }
            else
            {
                _animator.SetInteger(AnimState, 0);
            }

            var attack = Input.GetButton("Fire1");

            if (attack)
            {
                StartCoroutine(Attack());
            }
        }
    }
}