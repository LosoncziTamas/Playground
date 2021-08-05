using System.Collections.Generic;
using UnityEngine;

namespace Prototype02
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private GameObject _zombiePrefab;
        
        List<GameObject> _cached = new List<GameObject>();
        
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