using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Prototype02
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _zombiePrefab;

        private BoxCollider2D _boxCollider;
        private BoxCollider2D _heroCollider;
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            _heroCollider = HeroController.Instance.GetComponent<BoxCollider2D>();
        }

        private void OnGUI()
        {
            GUILayout.Space(300);
            if (GUILayout.Button("Spawn"))
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            var bounds = _boxCollider.bounds;

            var spawnXPos = Random.Range(bounds.min.x, bounds.max.x);
            var spawnYPos = bounds.min.y;

            var heroBounds = _heroCollider.bounds;
            
            var heroXMin = heroBounds.min.x;
            var heroXMax = heroBounds.max.x;
            
            while (spawnXPos >= heroXMin && spawnXPos <= heroXMax)
            {
                spawnXPos = Random.Range(bounds.min.x, bounds.max.x);
            }

            Instantiate(_zombiePrefab, new Vector2(spawnXPos, spawnYPos), Quaternion.identity);
        }
    }
}