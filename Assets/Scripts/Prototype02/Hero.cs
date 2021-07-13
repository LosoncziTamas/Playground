using Prototype02.States;
using UnityEngine;

namespace Prototype02
{
    public class Hero : StateMachine
    {
        public static Hero Instance;
        
        private const string AttackAnimStateName = "Attack1";
        private const string JumpAnimStateName = "Jump";
        
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int AnimState = Animator.StringToHash("AnimState");
        
        private static readonly int Attack1 = Animator.StringToHash(AttackAnimStateName);
        private static readonly int JumpAnim = Animator.StringToHash(JumpAnimStateName);

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private CollisionSensor _groundCollisionSensor;
        [SerializeField] private CollisionSensor _rightCollisionSensor;
        [SerializeField] private GameProps _gameProps;

        private bool _grounded;
        private bool _attacking;
        private bool _beingHurt;
        private bool _jumping;
        private bool _moving;
        private bool _idle;

        public bool IsGrounded => _grounded;

        public Animator Animator => _animator;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public CollisionSensor GroundCollisionSensor => _groundCollisionSensor;

        public bool Moving => _moving;
        public bool Idle => _idle;

        private void Awake()
        {
            Instance = this;
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            SetState(new FallingState());
        }

        private void FixedUpdate()
        {
            if (!_grounded && _groundCollisionSensor.Colliding)
            {
                _grounded = true;
                //SetState(new GroundedState());
            }
            else if (_grounded && !_groundCollisionSensor.Colliding)
            {
                _grounded = false;
                // SetState(new FallingState());
            }

            if (_rightCollisionSensor.CollidingWithEnemy)
            {
                // StartCoroutine(State.GetHurt(_rigidbody2D.position - Vector2.right * (_facingDirection * _gameProps.playerSpeed)));
            }
        }

        private void Update()
        {
            var _attacking = Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space);
            var _jumping = Input.GetKey(KeyCode.UpArrow);
            var horizontal = Input.GetAxis("Horizontal");
            _moving = horizontal < 0 || horizontal > 0;

            _idle = !_attacking && !_jumping && !_moving;
        }
    }
}