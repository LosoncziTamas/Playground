using UnityEngine;

namespace Prototype02
{
    public static class Utils
    {
        public static bool AnimatorIsPlaying(this Animator animator)
        {
            return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
        
        public static bool AnimatorIsPlaying(this Animator animator, string stateName)
        {
            return animator.AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }
    }
}