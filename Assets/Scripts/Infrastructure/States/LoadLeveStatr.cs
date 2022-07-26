using Scripts.Infrastructure.Factory;
using Scrips.Infrastructure;
using Scrips.Logic;
using Scripts.CameraLogic;
using UnityEngine;
using System;
using Scripts.Infrastructure.Services.PersistentProgress;

namespace Scripts.Infrastructure.States
{
    internal class LoadLeveStatr : IPlaylaodedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;

        public LoadLeveStatr(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService persistentProgressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            InitGameWord();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_persistentProgressService.Progress);
        }

        private void InitGameWord()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject character = _gameFactory.CreateCharacter(at: initialPoint);
            _gameFactory.CreateHud();

            CameraFllow(character);
        }

        private static void CameraFllow(GameObject character)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(character);
        }
    }
}