using System.Collections.Generic;
using Prototype02.Zombie;
using UnityEngine;

namespace Prototype02.New
{
    public class HeroAttackState : HeroState
    {
        private int _groundedAttackAnimIndex;
        private int _attackIndex;
        
        public HeroAttackState(HeroController heroController, HeroData heroData, HeroStateMachine heroStateMachine) : base(heroController, heroData, heroStateMachine)
        {
        }

        private int _attackStateId;
        private HeroData.AttackAnimProperties _animProperties;

        public override void Enter()
        {
            base.Enter();
            if (heroController.IsGrounded)
            {
                _groundedAttackAnimIndex %= 2;
                if (_groundedAttackAnimIndex == 0)
                {
                    _attackStateId = AnimStates.Attack1StateId;
                }
                else if (_groundedAttackAnimIndex == 1)
                {
                    _attackStateId = AnimStates.Attack2StateId;
                }
                _groundedAttackAnimIndex++;
                heroController.Rigidbody2D.velocity = Vector2.zero;
                _attackIndex = _groundedAttackAnimIndex;
            }
            else
            {
                _attackStateId = AnimStates.Attack3StateId;
                _attackIndex = 2;
            }

            _animProperties = heroData.attackAnimProperties[_attackIndex];
            heroController.Animator.SetBool(_attackStateId, true);
        }

        public override void Exit()
        {
            base.Exit();
            heroController.Animator.SetBool(AnimStates.Attack1StateId, false);
            heroController.Animator.SetBool(AnimStates.Attack2StateId, false);
            heroController.Animator.SetBool(AnimStates.Attack3StateId, false);
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
                if (startTime + _animProperties.hurtStateStart <= Time.time &&
                    startTime + _animProperties.hurtStateEnd >= Time.time)
                {
                    HitEnemiesWithinHitBoxes();
                }
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