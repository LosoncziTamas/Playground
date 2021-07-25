namespace Prototype02.New
{
    public class HeroIdleState : HeroState
    {
        public HeroIdleState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            heroController.Animator.SetInteger(AnimStates.AnimStateId, 0);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (heroController.Jumping)
            {
                heroStateMachine.ChangeState(heroController.HeroJumpState);
            }
            else if (heroController.Moving)
            {
                heroStateMachine.ChangeState(heroController.HeroMoveState);
            }
            else if (heroController.Attacking)
            {
                heroStateMachine.ChangeState(heroController.HeroAttackState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}