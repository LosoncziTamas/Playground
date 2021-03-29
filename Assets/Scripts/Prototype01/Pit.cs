using System;
using System.Collections;
using UnityEngine;

namespace Prototype01
{
    public class Pit : MonoBehaviour
    {
        public static Pit Instance;
        
        private const int WaveLength = 60;
        
        [SerializeField] private GameObject _enemyPrefab;

        private int _remainingSecondsInWave = WaveLength;
        private readonly int[] _enemyCountPerWaveSecond = new int[WaveLength];
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            InitWave();
            StartCoroutine(UpdateTick());
            // TODO: implement waves
            // TODO: create enemy attributes
        }

        private void InitWave()
        {
            _enemyCountPerWaveSecond[3] = 2;
            /*
            _enemyCountPerWaveSecond[10] = 1;
            _enemyCountPerWaveSecond[30] = 3;
            _enemyCountPerWaveSecond[50] = 4;*/
        }

        private void WaveComplete()
        {
            
        }


        private IEnumerator UpdateTick()
        {
            while (_remainingSecondsInWave > 0)
            {
                yield return new WaitForSeconds(1);
                var index = WaveLength - _remainingSecondsInWave;
                SpawnEnemy(_enemyCountPerWaveSecond[index]);
                _remainingSecondsInWave--;
            }

            WaveComplete();
        }
        
        private void SpawnEnemy(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            }
        }

        public void RespawnInTheFuture(GameObject enemy)
        {
            enemy.SetActive(false);
            StartCoroutine(Respawn(enemy, 2.0f));
        }

        private static IEnumerator Respawn(GameObject enemy, float seconds)
        {
            while (true)
            {
                yield return new WaitForSeconds(seconds);
                enemy.SetActive(true);
                break;
            }
        }
    }
}