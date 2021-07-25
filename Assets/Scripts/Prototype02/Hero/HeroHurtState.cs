using UnityEngine;

namespace Prototype02.New
{
    public class HeroHurtState : HeroState
    {
        public HeroHurtState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            heroController.Animator.SetTrigger(AnimStates.HurtAnimId);
            var offset = heroController.LastEnemyCollider.transform.position - heroController.transform.position;
            if (offset.x > 0)
            {
                heroController.Rigidbody2D.velocity = new Vector2(-1.0f * heroData.hurtBackOffX, heroController.Rigidbody2D.velocity.y);
            }
            else if (offset.x < 0)
            {
                heroController.Rigidbody2D.velocity = new Vector2(1.0f * heroData.hurtBackOffX, heroController.Rigidbody2D.velocity.y);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time - startTime > heroData.hurtDurationInSeconds)
            {
                if (heroController.IsGrounded)
                {
                    if (heroController.Moving)
                    {
                        heroStateMachine.ChangeState(heroController.HeroMoveState);
                    }
                    else
                    {
                        heroStateMachine.ChangeState(heroController.HeroIdleState);
                    }
                }
                else if (heroController.Rigidbody2D.velocity.y > 0)
                {
                    heroStateMachine.ChangeState(heroController.HeroJumpState);
                }
                else if (heroController.Rigidbody2D.velocity.y < 0)
                {
                    heroStateMachine.ChangeState(heroController.HeroFallingState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}