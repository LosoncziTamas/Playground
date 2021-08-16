using Prototype02.New;
using UnityEngine;

namespace Prototype02.Hero
{
    public class HeroHurtState : HeroState
    {
        public HeroHurtState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            heroController.HitPoints--;
            if (heroController.HitPoints == 0)
            {
                heroController.HeroStateMachine.ChangeState(heroController.HeroDeathState);
                return;
            }
            heroController.Animator.SetTrigger(AnimStates.HurtAnimId);
            var offset = heroController.LastHurtCollider.transform.position - heroController.transform.position;
            if (offset.x > 0)
            {
                heroController.Rigidbody2D.velocity = new Vector2(-1.0f * heroData.hurtBackOffX, heroController.Rigidbody2D.velocity.y);
            }
            else if (offset.x < 0)
            {
                heroController.Rigidbody2D.velocity = new Vector2(1.0f * heroData.hurtBackOffX, heroController.Rigidbody2D.velocity.y);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            var animating = heroController.Animator.IsAnimationPlaying(AnimStates.HurtAnimName);
            if (!animating)
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
    }
}