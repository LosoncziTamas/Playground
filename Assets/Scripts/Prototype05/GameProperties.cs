using UnityEngine;

namespace Prototype05
{
    [CreateAssetMenu(menuName = "My Assets/Game Properties")]
    public class GameProperties : ScriptableObject
    {
        public float tankSpeed;
        public float tankRotationSpeed;
        public float tankDrag;
        public float tankMaxVelocityMagnitude;
        
        public float bulletSpeed;
        public float bulletDrag;
        public float bulletMaxVelocityMagnitude;
        public float bulletLifeTime;
    }
}