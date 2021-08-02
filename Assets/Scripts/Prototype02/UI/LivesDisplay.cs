using UnityEngine;

namespace Prototype02.UI
{
    public class LivesDisplay : MonoBehaviour
    {
        public static LivesDisplay Instance { get; private set; }
    
        [SerializeField] private RectTransform _container;
        [SerializeField] private GameObject _heartPrefab;
    
        private void Awake()
        {
            Instance = this;
        }

        public void UpdateLivesCount(int newCount)
        {
            var oldCount = _container.childCount;
            if (newCount < oldCount)
            {
                for (var i = 0; i < oldCount - newCount; i++)
                {
                    Destroy(_container.GetChild(i).gameObject);
                }
            }
            else if (newCount > oldCount)
            {
                for (var i = 0; i < newCount - oldCount; i++)
                {
                    Instantiate(_heartPrefab, _container);
                }
            }
        }
    }
}
