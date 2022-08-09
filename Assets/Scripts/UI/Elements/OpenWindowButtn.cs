using Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class OpenWindowButtn : MonoBehaviour
    {
        public Button _button;
        public WindowId _windowId;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService) =>
            _windowService = windowService;

        private void Awake() => 
            _button.onClick.AddListener(Open);

        private void Open() =>
            _windowService.Open(_windowId);
    }
}