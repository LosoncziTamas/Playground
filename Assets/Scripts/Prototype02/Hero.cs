using System;
using UnityEngine;

namespace Prototype02
{
    public class Hero : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int AirSpeedY = Animator.StringToHash("AirSpeedY");

        [SerializeField] private CollisionSensor _groundCollisionSensor;

        private bool _grounded;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (!_grounded && _groundCollisionSensor.Colliding)
            {
                _grounded = true;
                _animator.SetBool(Grounded, _grounded);
            }
            else if (_grounded && !_groundCollisionSensor.Colliding)
            {
                _grounded = false;
                _animator.SetBool(Grounded, _grounded);
            }
        }

        private void Start()
        {
            _animator.SetFloat(AirSpeedY, -1.0f);
        }

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            if (horizontal < 0)
            {
                
            }
            else if (horizontal > 0)
            {
                
            }
            else
            {
                
            }
        }
    }
}