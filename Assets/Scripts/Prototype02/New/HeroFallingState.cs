namespace Prototype02.New
{
    public class HeroFallingState : HeroState
    {
        public HeroFallingState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            heroController.Animator.SetFloat(AnimStates.AirSpeedYAnimId, -1.0f);
            heroController.Animator.SetBool(AnimStates.GroundedAnimId, false);
        }

        public override void Exit()
        {
            base.Exit();
            heroController.Animator.SetFloat(AnimStates.AirSpeedYAnimId, 0);
            heroController.Animator.SetBool(AnimStates.GroundedAnimId, true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if (heroController.IsGrounded)
            {
                heroStateMachine.ChangeState(heroController.HeroIdleState);
            }
        }
    }
}