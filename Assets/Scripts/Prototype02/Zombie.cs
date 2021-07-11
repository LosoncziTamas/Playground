using System;
using UnityEngine;

namespace Prototype02
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Zombie : MonoBehaviour
    {
        public float Speed = 1.0f;
        private Rigidbody2D _rigidbody2D;
        private Hero _hero;
        
        private void Start()
        {
            _hero = Hero.Instance;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var offset = Vector2.MoveTowards(transform.position, _hero.transform.position, Speed * Time.deltaTime);
            // _rigidbody2D.MovePosition(offset);
        }
    }
}