using UnityEngine;
using Random = UnityEngine.Random;

namespace Prototype05
{
    public class MoveRandomly : MonoBehaviour
    {
        private Vector3 _randomDirection;
        [SerializeField] private float _speed;
        
        private void Start()
        {
            _randomDirection = Random.insideUnitCircle;
        }

        private void FixedUpdate()
        {
            transform.position += _randomDirection * _speed * Time.fixedDeltaTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + _randomDirection * 2.0f);
        }
    }
}
