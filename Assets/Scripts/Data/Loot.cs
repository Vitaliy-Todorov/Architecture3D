using System;

namespace Scripts.Data
{
    [Serializable]
    public class Loot
    {
        public string Id;
        public int Value;

        public PositionOnLevel PositionOnLevel;
    }
}