using UnityEngine;

namespace Prototype02
{
    [CreateAssetMenu(menuName = "My Assets/Game Data")]
    public class GameData : ScriptableObject
    {
        public float cameraSpeedDuringMovement;
        public float cameraCatchUpSpeed;
        public float cameraMovementMinDistance;
        public float cameraMovementMaxDistance;
        public Vector3 cameraHeroOffset;
    }
}