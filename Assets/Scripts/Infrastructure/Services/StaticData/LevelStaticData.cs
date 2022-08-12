using Scrips.Logic.EnemySpawner;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string KeyLevel;
        public List<EnemySpawnerData> EnemySpawner;
        public Vector3 InitialHeroPosition;
    }
}