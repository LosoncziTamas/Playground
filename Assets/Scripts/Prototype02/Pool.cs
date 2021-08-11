using System;
using Prototype02.Zombie;
using UnityEngine;

namespace Prototype02
{
    [ExecuteAlways]
    public class Pool : MonoBehaviour
    {
        public static Pool Instance { get; private set; }
        
        [SerializeField] private GameObject _zombiePrefab;
        [SerializeField] private ZombieData _zombieData;
        
        
        #if false
        private void Awake()
        {
            if (Application.IsPlaying(gameObject))
            {
                if (Instance == null)
                {
                    Instance = this;
                }
                else
                {
                    throw new InvalidOperationException("Pool already instantiated.");
                }
            }
        }
#endif

        private void PreInitPool()
        {
            var poolSize = _zombieData.poolSize;
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
            
            for (var i = 0; i < poolSize; i++)
            {
                var go = Instantiate(_zombiePrefab, transform);
                go.SetActive(false);
            }
        }

        private void Update()
        {
            if (!Application.IsPlaying(gameObject))
            {
                Debug.Log("In edit mode update");
                if (transform.childCount != _zombieData.poolSize)
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