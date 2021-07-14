using System.Collections;
using Prototype02.States;
using UnityEngine;

namespace Prototype02
{
    public class FallingState : State
    {
        private Animator _animator;
        private Hero _hero;

        public FallingState()
        {
            _animator = Hero.Instance.Animator;
            _hero = Hero.Instance;
        }

        public override IEnumerator Init()
        {
            _animator.SetFloat(AnimStates.AirSpeedYAnimId, -1.0f);
            _animator.SetBool(AnimStates.GroundedAnimId, false);
            while (!_hero.IsGrounded)
            {
                yield return null;
            }
            _hero.SetState(new GroundedState());
        }
    }
}