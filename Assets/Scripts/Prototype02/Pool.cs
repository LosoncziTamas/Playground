using System;
using System.Collections.Generic;
using Prototype02.Zombie;
using UnityEngine;

namespace Prototype02
{
    public class Pool : MonoBehaviour
    {
        public static Pool Instance { get; private set; }
        
        [SerializeField] private GameObject _zombiePrefab;
        [SerializeField] private ZombieData _zombieData;

        private readonly List<GameObject> _objects = new List<GameObject>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                throw new InvalidOperationException("Pool already instantiated.");
            }
            // TODO: call this in editor
            PreInitPool();
        }

        private void PreInitPool()
        {
            foreach (var obj in _objects)
            {
                DestroyImmediate(obj);
            }
            _objects.Clear();
            
            var poolSize = _zombieData.poolSize;
            for (var i = 0; i < poolSize; i++)
            {
                var go = Instantiate(_zombiePrefab, transform);
                go.SetActive(false);
                _objects.Add(go);
            }
        }

        private void Update()
        {
            if (!Application.IsPlaying(gameObject))
            {
                if (_objects.Count != _zombieData.poolSize)
                {
                    PreInitPool();
                }
            }
        }

        public void ReturnToPool(GameObject item)
        {
            item.transform.SetParent(transform);
            item.SetActive(false);
        }

        public GameObject Spawn()
        {
            if (transform.childCount > 0)
            {
                var item = transform.GetChild(0).gameObject;
                item.transform.SetParent(null);
                item.SetActive(true);
                return item;
            }
            // TODO: extend pool size
            return default;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}