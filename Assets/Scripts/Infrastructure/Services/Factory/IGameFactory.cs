using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Loots;
using Scripts.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject HeroGameObject { get; set; }

        GameObject CreateHud();
        GameObject CreateCharacter(Vector3 at);
        void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monstrTypeId);
        GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        LootPiece CreateLoot();
        void CreateLoot(Loot loot);
        void Cleanup();
    }
}