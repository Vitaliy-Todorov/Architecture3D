using Scripts.Infrastructure.Services.Ads;
using Scripts.Infrastructure.Services.AssetManagement;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.StaticData.Window;
using Scripts.UI.Services.Windows;
using Scripts.UI.Windows;
using Scripts.UI.Windows.Shop;
using UnityEngine;

namespace Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRoot = "Prefab/UI/UIRoot";
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IAdsService _adsService;
        private readonly IPersistentProgressService _progressService;
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, IAdsService adsService, IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _adsService = adsService;
            _progressService = progressService;
        }

        public void CreatedShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            ShopWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            window.Construct(_adsService, _progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Intantiate(UIRoot).transform;
    }
}