using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieAttackState : ZombieState
    {
        private bool _attackStarted;
        private float _attackStartTime;
        
        public ZombieAttackState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            zombieController.Animator.SetBool(AnimStates.ZombieAttackAnimId, true);
        }

        public override void Exit()
        {
            base.Exit();
            zombieController.Animator.SetBool(AnimStates.ZombieAttackAnimId, false);
        }

        public override void OnAnimEvent(AnimEvent animEvent)
        {
            if (animEvent == AnimEvent.ZombieAttack)
            {
                _attackStarted = true;
                _attackStartTime = Time.time;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!_attackStarted)
            {
                return;
            }
            
            if (_attackStartTime + zombieData.zombieAttackLength >= Time.time)
            {
                zombieController.ZombieAttackCollider.enabled = true;
                zombieController.ZombieIdleCollider.enabled = false;
            }
            else
            {
                zombieController.ZombieAttackCollider.enabled = false;
                zombieController.ZombieIdleCollider.enabled = true;
            }

            var animating = zombieController.Animator.IsAnimationPlaying(AnimStates.ZombieAttackAnimName);
            if (!animating)
            {
                zombieStateMachine.ChangeState(zombieController.ZombieMoveState);
            }
        }
    }
}