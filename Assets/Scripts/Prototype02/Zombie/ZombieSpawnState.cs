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
            zombieController.Animator.SetTrigger(AnimStates.SpawnAnimId);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time - startTime > zombieData.spawnDurationInSeconds)
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