using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieHurtState : ZombieState
    {
        public ZombieHurtState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            zombieController.Animator.SetTrigger(AnimStates.HurtAnimId);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (startTime + zombieData.zombieHurtDuration > Time.time)
            {
                zombieStateMachine.ChangeState(zombieController.ZombieMoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}