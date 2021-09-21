using UnityEngine;

namespace Prototype05
{
    public class ShipController : MonoBehaviour
    {

        private Vector3 _velocity;
        
        void Start()
        {
        
        }

        private void Update()
        {
            var rotation = -Input.GetAxis("Horizontal");
            var acceleration = Input.GetAxis("Vertical");
            transform.Rotate(Vector3.forward, rotation);
            Debug.Log("rotation" + transform.rotation);
            Debug.Log("eulerAngles" + transform.rotation.eulerAngles);
            Debug.Log("eulerAngles * acceleration" + transform.rotation.eulerAngles * acceleration);
            
        }
    }
}
