using UnityEngine;

namespace Prototype02.New
{
    public class HeroMoveState : HeroState
    {
        public HeroMoveState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            heroController.Animator.SetInteger(AnimStates.AnimStateId, 1);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            var horizontal = Input.GetAxis("Horizontal");
            heroController.FlipSpriteOnDirectionChange(horizontal);
            if (Mathf.Approximately(0f, horizontal) && !heroController.Jumping)
            {
                heroController.HeroStateMachine.ChangeState(heroController.HeroIdleState);
            }

            if (heroController.Jumping)
            {
                heroController.HeroStateMachine.ChangeState(heroController.HeroJumpState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            var horizontal = Input.GetAxis("Horizontal");
            heroController.Rigidbody2D.velocity = new Vector2(horizontal * heroData.horizontalMovementSpeed, heroController.Rigidbody2D.velocity.y);
        }
    }
}