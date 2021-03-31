using UnityEngine;

namespace Prototype01
{
    public class SelectAnimation : StateMachineBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        private SpriteRenderer GetRenderer(Component component)
        {
            if (!_spriteRenderer)
            {
                _spriteRenderer = component.GetComponent<SpriteRenderer>();
            }

            return _spriteRenderer;
        }
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GetRenderer(animator).color = Color.yellow;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GetRenderer(animator).color = Color.magenta;
        }
    }
}