using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class HpBar : MonoBehaviour
    {
        public Image _imegeCurrent;

        public void SetValue(float current, float max) =>
            _imegeCurrent.fillAmount = current / max;
    }
}