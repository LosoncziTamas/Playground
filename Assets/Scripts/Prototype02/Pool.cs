using System;
using System.Collections.Generic;
using Prototype02.Zombie;
using UnityEngine;
using Object = System.Object;

namespace Prototype02
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private GameObject _zombiePrefab;
        [SerializeField] private ZombieData _zombieData;
        
        List<GameObject> _cached = new List<GameObject>();

        private int _poolSize;


        private void PreAwake()
        {
            _poolSize = _zombieData.poolSize;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (var i = 0; i < _poolSize; i++)
            {
                var go = Instantiate(_zombiePrefab, transform);
                _cached.Add(go);
            }
        }

        public void ReturnToPool(GameObject item)
        {
            item.transform.SetParent(transform);
            item.SetActive(false);
            _cached.Add(item);
        }

        public GameObject Spawn()
        {
            if (_cached.Count > 0)
            {
                // unparent, etc
            }

            return Instantiate(_zombiePrefab);
        }

    }
}