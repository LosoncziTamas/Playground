using System;
using UnityEngine;

namespace Prototype02.Zombie
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ZombieController : MonoBehaviour
    {
        public FacingDirection ZombieFacingDirection { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public Animator Animator { get; private set; }
        public Collider2D ZombieAttackCollider => _zombieAttackCollider;
        public Collider2D ZombieIdleCollider { get; private set; }
        public ZombieStateMachine ZombieStateMachine { get; private set; }
        public ZombieSpawnState ZombieSpawnState { get; private set; }
        public ZombieMoveState ZombieMoveState { get; private set; }
        public ZombieAttackState ZombieAttackState { get; private set; }
        public ZombieHurtState ZombieHurtState { get; private set; }
        public ZombieDeathState ZombieDeathState { get; private set; }

        [SerializeField] private ZombieData _zombieData;
        [SerializeField] private Collider2D _zombieAttackCollider;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            ZombieIdleCollider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            ZombieStateMachine = new ZombieStateMachine();
            ZombieSpawnState = new ZombieSpawnState(this, _zombieData, ZombieStateMachine);
            ZombieMoveState = new ZombieMoveState(this, _zombieData, ZombieStateMachine);
            ZombieAttackState = new ZombieAttackState(this, _zombieData, ZombieStateMachine);
            ZombieHurtState = new ZombieHurtState(this, _zombieData, ZombieStateMachine);
            ZombieDeathState = new ZombieDeathState(this, _zombieData, ZombieStateMachine);
        }

        private void Start()
        {
            ZombieStateMachine.Initialize(ZombieSpawnState);
        }

        private void Update()
        {
            ZombieStateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            ZombieStateMachine.CurrentState.PhysicsUpdate();
        }
        
        public void FlipSpriteOnDirectionChange(float newHorizontalDirection)
        {
            if (newHorizontalDirection < 0)
            {
                if (ZombieFacingDirection == FacingDirection.Right)
                {
                    _spriteRenderer.flipX = false;
                }
                ZombieFacingDirection = FacingDirection.Left;
            }
            else if (newHorizontalDirection > 0)
            {
                if (ZombieFacingDirection == FacingDirection.Left)
                {
                    _spriteRenderer.flipX = true;
                }
                ZombieFacingDirection = FacingDirection.Right;
            }
        }

        public void OnHit()
        {
            ZombieStateMachine.ChangeState(ZombieHurtState);
        }
    }
}