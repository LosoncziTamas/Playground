using UnityEngine;

namespace Prototype02
{
    public class ActiveWhenVisible : MonoBehaviour
    {
        [SerializeField] private Collider2D _objectCollider;
        private IActivatable _activatable;
        private Collider2D _cameraCollider;
        private bool _touching;
        
        private void Awake()
        {
            _cameraCollider = Camera.main.GetComponent<Collider2D>();
            _activatable = GetComponent<IActivatable>();
        }

        private void FixedUpdate()
        {
            var collidersTouch = _cameraCollider.IsTouching(_objectCollider);
            if (!_touching && collidersTouch)
            {
                _activatable.Activate();
                _touching = true;
            }
            else if (_touching && !collidersTouch)
            {
                _activatable.Deactivate();
                _touching = false;
            }
        }
    }
}