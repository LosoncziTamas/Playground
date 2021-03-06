using UnityEngine;

namespace Prototype02.Zombie
{
    public class ZombieHurtState : ZombieState
    {
        private static readonly int OverrideWithWhite = Shader.PropertyToID("_OverrideWithWhite");
        
        public ZombieHurtState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            // zombieController.HitPoints--;
            if (zombieController.HitPoints <= 0)
            {
                zombieStateMachine.ChangeState(zombieController.ZombieDeathState);
                return;
            }
            
            var offset = HeroController.Instance.transform.position - zombieController.transform.position;
            zombieController.SpriteRenderer.material.SetFloat(OverrideWithWhite, 1.0f);
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
            zombieController.SpriteRenderer.material.SetFloat(OverrideWithWhite, 0.0f);
            zombieController.ZombieIdleCollider.enabled = true;
        }
    }
}