using Scrips.Enemy;
using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.Factory;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using Scripts.StaticData;
using System;
using UnityEngine;

namespace Scrips.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId _monsterTypeId;

        private string _id;

        [SerializeField]
        private bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
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
             GameObject monstr = _factory.CreateMonster(_monsterTypeId, transform);
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