using Scrips.Enemy;
using Scripts.Data;
using Scripts.Infrastructure.Services.Factory;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.StaticData;
using UnityEngine;

namespace Scrips.Logic.EnemySpawner
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId _monsterTypeId;

        private string _id;

        [SerializeField]
        private bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public string Id { get => _id; set => _id = value; }

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if(_slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
             GameObject monstr = _factory.
                CreateMonster(_monsterTypeId, transform);
            _enemyDeath = monstr.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if(_enemyDeath != null)
                _enemyDeath.Happened -= Slay;

            _slain = true;
        }
    }
}