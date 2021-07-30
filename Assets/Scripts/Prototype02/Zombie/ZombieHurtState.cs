using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieHurtState : ZombieState
    {
        private int _hitPoints;
        
        public ZombieHurtState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
            _hitPoints = zombieData.initialHitPoints;
        }

        public override void Enter()
        {
            base.Enter();
            _hitPoints--;
            if (_hitPoints < 0)
            {
                zombieStateMachine.ChangeState(zombieController.ZombieDeathState);
                return;
            }
            zombieController.Animator.SetBool(AnimStates.HurtAnimId, true);
            var offset = HeroController.Instance.transform.position - zombieController.transform.position;
            if (offset.x > 0)
            {
                zombieController.Rigidbody2D.velocity = new Vector2(-1.0f * zombieData.hurtBackOffX, zombieController.Rigidbody2D.velocity.y);
            }
            else if (offset.x < 0)
            {
                zombieController.Rigidbody2D.velocity = new Vector2(1.0f * zombieData.hurtBackOffX, zombieController.Rigidbody2D.velocity.y);
            }
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

        }
    }
}