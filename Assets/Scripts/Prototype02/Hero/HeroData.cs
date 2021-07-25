using UnityEngine;
using UnityEngine.Serialization;

namespace Prototype02.New
{
    [CreateAssetMenu(menuName = "My Assets/Hero Data")]
    public class HeroData : ScriptableObject
    {
        public float horizontalMovementSpeed;
        public float jumpVelocityY;
        public float attackDurationInSeconds;
        public float hurtDurationInSeconds;
        public float hurtBackOffX;
    }
}