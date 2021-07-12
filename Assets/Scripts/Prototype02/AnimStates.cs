using UnityEngine;

namespace Prototype02
{
    public static class AnimStates
    {
        public const string HurtAnimStateName = "Hurt";
        public const string JumpAnimStateName = "Jump";

        public static readonly int GroundedAnimId = Animator.StringToHash("Grounded");
        public static readonly int AirSpeedYAnimId = Animator.StringToHash("AirSpeedY");
        public static readonly int AnimStateId = Animator.StringToHash("AnimState");
        
        public static readonly int JumpStateId = Animator.StringToHash(JumpAnimStateName);
        public static readonly int HurtAnimId = Animator.StringToHash(HurtAnimStateName);
    }
}