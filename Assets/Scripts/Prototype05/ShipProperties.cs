using UnityEngine;

namespace Prototype05
{
    [CreateAssetMenu(menuName = "My Assets/Ship Properties")]
    public class ShipProperties : ScriptableObject
    {
        public float speed;
        public float rotationSpeed;
        public float drag;
        public float maxVelocityMagnitude;
    }
}