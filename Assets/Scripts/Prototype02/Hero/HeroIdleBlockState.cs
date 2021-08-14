using Prototype02.New;
using UnityEngine;

namespace Prototype02.Hero
{
    public class HeroIdleBlockState : HeroState
    {
        public HeroIdleBlockState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            heroController.Animator.SetBool(AnimStates.IdleBlockId, true);
            heroController.Animator.SetTrigger(AnimStates.IdleBlockTriggerId);
        }

        public override void Exit()
        {
            base.Exit();
            heroController.Animator.SetBool(AnimStates.IdleBlockId, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (heroController.Blocking && heroController.BeingHurt)
            {
                var offset = heroController.LastHurtCollider.transform.position - heroController.transform.position;
                var attackBlocked = offset.x > 0 && heroController.HeroFacingDirection == FacingDirection.Right ||
                                    offset.x < 0 && heroController.HeroFacingDirection == FacingDirection.Left;
                if (attackBlocked)
                {
                    heroController.HeroStateMachine.ChangeState(heroController.heroBlockState);
                }
                else
                {
                    heroController.HeroStateMachine.ChangeState(heroController.HeroHurtState);
                }
            }
            else if (!heroController.Blocking)
            {
                heroController.HeroStateMachine.ChangeState(heroController.HeroIdleState);
            }
        }
    }
}