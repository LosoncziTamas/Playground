using System;
using UnityEngine;

namespace Prototype05
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        
        private void FixedUpdate()
        {
            transform.position += transform.right * speed * Time.fixedDeltaTime;
        }
    }
}