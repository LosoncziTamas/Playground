using System;
using UnityEngine;

namespace Prototype02
{
    public class VerticalMover : MonoBehaviour
    {
        [SerializeField] public Vector3 _targetOffset;
        [SerializeField] public float _maxTime;
        
        private Vector3 _startPos;
        private float _time;
        private int _sign;

        private void Start()
        {
            _startPos = transform.position;
            _sign = 1;
        }

        private void Update()
        {
            _time += Time.deltaTime * _sign;
            if (_time <= 0)
            {
                _sign = 1;
            }
            if (_time >= _maxTime)
            {
                _sign = -1;
            }
            // TODO: implement
            
        }
    }
}
