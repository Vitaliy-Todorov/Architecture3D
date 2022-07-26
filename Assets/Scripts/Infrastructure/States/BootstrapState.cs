using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scrips.Infrastructure;
using System;
using UnityEngine;
using Scripts.Infrastructure.Services.SaveLoad;
using Scripts.Infrastructure.Services.InputService;

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

        public void Enter()
        {
            _sceneLoader.Load(_intial, onLoadwr: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
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

        private static GameFactory GameFactory()
        {
            IAssetProvider assetProvider = AllServices.Container.Single<IAssetProvider>();

            return new GameFactory(assetProvider);
        }

        private SaveLoadService SaveLoadService()
        {
            IPersistentProgressService persistentProgressService = _services.Single<IPersistentProgressService>();
            IGameFactory gameFactory = _services.Single<IGameFactory>();
            
            return new SaveLoadService(persistentProgressService, gameFactory);
        }
    }
}