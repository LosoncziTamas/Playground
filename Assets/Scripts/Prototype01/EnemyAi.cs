using System;
using Pathfinding;
using UnityEngine;

namespace Prototype01
{
    public class EnemyAi : MonoBehaviour
    {
        
        public float nextWayPointDistance = 2.0f;
        public float movementSpeed = 2.0f;
        public float jumpForce = 6.0f;
        
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

        private void Jump(float jumpForce)
        {
            _rigidbody2D.AddForce(Vector2.up * Time.deltaTime * jumpForce, ForceMode2D.Impulse);
        }

        // TODO: figure out this
        private bool Jumping()
        {
           return  _rigidbody2D.velocity.y < -0.1f || _rigidbody2D.velocity.y > 0.1f;
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
                return;
            }

            _nextWayPoint = _path.vectorPath[_currentWaypointIdx];
            
            var currPos = _rigidbody2D.position;
            var directionToGo = (_nextWayPoint - currPos).normalized;
            
            if (!Jumping())
            {
                _rigidbody2D.AddForce(directionToGo * movementSpeed * Time.deltaTime);
            }
            
            var distance = Vector2.Distance(currPos, _nextWayPoint);
            if (distance < nextWayPointDistance)
            {
                _currentWaypointIdx++;
            }

            _lastPos = _rigidbody2D.position;
        }

        private void OnGUI()
        {
            GUILayout.Space(100.0f);
            if (GUILayout.Button("Jump") && !Jumping())
            {
                Jump(jumpForce);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, _nextWayPoint);
        }
    }
}
