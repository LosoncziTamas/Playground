using UnityEngine;

namespace Prototype01
{
    public class Pit : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;

        private void Start()
        {
            SpawnEnemy();
        }

        public void SpawnEnemy()
        {
            var enemy = Instantiate(_enemyPrefab, transform);
        }
    }
}