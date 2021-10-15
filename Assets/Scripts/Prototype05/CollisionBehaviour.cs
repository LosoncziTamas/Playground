using UnityEngine;

namespace Prototype05
{
    public abstract class CollisionBehaviour : ScriptableObject
    {
        public abstract void OnCollisionEnter();

        public abstract void OnCollisionExit();

    }
}