using UnityEngine;

namespace Prototype05
{
    public class GameWorld : MonoBehaviour
    {
        [SerializeField] private ShipController _shipController;

        public ShipController ShipController => _shipController;
        // TODO: add other actors
    }
}