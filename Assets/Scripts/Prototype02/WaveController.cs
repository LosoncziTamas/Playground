using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype02
{
    public class WaveController : MonoBehaviour
    {
        [SerializeField] private WaveData[] _waves;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Text _waveText;
        
        private int _currentWaveIndex;
        
        private void Start()
        {
            StartCoroutine(StartWave( _waves[_currentWaveIndex]));
        }
        

        private void WaveFinished()
        {
            _currentWaveIndex++;
            if (_waves.Length > _currentWaveIndex)
            {
                StartCoroutine(StartWave(_waves[_currentWaveIndex]));
            }
        }

        private IEnumerator StartWave(WaveData wave)
        {
            _waveText.text = $"Wave {_currentWaveIndex + 1}";
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