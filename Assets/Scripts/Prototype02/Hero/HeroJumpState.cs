using Prototype02.New;
using UnityEngine;

namespace Prototype02.Hero
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
            if (!(heroController.Rigidbody2D.velocity.y > 0))
            {
                heroController.Rigidbody2D.velocity = new Vector2(heroController.Rigidbody2D.velocity.x, heroData.jumpVelocityY);
            }
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            var horizontal = 0; // Input.GetAxis("Horizontal")
            heroController.FlipSpriteOnDirectionChange(horizontal);
            
            if (heroController.Rigidbody2D.velocity.y < 0.0f)
            {
                heroStateMachine.ChangeState(heroController.HeroFallingState);
            }
            if (heroController.CanAttack() && heroController.Attacking)
            {
                heroStateMachine.ChangeState(heroController.HeroAttackState);
            }
            if (heroController.BeingHurt)
            {
                heroStateMachine.ChangeState(heroController.HeroHurtState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            var horizontal = 0;//Input.GetAxis("Horizontal");
            heroController.Rigidbody2D.velocity = new Vector2(horizontal * heroData.horizontalMovementSpeed, heroController.Rigidbody2D.velocity.y);
        }
    }
}