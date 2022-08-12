using Scripts.StaticData;
using System;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StaticData
{
    [Serializable]
    public partial class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeId MonstrTypeId;
        public Vector3 Position;

        public EnemySpawnerData(string id, MonsterTypeId monstrTypeId, Vector3 position)
        {
            Id = id;
            MonstrTypeId = monstrTypeId;
            Position = position;
        }
    }
}