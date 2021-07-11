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

        public override IEnumerator Idle()
        {
            _animator.SetInteger(AnimStates.AnimStateId, 0);
            yield break;
        }
    }
}