using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        [Range(1, 100)]
        public float Hp;
        [Range(1, 30)]
        public float Damage;
        [Range(1, 10)]
        public float MoveSpeed;

        public int MinLoot;
        public int MaxLoot;

        [Range(.5f, 2)]
        public float EffectiveDistance;
        [Range(.5f, 2)]
        public float Cleavage;

        public GameObject Prefab;
    }
}