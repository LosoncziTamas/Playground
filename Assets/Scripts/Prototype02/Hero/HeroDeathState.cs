using Prototype02.New;
using Prototype02.UI;
using UnityEngine;

namespace Prototype02.Hero
{
    public class HeroDeathState : HeroState
    {
        public HeroDeathState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            heroController.Animator.SetBool(AnimStates.DeathAnimId, true);
            Overlay.Instance.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            heroController.Animator.SetBool(AnimStates.DeathAnimId, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (startTime + heroData.deathDurationInSeconds <= Time.time)
            {
                // TODO: respawn
                
            }
        }
    }
}