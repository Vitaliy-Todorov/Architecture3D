using Scrips.Enemy;
using Scripts.Data;
using Scripts.Infrastructure.Services.Factory;
using Scripts.Infrastructure.Services.Randomizer;
using System;
using UnityEngine;

namespace Scripts.Loots
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private IRandomService _random;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }

        private void Start()
        {
            _enemyDeath.Happened += Spawnloot;
        }

        private void Spawnloot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            Loot lootItem = new Loot()
            {
                Id = $"{gameObject.scene.name}_{Guid.NewGuid().ToString()}",
                Value = _random.Next(_lootMin, _lootMax)
            };

            loot.Initialize(lootItem);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}