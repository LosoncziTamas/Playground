using UnityEngine;

namespace Prototype02
{
    public static class Utility
    {
        private const int BaseLayerIndex = 0;
        
        public static bool IsAnimationPlaying(this Animator animator, string animName)
        {
            return animator.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName(animName);
        }
    }
}