using UnityEngine;

namespace Prototype02.Zombie
{
    [CreateAssetMenu(menuName = "My Assets/Zombie Data")]
    public class ZombieData : ScriptableObject
    {
        public float walkSpeed;
        public float zombieAttackLength;
        public float zombieAttackDistance;
        public float zombieHurtDuration;
        public float hurtBackOffX;
        public int initialHitPoints;
        public float deathDuration;
        public int poolSize;
    }
}