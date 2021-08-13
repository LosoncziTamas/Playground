using System.Collections.Generic;
using Prototype02.Zombie;
using UnityEngine;

namespace Prototype02.New
{
    public class HeroAttackState : HeroState
    {
        private int _groundedAttackAnimIndex;
        
        public HeroAttackState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            if (heroController.IsGrounded)
            {
                _groundedAttackAnimIndex %= 2;
                if (_groundedAttackAnimIndex == 0)
                {
                    heroController.Animator.SetBool(AnimStates.Attack1StateId, true);
                }
                else if (_groundedAttackAnimIndex == 1)
                {
                    heroController.Animator.SetBool(AnimStates.Attack2StateId, true);
                }
                _groundedAttackAnimIndex++;
                heroController.Rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                heroController.Animator.SetBool(AnimStates.Attack3StateId, true);
            }

            // TODO: find a more fine grained way for determining animations states
            var time = heroController.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            
        }

        public override void Exit()
        {
            base.Exit();
            heroController.LastAttackTime = Time.time;
            heroController.Animator.SetBool(AnimStates.Attack1StateId, false);
            heroController.Animator.SetBool(AnimStates.Attack2StateId, false);
            heroController.Animator.SetBool(AnimStates.Attack3StateId, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (startTime + heroData.attackDurationInSeconds < Time.time)
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
                if (heroController.BeingHurt)
                {
                    heroController.HeroStateMachine.ChangeState(heroController.HeroHurtState);
                }
            }
        }

        public override void OnAnimEvent(AnimEvent animEvent)
        {
            if (animEvent == AnimEvent.HeroAttack)
            {
                HitEnemiesWithinHitBoxes();
            }
        }

        private void HitEnemiesWithinHitBoxes()
        {
            var enemies = new List<Collider2D>();
            
            switch (heroController.HeroFacingDirection)
            {
                case FacingDirection.Right when heroController.EnemyWithinRightHitBox:
                    enemies.AddRange(heroController.GetEnemiesFromHitBox(FacingDirection.Right));
                    break;
                case FacingDirection.Left when heroController.EnemyWithinLeftHitBox:
                    enemies.AddRange(heroController.GetEnemiesFromHitBox(FacingDirection.Left));
                    break;
            }

            for (var index = 0; index < enemies.Count; index++)
            {
                var enemy = enemies[index];
                var zombieController = enemy.GetComponentInParent<ZombieController>();
                if (zombieController.ZombieStateMachine.CurrentState != zombieController.ZombieHurtState && zombieController.ZombieStateMachine.CurrentState != zombieController.ZombieDeathState)
                {
                    zombieController.ZombieStateMachine.ChangeState(zombieController.ZombieHurtState);
                }
            }
        }
    }
}