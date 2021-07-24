using UnityEngine;

namespace Prototype02
{
    public abstract class AttackBehaviour : ScriptableObject
    {
        public abstract void Attack(Transform transform);
    }
}