using UnityEngine;

namespace Prototype02.New
{
    public class HeroJumpState : HeroState
    {
        public HeroJumpState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            heroController.Animator.SetTrigger(AnimStates.JumpStateId);
            heroController.Animator.SetBool(AnimStates.GroundedAnimId, false);
            heroController.Rigidbody2D.velocity = new Vector2(heroController.Rigidbody2D.velocity.x, heroData.jumpVelocityY);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            heroController.FlipSpriteOnDirectionChange(Input.GetAxis("Horizontal"));
            
            if (heroController.Rigidbody2D.velocity.y < 0.0f)
            {
                heroStateMachine.ChangeState(heroController.HeroFallingState);
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