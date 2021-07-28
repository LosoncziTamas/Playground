using System.Collections.Generic;
using Prototype02.Zombie;
using UnityEngine;

namespace Prototype02.New
{
    public class HeroAttackState : HeroState
    {
        // TODO: hurt while attacked
        private int _attackAnimIndex;
        
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
                var zombieController = enemy.GetComponent<ZombieController>();
                if (zombieController.ZombieStateMachine.CurrentState != zombieController.ZombieHurtState)
                {
                    zombieController.ZombieStateMachine.ChangeState(zombieController.ZombieHurtState);
                }
            }
        }
        

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}