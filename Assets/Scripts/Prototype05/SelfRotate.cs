using UnityEngine;

namespace Prototype05
{
    public class SelfRotate : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.forward, -_speed * Time.fixedDeltaTime, Space.Self);
        }
    }
}
