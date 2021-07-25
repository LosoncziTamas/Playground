using UnityEngine;

namespace Prototype02.New
{
    public class HeroAttackState : HeroState
    {

        private int _attackAnimIndex = 0;
        public HeroAttackState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _attackAnimIndex %= 3;
            switch (_attackAnimIndex)
            {
                case 0:
                    heroController.Animator.SetTrigger(AnimStates.Attack1StateId);
                    break;
                case 1:
                    heroController.Animator.SetTrigger(AnimStates.Attack2StateId);
                    break;
                case 2:
                    heroController.Animator.SetTrigger(AnimStates.Attack3StateId);
                    break;
            }
            _attackAnimIndex++;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time - startTime > heroData.attackDurationInSeconds)
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
            else
            {
                if (heroController.HeroFacingDirection == HeroController.FacingDirection.Right && heroController.EnemyWithinRightHitBox)
                {
                    Debug.Log("[HeroAttackState] enemy hit");
                } 
                else if (heroController.HeroFacingDirection == HeroController.FacingDirection.Left &&
                         heroController.EnemyWithinLeftHitBox)
                {
                    Debug.Log("[HeroAttackState] enemy hit");
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}