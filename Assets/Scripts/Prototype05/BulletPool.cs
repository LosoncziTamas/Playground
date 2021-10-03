using System;
using System.Collections.Generic;
using Prototype02;
using UnityEngine;

namespace Prototype05
{
    public class BulletPool : MonoBehaviour
    {
        private const int PoolSize = 20;
        
        [SerializeField] private Bullet _bulletPrefab;

        private readonly List<Bullet> _pooled = new List<Bullet>(PoolSize);

        private void Start()
        {
            for (var i = 0; i < PoolSize; i++)
            {
                var bullet = Instantiate(_bulletPrefab);
                bullet.gameObject.SetActive(false);
                _pooled.Add(bullet);
            }
        }

        public Bullet Spawn()
        {
            Debug.Assert(_pooled.Count > 0);
            var result = _pooled[0];
            _pooled.RemoveAt(0);
            result.gameObject.SetActive(true);
            result.transform.SetParent(null);
            return result;
        }

        public void Despawn(Bullet bullet)
        {
            bullet.transform.SetParent(transform);
            bullet.gameObject.SetActive(false);
            _pooled.Add(bullet);
        }
        
    }
}