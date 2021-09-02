using UnityEngine;

namespace Prototype02
{
    [CreateAssetMenu(menuName = "My Assets/Game Data")]
    public class GameData : ScriptableObject
    {
        public float cameraMovementMaxDistance;
        public float wallMovementSpeed;
    }
}