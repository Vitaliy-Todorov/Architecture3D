using System;

namespace Scripts.Data
{
    [Serializable]
    public partial class PlayerProgress
    {
        public State HeroState;
        public Stats HeroStats;
        public WorldData WorldData;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new State();
            HeroStats = new Stats();
            KillData = new KillData();
        }
    }
}