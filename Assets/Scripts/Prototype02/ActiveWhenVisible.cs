using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Collider2D))]
    public class ActiveWhenVisible : MonoBehaviour
    {
        private IActivatable _activatable;

        private void Awake()
        {
            _activatable = GetComponent<IActivatable>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.MainCamera))
            {
                _activatable.Activate();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.MainCamera))
            {
                _activatable.Deactivate();
            }
        }
    }
}