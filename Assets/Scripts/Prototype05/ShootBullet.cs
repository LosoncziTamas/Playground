using UnityEngine;

namespace Prototype05
{
    public class ShootBullet : MonoBehaviour
    {
        [SerializeField] private BulletPool _bulletPool;
        

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var bullet = _bulletPool.Spawn();
                bullet.transform.rotation = transform.rotation;
            }
        }
    }
}
