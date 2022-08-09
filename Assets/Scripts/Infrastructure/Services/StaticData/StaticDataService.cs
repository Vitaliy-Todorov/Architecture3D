using Scripts.StaticData;
using Scripts.StaticData.Window;
using Scripts.UI.Services.Windows;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataMonstersPath = "StaticData/Monsters";
        private const string StaticDataLevelsPath = "StaticData/Level";
        private const string StaticDataWindiwsPath = "StaticData/UI/WindowData";
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _level;
        private Dictionary<WindowId, WindowConfig> _windiwConfigs;

        public StaticDataService() => 
            LoadData();

        public void LoadData()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(StaticDataMonstersPath)
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _level = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.KeyLevel, x => x);

            _windiwConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindiwsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData)
            ? staticData
            : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _level.TryGetValue(sceneKey, out LevelStaticData staticData)
            ? staticData
            : null;

        public WindowConfig ForWindow(WindowId windowId) =>
            _windiwConfigs.TryGetValue(windowId, out WindowConfig windowConfig) 
            ? windowConfig 
            : null;
    }
}