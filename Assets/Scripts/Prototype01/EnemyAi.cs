using Pathfinding;
using UnityEngine;

namespace Prototype01
{
    public class EnemyAi : MonoBehaviour
    {
        
        public float nextWayPointDistance = 2.0f;
        public float movementSpeed = 2.0f;
        public Vector2 jumpForce = new Vector2(0, 1.5f);
        
        [SerializeField] private Seeker _seeker;
        [SerializeField] private AIDestinationSetter _destinationSetter;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Transform _playerTransform;

        private int _currentWaypointIdx;
        private Path _path;
        private bool _reachedEndOfPath;
        private Vector2 _nextWayPoint;
        private Vector2 _lastPos;
        private float _jumpScale;
        
        private void Start()
        {
            _nextWayPoint = transform.position;
            SetTarget();
        }

        private void SetTarget()
        {
            _seeker.StartPath(transform.position, _playerTransform.position, OnPathCalculated);

        }

        private void OnPathCalculated(Path p)
        {
            if (p.error)
            {
                Debug.LogError(p.errorLog);
                return;
            }
            
            _currentWaypointIdx = 0;
            _path = p;
        }

        private void Jump()
        {
            var jumpVec = Vector2.up * Time.deltaTime * jumpForce;
            Debug.Log(jumpVec);
            _rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        private bool Jumping()
        {
           return _rigidbody2D.velocity.y < -0.1f || _rigidbody2D.velocity.y > 0.1f;
        }

        private void FixedUpdate()
        {
            if (_path == null)
            {
                return;
            }

            _reachedEndOfPath = _currentWaypointIdx >= _path.vectorPath.Count;
            if (_reachedEndOfPath)
            {
                SetTarget();
                Debug.Log("Reached target");
                return;
            }

            _nextWayPoint = _path.vectorPath[_currentWaypointIdx];
            
            var currPos = _rigidbody2D.position;
            var directionToGo = (_nextWayPoint - currPos).normalized;
            
            _rigidbody2D.AddForce(directionToGo * movementSpeed * Time.deltaTime);
            
            var distance = Vector2.Distance(currPos, _nextWayPoint);
            if (distance < nextWayPointDistance)
            {
                _currentWaypointIdx++;
            }
            
            if (!Jumping() && !IsInvoking(nameof(Jump)))
            {
                ScheduleRandomJump();
            }

            _lastPos = _rigidbody2D.position;
        }

        private void ScheduleRandomJump()
        {
            var jumpTime = Random.Range(0f, 2.0f);
            Debug.Log("ScheduleRandomJump" + jumpTime);
            Invoke(nameof(Jump), jumpTime);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, _nextWayPoint);
        }
    }
}
