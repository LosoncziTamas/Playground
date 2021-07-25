using UnityEngine;

namespace Prototype02.New
{
    [CreateAssetMenu(menuName = "My Assets/Hero Data")]
    public class HeroData : ScriptableObject
    {
        public float playerSpeed;
        public float horizontalMovementSpeed;
        public float jumpVelocityY;
        public float attackDurationInSeconds;
    }
}