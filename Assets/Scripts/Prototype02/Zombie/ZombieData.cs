using UnityEngine;

namespace Prototype02.Zombie
{
    [CreateAssetMenu(menuName = "My Assets/Zombie Data")]
    public class ZombieData : ScriptableObject
    {
        public float spawnDurationInSeconds;
        public float walkSpeed;
        public float zombieAttackDurationInSeconds;
        public float zombieAttackStart;
        public float zombieAttackEnd;
        public float zombieAttackDistance;
    }
}