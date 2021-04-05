using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Prototype01
{
    public class EnemyAi : MonoBehaviour
    {
        public float nextWayPointDistance = 2.0f;
        public float movementSpeed = 2.0f;
        public Vector2 jumpForce = new Vector2(0, 1.5f);
        
        [SerializeField] private Seeker _seeker;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private int _currentWaypointIdx;
        private Path _path;
        private bool _reachedEndOfPath;
        private Vector2 _nextWayPoint;
        private float _jumpScale;
        private Vector2 _targetPosition;
        private bool _onLand;

        private void Start()
        {
            _nextWayPoint = transform.position;
            SetTarget();
            _targetPosition = Player.Instance.transform.position;
        }

        private void SetTarget()
        {
            _seeker.StartPath(transform.position, _targetPosition, OnPathCalculated);

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
            _rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
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
            
            if (_onLand && !IsInvoking(nameof(Jump)))
            {
                ScheduleRandomJump();
            }
        }

        private void ScheduleRandomJump()
        {
            var jumpTime = Random.Range(0f, 2.0f);
            Invoke(nameof(Jump), jumpTime);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, _nextWayPoint);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_onLand && IsLandCollision(other))
            {
                _onLand = true;
            }
        }

        private static bool IsLandCollision(Collision2D other)
        {
            const string landTag = "Land";
            return other.gameObject.CompareTag(landTag);
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (_onLand && IsLandCollision(other))
            {
                _onLand = false;
            }
        }
    }
}
