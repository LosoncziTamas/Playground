using UnityEngine;

namespace Prototype02.New
{
    [CreateAssetMenu(menuName = "My Assets/Hero Data")]
    public class HeroData : ScriptableObject
    {
        public float horizontalMovementSpeed;
        public float jumpVelocityY;
        public float hurtBackOffX;
        public int initialHitPoints;
        public float deathDurationInSeconds;
        public float blockDurationInSeconds;
        public float delayBetweenAttacksInSeconds;
    }
}