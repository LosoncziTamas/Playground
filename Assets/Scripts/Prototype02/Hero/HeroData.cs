using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Prototype02.New
{
    [CreateAssetMenu(menuName = "My Assets/Hero Data")]
    public class HeroData : ScriptableObject
    {
        [Serializable]
        public struct AttackAnimProperties
        {
            public float hurtStateStart;
            public float hurtStateEnd;
        }
        
        public float horizontalMovementSpeed;
        public float jumpVelocityY;
        public float attackDurationInSeconds;
        public float hurtDurationInSeconds;
        public float hurtBackOffX;
        public int initialHitPoints;
        public float deathDurationInSeconds;
        public float blockDurationInSeconds;
        public AttackAnimProperties[] attackAnimProperties;
    }
}