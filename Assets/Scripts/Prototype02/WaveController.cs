using System;
using System.Collections;
using UnityEngine;

namespace Prototype02
{
    public class WaveController : MonoBehaviour
    {
        [SerializeField] private WaveData[] _waves;
        [SerializeField] private Spawner _spawner;
        
        private int _currentWaveIndex;
        private WaveData _currentWave;
        
        private void Start()
        {
            _currentWave = _waves[_currentWaveIndex];
            StartCoroutine(StartWave(_currentWave));
        }

        private void WaveFinished()
        {
            Debug.Log("Wave finished");
            _currentWaveIndex++;
            if (_waves.Length > _currentWaveIndex)
            {
                _currentWave = _waves[_currentWaveIndex];
                StartCoroutine(StartWave(_currentWave));
            }
        }

        private IEnumerator StartWave(WaveData wave)
        {
            var spawnSchedule = wave.CreateSpawnSchedule();

            for (var i = 0; i < spawnSchedule.Length; i++)
            {
                var slot = spawnSchedule[i];
                yield return new WaitForSecondsRealtime(slot.delayBeforeSpawn);
                for (var j = 0; j < slot.spawnCount; j++)
                {
                    _spawner.Spawn();
                }
            }

            WaveFinished();
        }
    }
}