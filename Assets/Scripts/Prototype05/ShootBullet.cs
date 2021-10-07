using UnityEngine;

namespace Prototype05
{
    public class ShootBullet : MonoBehaviour
    {
        [SerializeField] private BulletPool _bulletPool;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var bullet = _bulletPool.Spawn();
                bullet.ShootBy(_transform, _bulletPool);
            }
        }
    }
}
