using Scripts.UI.Elements;
using Scrips.Enemy;
using Scrips.Logic.EnemySpawner;
using Scripts.Data;
using Scripts.Infrastructure.Services.AssetManagement;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Infrastructure.Services.Randomizer;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Logic;
using Scripts.Loots;
using Scripts.StaticData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.UI.Elements;
using Scripts.UI.Services.Windows;
using UnityEngine.SceneManagement;

namespace Scripts.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticData;
        private IRandomService _random;
        private IPersistentProgressService _progressService;
        private IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, IRandomService random, IPersistentProgressService progressService, IWindowService windowService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _random = random;
            _progressService = progressService;
            _windowService = windowService;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            hud.GetComponentInChildren<LootCounter>().
                Construct(_progressService.Progress.WorldData);

            foreach (OpenWindowButtn openWindowButtn in hud.GetComponentsInChildren<OpenWindowButtn>())
                openWindowButtn.Construct(_windowService);

            return hud;
        }

        public GameObject CreateCharacter(Vector3 at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.CharacterPath, at);
            return HeroGameObject;
        }

        public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monstrTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);

            spawner.Id = spawnerId;
            spawner._monsterTypeId = monstrTypeId;
        }

        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeId);
            GameObject monster = GameObject.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);

            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            if(lootSpawner != null)
            {
                lootSpawner.Construct(this, _random);
                lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
            }

            monster.GetComponent<RotateToHero>().Construct(monster.transform);

            EnemyAttack attack = monster.GetComponent<EnemyAttack>();
            attack.Construct(HeroGameObject.transform);
            attack._damage = monsterData.Damage;
            attack._cleaveg = monsterData.Cleavage;
            attack._effectiveDistance = monsterData.EffectiveDistance;

            return monster;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot).
                GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress);

            return lootPiece;
        }

        public void CreateLoot(Loot loot)
        {
            if (loot.PositionOnLevel.Level != SceneManager.GetActiveScene().name)
                return;

            Vector3 position = loot.PositionOnLevel.Position.AsUnityVector();
            LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot, position).
                GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.Progress);
            lootPiece.Initialize(loot);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetProvider.Intantiate(prefabPath);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assetProvider.Intantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}