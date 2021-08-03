using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype02.UI
{
    public class Overlay : MonoBehaviour
    {
        public static Overlay Instance { private set; get; }

        private CanvasGroup _canvasGroup;
        
        public enum State
        {
        }
        
        [SerializeField] private Text _mainText;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            Instance = this;
            _canvasGroup = GetComponent<CanvasGroup>();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _canvasGroup.alpha = 1.0f;
            _restartButton.onClick.AddListener(OnRestartClick);
        }

        private void HideSelf()
        {
            gameObject.SetActive(false);
        }

        private void OnRestartClick()
        {
            HeroController.Instance.Revive();
            HideSelf();
        }
    }
}