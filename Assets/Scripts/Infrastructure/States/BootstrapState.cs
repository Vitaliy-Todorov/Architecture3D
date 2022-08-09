using Scripts.Infrastructure.Services.AssetManagement;
using Scripts.Infrastructure.Services.Factory;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scrips.Infrastructure;
using System;
using UnityEngine;
using Scripts.Infrastructure.Services.SaveLoad;
using Scripts.Infrastructure.Services.InputService;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Infrastructure.Services.Randomizer;
using Scripts.UI.Services.Factory;
using Scripts.UI.Services.Windows;

namespace Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string _intial = "Initial";
        private const string _main = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.Load(_intial, onLoadwr: EnterLoadLevel);

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _services.RegisterSingle<IRandomService>(new RandomService());

            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IStaticDataService>(new StaticDataService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());

            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            _services.RegisterSingle<IUIFactory>(UIFactory());
            _services.RegisterSingle<IWindowService>(WindowService());
            
            _services.RegisterSingle<IGameFactory>(GameFactory());
            _services.RegisterSingle<ISaveLoadService>(SaveLoadService());
        }

        public void Exit()
        {
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new KeyboardMouseInputServuce();
            else
                throw new Exception("InputServices == null");
        }

        private GameFactory GameFactory()
        {
            IAssetProvider assetProvider = _services.Single<IAssetProvider>();
            IStaticDataService staticData = _services.Single<IStaticDataService>();
            IRandomService random = _services.Single<IRandomService>();
            IPersistentProgressService progress = _services.Single<IPersistentProgressService>();
            IWindowService windowService = _services.Single<IWindowService>();

            return new GameFactory(assetProvider, staticData, random, progress, windowService);
        }

        private SaveLoadService SaveLoadService()
        {
            IPersistentProgressService persistentProgressService = _services.Single<IPersistentProgressService>();
            IGameFactory gameFactory = _services.Single<IGameFactory>();
            
            return new SaveLoadService(persistentProgressService, gameFactory);
        }
        
        private UIFactory UIFactory()
        {
            IAssetProvider assetProvider = _services.Single<IAssetProvider>();
            IStaticDataService staticData = _services.Single<IStaticDataService>();
            return new UIFactory(assetProvider, staticData);
        }

        private WindowService WindowService()
        {
            IUIFactory uiFactory = _services.Single<IUIFactory>();
            return new WindowService(uiFactory);
        }
    }
}