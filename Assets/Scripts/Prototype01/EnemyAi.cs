using System;
using Pathfinding;
using UnityEngine;

namespace Prototype01
{
    public class EnemyAi : MonoBehaviour
    {
        
        public float nextWayPointDistance = 2.0f;
        public float movementForce = 10.0f;
        
        [SerializeField] private Seeker _seeker;
        [SerializeField] private AIDestinationSetter _destinationSetter;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Transform _playerTransform;

        private int _currentWaypointIdx;
        private Path _path;
        private bool _reachedEndOfPath;
        private Vector2 _nextWayPoint;
        
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
            
            Debug.Log("OnPathCalculated");

            _currentWaypointIdx = 0;
            _path = p;
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
                Debug.Log("reachedEndOfPath");
                return;
            }

            _nextWayPoint = _path.vectorPath[_currentWaypointIdx];
            Debug.Log("nextWayPoint " + _nextWayPoint);

            var directionToGo = (_nextWayPoint - _rigidbody2D.position).normalized;
            _rigidbody2D.AddForce(directionToGo * movementForce * Time.deltaTime);

            var distance = Vector2.Distance(_rigidbody2D.position, _nextWayPoint);
            if (distance < nextWayPointDistance)
            {
                _currentWaypointIdx++;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, _nextWayPoint);
        }
    }
}
