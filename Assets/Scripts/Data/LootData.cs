using System;

namespace Scripts.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public Action Changed;

        internal void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }

        internal void Add(int loot)
        {
            Collected += loot;
            Changed?.Invoke();
        }
    }
}