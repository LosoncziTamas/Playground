using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieHurtState : ZombieState
    {
        private const float MaxDistanceX = 0.93f;
        
        public ZombieHurtState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            var offset = HeroController.Instance.transform.position - zombieController.transform.position;
            var actualDistance = Mathf.Abs(offset.x);
            var t = (actualDistance / MaxDistanceX);
            // TODO: hurt closest first
            zombieController.HitPoints--;
            if (zombieController.HitPoints <= 0)
            {
                zombieStateMachine.ChangeState(zombieController.ZombieDeathState);
                return;
            }
            zombieController.Animator.SetBool(AnimStates.HurtAnimId, true);
            if (offset.x > 0)
            {
                zombieController.Rigidbody2D.velocity = new Vector2(-1.0f * zombieData.hurtBackOffX, zombieController.Rigidbody2D.velocity.y);
            }
            else if (offset.x < 0)
            {
                zombieController.Rigidbody2D.velocity = new Vector2(1.0f * zombieData.hurtBackOffX, zombieController.Rigidbody2D.velocity.y);
            }
            zombieController.ZombieIdleCollider.enabled = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (startTime + zombieData.zombieHurtDuration < Time.time)
            {
                zombieStateMachine.ChangeState(zombieController.ZombieMoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            zombieController.Animator.SetBool(AnimStates.HurtAnimId, false);
            zombieController.ZombieIdleCollider.enabled = true;
        }
    }
}