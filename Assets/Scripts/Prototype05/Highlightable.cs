using System.Collections;
using UnityEngine;

namespace Prototype05
{
    public class Highlightable : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Color _defaultColor;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor = _spriteRenderer.color;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Radar"))
            {
                StartCoroutine(Highlight());
            }        
        }

        private IEnumerator Highlight()
        {
            for (var i = 100; i > 0; i--)
            {
                _spriteRenderer.color = new Color(0, 1, 0, i * 0.01f);
                yield return null;
            }
            _spriteRenderer.color = _defaultColor;
        }
    }
}
