using Scripts.Infrastructure.Services.AssetManagement;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.StaticData.Window;
using Scripts.UI.Services.Windows;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRoot = "Prefab/UI/UIRoot";
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public void CreatedShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            Object.Instantiate(config.Prefab, _uiRoot);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Intantiate(UIRoot).transform;
    }
}