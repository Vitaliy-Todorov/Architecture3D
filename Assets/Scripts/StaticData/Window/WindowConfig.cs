using Scripts.UI.Services.Windows;
using Scripts.UI.Windows;
using System;

namespace Scripts.StaticData.Window
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}