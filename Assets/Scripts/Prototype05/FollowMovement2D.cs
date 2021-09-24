using UnityEngine;

namespace Prototype05
{
    public class FollowMovement2D : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _threshold;
        [SerializeField] private float _speed;

        private void FixedUpdate()
        {
            var currPos = transform.position;
            var distance = Vector2.Distance(_target.position, currPos);
            if (distance > _threshold)
            {
                var newPos = Vector3.MoveTowards(currPos, _target.position, _speed * Time.fixedDeltaTime);
                newPos.z = currPos.z;
                transform.position = newPos;
            }
        }
    }
}
