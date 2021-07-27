using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieAttackState : ZombieState
    {
        public ZombieAttackState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            zombieController.Animator.SetTrigger(AnimStates.ZombieAttackAnimId);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (startTime + zombieData.zombieAttackStart <= Time.time && startTime + zombieData.zombieAttackEnd >= Time.time)
            {
                zombieController.ZombieAttackCollider.enabled = true;
                zombieController.ZombieIdleCollider.enabled = false;
            }
            else
            {
                zombieController.ZombieAttackCollider.enabled = false;
                zombieController.ZombieIdleCollider.enabled = true;
            }

            if (startTime + zombieData.zombieAttackDurationInSeconds <= Time.time)
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