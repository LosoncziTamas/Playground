using UnityEngine;

namespace Prototype02
{
    [CreateAssetMenu(menuName = "My Assets/Default Attack Behaviour")]
    public class DefaultAttackBehaviour : AttackBehaviour
    {
        private static readonly int Attack1 = Animator.StringToHash("Attack1");
        private static readonly int Attack2 = Animator.StringToHash("Attack2");
        private static readonly int Attack3 = Animator.StringToHash("Attack3");
        
        private Animator _animator;

        public override void Attack(Transform transform)
        {
            if (_animator == null)
            {
                _animator = transform.GetComponent<Animator>();
            }
            _animator.SetTrigger(Attack1);
        }
    }
}