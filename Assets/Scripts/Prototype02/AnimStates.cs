using UnityEngine;

namespace Prototype02
{
    public static class AnimStates
    {
        public const string HurtAnimStateName = "Hurt";
        public const string JumpAnimStateName = "Jump";
        public const string Attack1AnimStateName = "Attack1";
        public const string Attack2AnimStateName = "Attack2";
        public const string Attack3AnimStateName = "Attack3";

        public static readonly int GroundedAnimId = Animator.StringToHash("Grounded");
        public static readonly int AirSpeedYAnimId = Animator.StringToHash("AirSpeedY");
        public static readonly int AnimStateId = Animator.StringToHash("AnimState");
        public static readonly int Attack1StateId = Animator.StringToHash(Attack1AnimStateName);
        public static readonly int Attack2StateId = Animator.StringToHash(Attack2AnimStateName);
        public static readonly int Attack3StateId = Animator.StringToHash(Attack3AnimStateName);
        public static readonly int JumpStateId = Animator.StringToHash(JumpAnimStateName);
        public static readonly int HurtAnimId = Animator.StringToHash(HurtAnimStateName);
    }
}