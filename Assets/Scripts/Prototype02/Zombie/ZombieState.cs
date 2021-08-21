using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieState
    {
        protected ZombieController zombieController;
        protected ZombieData zombieData;
        protected ZombieStateMachine zombieStateMachine;

        protected float startTime;

        public ZombieState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine)
        {
            this.zombieController = zombieController;
            this.zombieData = zombieData;
            this.zombieStateMachine = zombieStateMachine;
        }

        public virtual void Enter()
        {
            startTime = Time.time;
        }

        public virtual void Exit()
        {
            
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            
        }

        public virtual void OnAnimEvent(AnimEvent animEvent)
        {
            
        }
    }
}