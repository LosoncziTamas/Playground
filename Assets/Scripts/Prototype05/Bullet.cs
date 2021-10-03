using System;
using UnityEngine;

namespace Prototype05
{
    public class Bullet : MonoBehaviour
    {

        public float speed;

        public Vector3 Direction { get; set; }


        private void FixedUpdate()
        {
            transform.position += Direction * speed * Time.fixedDeltaTime;
        }
    }
}