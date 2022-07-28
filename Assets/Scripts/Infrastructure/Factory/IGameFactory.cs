using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject HeroGameObject { get; set; }
        event Action HeroCreated;

        GameObject CreateCharacter(GameObject at);
        void CreateHud();
        void Cleanup();
    }
}