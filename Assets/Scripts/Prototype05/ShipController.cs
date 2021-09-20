using UnityEngine;

namespace Prototype05
{
    public class ShipController : MonoBehaviour
    {
        void Start()
        {
        
        }

        private void Update()
        {
            var rotation = -Input.GetAxis("Horizontal");
            var acceleration = Input.GetAxis("Vertical");
            transform.Rotate(Vector3.forward, rotation);
            Debug.Log("rotation " + rotation + " acceleration " + acceleration);
        }
    }
}
