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
                // TODO: check attack from behind
                heroController.HeroStateMachine.ChangeState(heroController.heroBlockState);
            }
            else if (!heroController.Blocking)
            {
                heroController.HeroStateMachine.ChangeState(heroController.HeroIdleState);
            }
        }
    }
}