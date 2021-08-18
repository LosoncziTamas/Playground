using UnityEngine;

namespace Prototype02
{
    [CreateAssetMenu(menuName = "My Assets/Wave Data")]
    public class WaveData : ScriptableObject
    {
        public struct TimeTableSlot
        {
            public int delayBeforeSpawn;
            public int spawnCount;
        }

        public AnimationCurve waveDistribution = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public int maxZombieCountPerSpawn;
        public float waveLengthInSeconds;
        
        public TimeTableSlot[] CreateSpawnSchedule()
        {
            var result = new TimeTableSlot[waveDistribution.length];

            var lastTime = 0;
            for (var index = 0; index < waveDistribution.keys.Length; index++)
            {
                var keyFrame = waveDistribution.keys[index];
                var time = Mathf.RoundToInt(Mathf.Clamp01(keyFrame.time) * waveLengthInSeconds);
                var value = Mathf.RoundToInt(Mathf.Clamp01(keyFrame.value) * maxZombieCountPerSpawn);
                result[index] = new TimeTableSlot
                {
                    delayBeforeSpawn = time - lastTime,
                    spawnCount = value
                };
                lastTime = time;
            }

            return result;
        }

    }
}