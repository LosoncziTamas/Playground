using UnityEngine;

namespace Prototype04
{
    public class BotMovement : MonoBehaviour
    {
        private static readonly int Walking = Animator.StringToHash("Walking");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(Idle, true);
        }

        private void Update()
        {
            var lookHorizontal = Input.GetAxis("Mouse X");
            var lookVertical = Input.GetAxis("Mouse Y");
            
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0)
            {
                _animator.SetBool(Walking, true);
                _animator.SetBool(Idle, false);
            }
            else
            {
                _animator.SetBool(Idle, true);
                _animator.SetBool(Walking, false);
            }

        }
    }
}
