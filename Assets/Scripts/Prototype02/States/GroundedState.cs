using System.Collections;
using UnityEngine;

namespace Prototype02.States
{
    public class GroundedState : State
    {
        private Animator _animator;
        private Hero _hero;
        private SpriteRenderer _spriteRenderer;
        
        private bool _jumping;
        private bool _attacking;
        private int _facingDirection;

        public GroundedState()
        {
            _animator = Hero.Instance.Animator;
            _hero = Hero.Instance;
            _spriteRenderer = _hero.SpriteRenderer;
        }
        
        public override IEnumerator Init()
        {
            _animator.SetFloat(AnimStates.AirSpeedYAnimId, 0);
            _animator.SetBool(AnimStates.GroundedAnimId, true);
            if (_hero.Moving)
            {
                _hero.StartCoroutine(Move());
                // Set to moving
            }
            else if (_hero.Idle)
            {
                _hero.StartCoroutine(Idle());
                // Set to idle
            }
            yield break;
        }

        public override IEnumerator Move()
        {
            _animator.SetInteger(AnimStates.AnimStateId, 1);
            var horizontal = Input.GetAxis("Horizontal");
            while (horizontal > 0 || horizontal < 0)
            {
                if (horizontal < 0)
                {
                    if (_facingDirection > 0)
                    {
                        _spriteRenderer.flipX = true;
                    }
                    _facingDirection = -1;
                }
                else if (horizontal > 0)
                {
                    if (_facingDirection < 0)
                    {
                        _spriteRenderer.flipX = false;
                    }
                    _facingDirection = 1;
                }
                yield return null;
                horizontal = Input.GetAxis("Horizontal");
                _hero.Rigidbody2D.MovePosition(horizontal * Vector2.right * 0.1f);
            }

            
            
            
            yield break;
        }
        
        public override IEnumerator Jump(Vector2 velocityOffset)
        {
            if (_jumping)
            {
                yield break;
            }

            _jumping = true;
            _hero.Rigidbody2D.velocity += velocityOffset;
            _animator.SetTrigger(AnimStates.JumpStateId);
            while (_animator.AnimatorIsPlaying(AnimStates.JumpAnimStateName))
            {
                yield return null;
            }

            _animator.SetFloat(AnimStates.AirSpeedYAnimId, -1);
            _animator.SetBool(AnimStates.GroundedAnimId, false);
            while (!_hero.IsGrounded)
            {
                yield return null;
            }
            _jumping = false;
        }

        public override IEnumerator Attack()
        {
            if (_attacking)
            {
                yield break;
            }

            _attacking = true;
            _animator.SetTrigger(AnimStates.Attack1StateId);
            while (_animator.AnimatorIsPlaying(AnimStates.AttackAnimStateName))
            {
                yield return null;
            }
            _attacking = false;
        }

        public override IEnumerator GetHurt(Vector2 backOff)
        {
            _animator.SetTrigger(AnimStates.HurtAnimId);
            while (_animator.AnimatorIsPlaying(AnimStates.HurtAnimStateName))
            {
                yield return null;
                _hero.Rigidbody2D.MovePosition(backOff);
            }
        }

        public override IEnumerator Idle()
        {
            _animator.SetInteger(AnimStates.AnimStateId, 0);
            var inAttackState = false;
            var inJumpingState = false;
            while (true)
            {
                yield return null;
                if (_hero.Attacking && !inAttackState)
                {
                    inAttackState = true;
                    _animator.SetTrigger(AnimStates.Attack1StateId);
                    while (_animator.AnimatorIsPlaying(AnimStates.AttackAnimStateName))
                    {
                        yield return null;
                    }
                    inAttackState = false;
                }
                else if (_hero.Jumping && !inJumpingState)
                {
                    inJumpingState = true;
                    _hero.Rigidbody2D.velocity += new Vector2(0, 4.0f);
                    _animator.SetTrigger(AnimStates.JumpStateId);
                    _animator.SetBool(AnimStates.GroundedAnimId, false);
                    yield return new WaitForFixedUpdate();
                    while (_hero.Rigidbody2D.velocity.y > 0.0f)
                    {
                        if (_hero.Moving)
                        {
                            var horizontal = Input.GetAxis("Horizontal");
                            if (horizontal < 0)
                            {
                                if (_facingDirection > 0)
                                {
                                    _spriteRenderer.flipX = true;
                                }
                                _facingDirection = -1;
                            }
                            else if (horizontal > 0)
                            {
                                if (_facingDirection < 0)
                                {
                                    _spriteRenderer.flipX = false;
                                }
                                _facingDirection = 1;
                            }
                            _hero.Rigidbody2D.velocity += new Vector2(horizontal * 0.1f, 0.0f);
                        }
                        yield return null;
                    }
                    
                    _animator.SetFloat(AnimStates.AirSpeedYAnimId, -1.0f);
                    while (!_hero.IsGrounded)
                    {
                        if (_hero.Moving)
                        {
                            var horizontal = Input.GetAxis("Horizontal");
                            if (horizontal < 0)
                            {
                                if (_facingDirection > 0)
                                {
                                    _spriteRenderer.flipX = true;
                                }
                                _facingDirection = -1;
                            }
                            else if (horizontal > 0)
                            {
                                if (_facingDirection < 0)
                                {
                                    _spriteRenderer.flipX = false;
                                }
                                _facingDirection = 1;
                            }
                            _hero.Rigidbody2D.velocity += new Vector2(horizontal * 0.1f, 0.0f);
                        }
                        yield return null;
                    }
                    _hero.Rigidbody2D.velocity = Vector2.zero;
                    _animator.SetFloat(AnimStates.AirSpeedYAnimId, 0);
                    _animator.SetBool(AnimStates.GroundedAnimId, true);
                    inJumpingState = false;
                }
                else if (_hero.Moving)
                {
                    _animator.SetInteger(AnimStates.AnimStateId, 1);
                    var horizontal = Input.GetAxis("Horizontal");
                    while (horizontal > 0 || horizontal < 0)
                    {
                        if (horizontal < 0)
                        {
                            if (_facingDirection > 0)
                            {
                                _spriteRenderer.flipX = true;
                            }
                            _facingDirection = -1;
                        }
                        else if (horizontal > 0)
                        {
                            if (_facingDirection < 0)
                            {
                                _spriteRenderer.flipX = false;
                            }
                            _facingDirection = 1;
                        }
                        yield return null;
                        horizontal = Input.GetAxis("Horizontal");
                        _hero.Rigidbody2D.velocity += new Vector2(horizontal * 0.1f, 0.0f);
                    }
                    _animator.SetInteger(AnimStates.AnimStateId, 0);
                }
                else if (_hero.CollidingWithEnemy)
                {
                    
                }
            }
            yield break;
        }
    }
}