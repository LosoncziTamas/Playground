using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Prototype02.Zombie
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ZombieController : MonoBehaviour, IActivatable
    {
        public static readonly List<ZombieController> EnabledInstances = new List<ZombieController>();
        
        public FacingDirection ZombieFacingDirection { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public Animator Animator { get; private set; }
        public Collider2D ZombieAttackCollider => _zombieAttackCollider;
        public Collider2D ZombieIdleCollider => _zombieIdleCollider;
        public ZombieStateMachine ZombieStateMachine { get; private set; }
        public ZombieSpawnState ZombieSpawnState { get; private set; }
        public ZombieMoveState ZombieMoveState { get; private set; }
        public ZombieAttackState ZombieAttackState { get; private set; }
        public ZombieHurtState ZombieHurtState { get; private set; }
        public ZombieDeathState ZombieDeathState { get; private set; }
        public SpriteRenderer SpriteRenderer { private set; get; }

        public bool Activated => Animator.enabled && enabled;
        
        public int HitPoints { get; set; }

        [SerializeField] private ZombieData _zombieData;
        [SerializeField] private Collider2D _zombieAttackCollider;
        [SerializeField] private Collider2D _zombieIdleCollider;
        
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            
            ZombieStateMachine = new ZombieStateMachine();
            ZombieSpawnState = new ZombieSpawnState(this, _zombieData, ZombieStateMachine);
            ZombieMoveState = new ZombieMoveState(this, _zombieData, ZombieStateMachine);
            ZombieAttackState = new ZombieAttackState(this, _zombieData, ZombieStateMachine);
            ZombieHurtState = new ZombieHurtState(this, _zombieData, ZombieStateMachine);
            ZombieDeathState = new ZombieDeathState(this, _zombieData, ZombieStateMachine);
        }

        private void Start() 
        {
            var offset = HeroController.Instance.transform.position - transform.position;
            if (offset.x < 0)
            {
                SpriteRenderer.flipX = false;
                ZombieFacingDirection = FacingDirection.Left;
            }
            else
            {
                SpriteRenderer.flipX = true;
                ZombieFacingDirection = FacingDirection.Right;
            }
            
            ZombieStateMachine.Initialize(ZombieSpawnState);
            Deactivate();
        }

        private void OnEnable()
        {
            EnabledInstances.Add(this);
        }

        private void OnDisable()
        {
            EnabledInstances.Remove(this);
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
                    SpriteRenderer.flipX = false;
                }
                ZombieFacingDirection = FacingDirection.Left;
            }
            else if (newHorizontalDirection > 0)
            {
                if (ZombieFacingDirection == FacingDirection.Left)
                {
                    SpriteRenderer.flipX = true;
                }
                ZombieFacingDirection = FacingDirection.Right;
            }
        }
        
        [UsedImplicitly]
        public void OnAnimationEvent(AnimEvent animEvent)
        {
            ZombieStateMachine.CurrentState.OnAnimEvent(animEvent);
        }

        public void Activate()
        {
            Debug.Log($"{gameObject.name} Activated");
            enabled = true;
            Animator.enabled = true;
        }

        public void Deactivate()
        {
            Debug.Log($"{gameObject.name} Deactivated");
            enabled = false;
            Animator.enabled = false;
        }

        private void OnDrawGizmos()
        {
            Handles.Label(transform.position + Vector3.right * 0.01f, Activated ? "Activated " : "Deactivated");
        }
    }
}