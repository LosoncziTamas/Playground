using System;
using UnityEngine;

namespace Prototype05
{
    public class ShipEnemy : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("OnTriggerEnter2D");
            if (other.gameObject.CompareTag("Radar"))
            {
                _spriteRenderer.color = Color.red;
            }        
        }
    }
}
