using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieSpawnState : ZombieState
    {
        public ZombieSpawnState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            zombieController.HitPoints = zombieData.initialHitPoints;
            zombieController.Animator.SetBool(AnimStates.SpawnAnimId, true);
            zombieController.ZombieIdleCollider.enabled = false;
        }

        public override void Exit()
        {
            base.Exit();
            zombieController.Animator.SetBool(AnimStates.SpawnAnimId, false);
            zombieController.ZombieIdleCollider.enabled = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time - startTime > zombieData.spawnDurationInSeconds)
            {
                zombieStateMachine.ChangeState(zombieController.ZombieMoveState);
            }
        }
    }
}