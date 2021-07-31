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
                Debug.Log("Attack blocked");
            }
            else if (!heroController.Blocking)
            {
                heroController.HeroStateMachine.ChangeState(heroController.HeroIdleState);
            }
        }
    }
}