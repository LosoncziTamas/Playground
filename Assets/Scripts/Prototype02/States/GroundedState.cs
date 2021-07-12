using System.Collections;
using UnityEngine;

namespace Prototype02
{
    public class GroundedState : State
    {
        private Animator _animator;
        private Hero _hero;

        public GroundedState()
        {
            _animator = Hero.Instance.Animator;
            _hero = Hero.Instance;
        }
        
        public override IEnumerator Init()
        {
            _animator.SetFloat(AnimStates.AirSpeedYAnimId, 0);
            _animator.SetBool(AnimStates.GroundedAnimId, true);
            yield break;
        }

        public override IEnumerator Move(Vector2 offset)
        {
            _animator.SetInteger(AnimStates.AnimStateId, 1);
            _hero.Rigidbody2D.MovePosition(_hero.Rigidbody2D.position + offset);
            yield break;
        }

        public override IEnumerator Jump(Vector2 velocityOffset)
        {
            _hero.Rigidbody2D.velocity += velocityOffset;
            _animator.SetTrigger(AnimStates.JumpStateId);
            while (_animator.AnimatorIsPlaying(AnimStates.JumpAnimStateName) || !_hero.IsGrounded)
            {
                yield return null;
            }
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
            yield break;
        }
    }
}