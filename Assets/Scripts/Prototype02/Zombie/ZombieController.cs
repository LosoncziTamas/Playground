using System;
using UnityEngine;

namespace Prototype02.Zombie
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ZombieController : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D { get; private set; }
        public Animator Animator { get; private set; }
        public ZombieStateMachine ZombieStateMachine { get; private set; }
        public ZombieSpawnState ZombieSpawnState { get; private set; }
        public ZombieMoveState ZombieMoveState { get; private set; }

        [SerializeField] private ZombieData _zombieData;
        
        private HeroController _hero;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            
            ZombieStateMachine = new ZombieStateMachine();
            ZombieSpawnState = new ZombieSpawnState(this, _zombieData, ZombieStateMachine);
            ZombieMoveState = new ZombieMoveState(this, _zombieData, ZombieStateMachine);
        }

        private void Start()
        {
            _hero = HeroController.Instance;
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
    }
}