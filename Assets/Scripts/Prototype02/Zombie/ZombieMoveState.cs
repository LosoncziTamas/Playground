using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieMoveState : ZombieState
    {
        public ZombieMoveState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            zombieController.Animator.SetInteger(AnimStates.AnimStateId, 1);
        }

        public override void Exit()
        {
            base.Exit();
            zombieController.Animator.SetInteger(AnimStates.AnimStateId, 0);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            var offset = HeroController.Instance.transform.position - zombieController.transform.position;
            zombieController.FlipSpriteOnDirectionChange(offset.x);
            if (HeroController.Instance.IsGrounded && Mathf.Abs(offset.x) < zombieData.zombieAttackDistance)
            {
                zombieStateMachine.ChangeState(zombieController.ZombieAttackState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            var targetPos = HeroController.Instance.transform.position;
            var movePos = Vector2.MoveTowards(zombieController.transform.position, targetPos, Time.deltaTime * zombieData.walkSpeed);
            zombieController.transform.position = movePos;
        }
    }
}