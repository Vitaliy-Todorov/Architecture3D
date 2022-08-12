using Scripts.Infrastructure.Services.Factory;
using Scrips.Infrastructure;
using Scrips.Logic;
using Scripts.CameraLogic;
using UnityEngine;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scrips.Character;
using Scripts.Data;
using Scripts.Infrastructure.Services.StaticData;
using UnityEngine.SceneManagement;
using Scripts.UI.Elements;
using System;
using Scripts.UI.Services.Factory;
using Scrips.Logic.EnemySpawner;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Infrastructure.States
{
    internal class LoadLeveStatr : IPlaylaodedState<string>
    {

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;

        public LoadLeveStatr(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService persistentProgressService, IStaticDataService staticData, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
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
            InitUIRoot();
            InitGameWord();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() => 
            _uiFactory.CreateUIRoot();

        private void InitGameWord()
        {
            InitLevelTransferTrigger(LevelStaticData());
            InitSpawnerLoot();
            InitSpawnerEnemy(LevelStaticData());
            GameObject character = CreateCharacter(LevelStaticData());

            CreateHud(character);
            CameraFllow(character);
        }

        private void InitLevelTransferTrigger(LevelStaticData levelData)
        {
            List<LevelTransferTrigger> levelTransferTriggers =
                GameObject.FindObjectsOfType<LevelTransferTrigger>()
                .ToList();

            foreach (LevelTransferTrigger transferTrigger in levelTransferTriggers)
                transferTrigger.Construct(_stateMachine);
        }

        private void InitSpawnerLoot()
        {
            foreach(Loot loot in _persistentProgressService.Progress.LootsOnMap)
                _gameFactory.CreateLoot(loot);
        }

        private void InitSpawnerEnemy(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawner)
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonstrTypeId);
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_persistentProgressService.Progress);
        }

        private GameObject CreateCharacter(LevelStaticData levelStaticData) => 
            _gameFactory.CreateCharacter(at: levelStaticData.InitialHeroPosition);

        private GameObject CreateHud(GameObject character)
        {
            GameObject hud = _gameFactory.CreateHud();
            HeroHealth heroHealth = character.GetComponentInChildren<HeroHealth>();
            hud.GetComponentInChildren<ActorUI>()
                .Construct(heroHealth);
            return hud;
        }

        private LevelStaticData LevelStaticData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            return _staticData.ForLevel(sceneKey);
        }

        private static void CameraFllow(GameObject character)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(character);
        }
    }
}