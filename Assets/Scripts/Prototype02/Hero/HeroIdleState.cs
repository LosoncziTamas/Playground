using Prototype02.New;

namespace Prototype02.Hero
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
            else if (heroController.CanAttack() && heroController.Attacking)
            {
                heroStateMachine.ChangeState(heroController.HeroAttackState);
            }
            else if (heroController.Blocking)
            {
                heroStateMachine.ChangeState(heroController.heroIdleBlockState);
            }
            else if (heroController.BeingHurt)
            {
                heroStateMachine.ChangeState(heroController.HeroHurtState);
            }
            else if (!heroController.IsGrounded)
            {
                heroStateMachine.ChangeState(heroController.HeroFallingState);
            }
        }
    }
}