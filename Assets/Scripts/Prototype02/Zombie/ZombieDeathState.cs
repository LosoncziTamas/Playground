using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieDeathState : ZombieState
    {
        public ZombieDeathState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            zombieController.Animator.SetBool(AnimStates.DeathAnimId, true);
            zombieController.Animator.SetInteger(AnimStates.AnimStateId, 0);
            // TODO: disable interaction
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (startTime + zombieData.deathDuration < Time.time)
            {
                zombieController.ZombieStateMachine.ChangeState(zombieController.ZombieSpawnState);
            }
        }
    }
}