using UnityEngine;

namespace Prototype05
{
    [CreateAssetMenu(menuName = "My Assets/StopMovementCollisionBehaviour")]
    public class StopMovementCollisionBehaviour : CollisionBehaviour
    {
        public override void OnCollisionEnter()
        {
            Debug.Log("StopMovementCollisionBehaviour OnCollisionEnter");
        }

        public override void OnCollisionExit()
        {
            Debug.Log("StopMovementCollisionBehaviour OnCollisionExit");
        }
    }
}