using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.StaticData;
using System;
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
        GameObject CreateCharacter(GameObject at);
        GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);

        void Cleanup();
        void Register(ISavedProgressReader savedProgress);
    }
}