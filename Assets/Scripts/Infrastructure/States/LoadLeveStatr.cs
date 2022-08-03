using Scripts.Infrastructure.Services.Factory;
using Scrips.Infrastructure;
using Scrips.Logic;
using Scripts.CameraLogic;
using UnityEngine;
using System;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.UI;
using Scrips.Character;

namespace Scripts.Infrastructure.States
{
    internal class LoadLeveStatr : IPlaylaodedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string EnemySpawner = "EnemySpawner";
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

        private void InitGameWord()
        {
            InitSpawner();
            GameObject character = CreateCharacter();

            CreateHud(character);
            CameraFllow(character);
        }

        private void InitSpawner()
        {
            foreach( GameObject spawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawner))
            {
                EnemySpawner spawner = spawnerObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_persistentProgressService.Progress);
        }

        private GameObject CreateCharacter()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject character = _gameFactory.CreateCharacter(at: initialPoint);
            return character;
        }

        private GameObject CreateHud(GameObject character)
        {
            GameObject hud = _gameFactory.CreateHud();
            HeroHealth heroHealth = character.GetComponentInChildren<HeroHealth>();
            hud.GetComponentInChildren<ActorUI>()
                .Construct(heroHealth);
            return hud;
        }

        private static void CameraFllow(GameObject character)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(character);
        }
    }
}