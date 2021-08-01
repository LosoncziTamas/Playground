using Prototype02.New;
using UnityEngine;

namespace Prototype02.Hero
{
    public class HeroBlockState : HeroState
    {
        public HeroBlockState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            heroController.Animator.SetBool(AnimStates.IdleBlockId, true);
            heroController.Animator.SetTrigger(AnimStates.BlockId);
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

        public override void Exit()
        {
            base.Exit();
            heroController.Animator.SetBool(AnimStates.IdleBlockId, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (startTime + heroData.blockDurationInSeconds <= Time.time)
            {
                if (heroController.Blocking)
                {
                    heroController.HeroStateMachine.ChangeState(heroController.heroIdleBlockState);
                }
            }
            if (!heroController.Blocking)
            {
                heroController.HeroStateMachine.ChangeState(heroController.HeroIdleState);
            }
        }
    }
}