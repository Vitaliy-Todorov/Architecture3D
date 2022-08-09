using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;

        private void Awake()
        {
            OnAwake();
        }

        protected void OnAwake() =>
            CloseButton.onClick.AddListener(() => Destroy(gameObject));
    }
}