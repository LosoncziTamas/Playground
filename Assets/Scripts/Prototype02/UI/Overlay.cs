using UnityEngine;
using UnityEngine.UI;

namespace Prototype02.UI
{
    public class Overlay : MonoBehaviour
    {
        public static Overlay Instance { private set; get; }
        
        public enum State
        {
        }
        
        [SerializeField] private Text _mainText;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            Instance = this;
        }
    }
}