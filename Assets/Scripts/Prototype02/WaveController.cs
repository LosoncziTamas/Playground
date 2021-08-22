using System.Collections;
using Prototype02.Zombie;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype02
{
    public class WaveController : MonoBehaviour
    {
        private const float FadeDuration = 3.0f;
        
        [SerializeField] private WaveData[] _waves;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Text _waveText;
        
        private int _currentWaveIndex;
        
        private void Start()
        {
            StartCoroutine(StartWave( _waves[_currentWaveIndex]));
            _waveText.CrossFadeAlpha(1.0f, 0.0f, false);
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
            _waveText.CrossFadeAlpha(1.0f, FadeDuration, true);
            yield return new WaitForSeconds(FadeDuration);

            _waveText.CrossFadeAlpha(0.0f, FadeDuration, true);
            var spawnSchedule = wave.CreateSpawnSchedule();

            for (var i = 0; i < spawnSchedule.Length; i++)
            {
                var slot = spawnSchedule[i];
                yield return new WaitForSeconds(slot.delayBeforeSpawn);
                for (var j = 0; j < slot.spawnCount; j++)
                {
                    yield return new WaitForSeconds(0.1f);
                    _spawner.Spawn();
                }
            }

            while (ZombieController.EnabledInstances.Count > 0)
            {
                yield return new WaitForSeconds(1.0f);
            }

            _waveText.text = $"Wave {_currentWaveIndex + 1} completed";
            _waveText.CrossFadeAlpha(1.0f, FadeDuration, true);
            yield return new WaitForSeconds(FadeDuration);
            
            _waveText.CrossFadeAlpha(0.0f, FadeDuration, true);
            yield return new WaitForSeconds(FadeDuration);
            
            WaveFinished();
        }
    }
}