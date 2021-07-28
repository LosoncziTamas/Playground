using System.Collections.Generic;
using Prototype02.New;
using UnityEngine;

namespace Prototype02
{
    public class HeroController : MonoBehaviour
    {
        public static HeroController Instance { get; private set; }
        
        [SerializeField] private CollisionSensor _groundCollisionSensor;
        [SerializeField] private EnemyHitBox _attackSensorRight;
        [SerializeField] private EnemyHitBox _attackSensorLeft;
        [SerializeField] private CollisionSensor _hurtSensor;
        [SerializeField] private HeroData _heroData;
        
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public FacingDirection HeroFacingDirection { get; set; } = FacingDirection.Right;
        public HeroStateMachine HeroStateMachine { get; private set; }
        public HeroFallingState HeroFallingState { get; private set; }
        public HeroJumpState HeroJumpState { get; private set; }
        public HeroIdleState HeroIdleState { get; private set; }
        public HeroMoveState HeroMoveState { get; private set; }
        public HeroAttackState HeroAttackState { get; private set; }
        public HeroHurtState HeroHurtState { get; private set; }
        
        public bool Jumping { get; private set; }
        public bool Moving { get; private set; }
        public bool Attacking { get; private set; }
        public bool IsGrounded { get; private set; }
        // TODO: use these for attack
        public bool EnemyWithinRightHitBox => _attackSensorRight.EnemyColliders.Count > 0;
        public bool EnemyWithinLeftHitBox => _attackSensorLeft.EnemyColliders.Count > 0;
        public bool BeingHurt => _hurtSensor.CollidingWithEnemy;

        public Collider2D LastHurtCollider => _hurtSensor.LastEnemyCollider;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            Instance = this;
            
            Animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Rigidbody2D = GetComponent<Rigidbody2D>();

            HeroStateMachine = new HeroStateMachine();
            
            HeroFallingState = new HeroFallingState(this, _heroData, HeroStateMachine);
            HeroIdleState = new HeroIdleState(this, _heroData, HeroStateMachine);
            HeroJumpState = new HeroJumpState(this, _heroData, HeroStateMachine);
            HeroMoveState = new HeroMoveState(this, _heroData, HeroStateMachine);
            HeroAttackState = new HeroAttackState(this, _heroData, HeroStateMachine);
            HeroHurtState = new HeroHurtState(this, _heroData, HeroStateMachine);
        }

        private void Start()
        {
            if (!IsGrounded)
            {
                HeroStateMachine.Initialize(HeroFallingState);
            }
            else
            {
                HeroStateMachine.Initialize(HeroIdleState);
            }
        }

        private void FixedUpdate()
        {
            if (!IsGrounded && _groundCollisionSensor.Colliding)
            {
                IsGrounded = true;
            }
            else if (IsGrounded && !_groundCollisionSensor.Colliding)
            {
                IsGrounded = false;
            }
            
            HeroStateMachine.CurrentState.PhysicsUpdate();
        }

        private void Update()
        {
            HeroStateMachine.CurrentState.LogicUpdate();
            Jumping = Input.GetKey(KeyCode.UpArrow);
            Moving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0f;
            Attacking = Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space);
        }

        public void FlipSpriteOnDirectionChange(float horizontal)
        {
            if (horizontal < 0)
            {
                if (HeroFacingDirection == FacingDirection.Right)
                {
                    _spriteRenderer.flipX = true;
                }
                HeroFacingDirection = FacingDirection.Left;
            }
            else if (horizontal > 0)
            {
                if (HeroFacingDirection == FacingDirection.Left)
                {
                    _spriteRenderer.flipX = false;
                }
                HeroFacingDirection = FacingDirection.Right;
            }
        }

        public List<Collider2D> GetEnemiesFromHitBox(FacingDirection facingDirection)
        {
            return facingDirection == FacingDirection.Right ? _attackSensorRight.EnemyColliders : _attackSensorLeft.EnemyColliders;
        }


        private void OnGUI()
        {
            GUILayout.Label(HeroStateMachine.CurrentState.GetType().ToString());
        }
    }
}