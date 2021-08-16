using Prototype02.Hero;
using UnityEngine;

namespace Prototype02.New
{
    public class HeroState
    {
        protected HeroController heroController;
        protected HeroData heroData;
        protected HeroStateMachine heroStateMachine;

        protected float startTime;

        public HeroState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine)
        {
            this.heroController = heroController;
            this.heroData = heroData;
            this.heroStateMachine = heroStateMachine;
        }

        public virtual void Enter()
        {
            startTime = Time.time;
        }

        public virtual void Exit()
        {
            
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            
        }

        public virtual void OnAnimEvent(AnimEvent animEvent)
        {
            Debug.Log("OnAnimEvent " + animEvent);
        }
    }
}