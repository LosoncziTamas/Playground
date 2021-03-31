using UnityEngine;

namespace Prototype01
{
    public class SimpleAnim : MonoBehaviour
    {
        private static readonly int IsSelected = Animator.StringToHash("IsSelected");

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Select"))
            {
                _animator.SetBool(IsSelected, true);
            }

            if (GUILayout.Button("Unselect"))
            {
                _animator.SetBool(IsSelected, false);
            }
        }
    }
}