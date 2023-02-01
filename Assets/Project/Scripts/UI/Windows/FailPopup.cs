using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class FailPopup : MonoBehaviour, IWindow
    {
        [SerializeField]
        private Button _goHomeButton = null;
        
        private UISystem _uiSystem;
        
        public bool IsPopup
        {
            get => true;
        }

        public void Prepare(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
            _goHomeButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
           _uiSystem.ShowWindow<MainWindow>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}