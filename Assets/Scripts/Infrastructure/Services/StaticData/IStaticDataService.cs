using Scripts.StaticData;
using Scripts.StaticData.Window;
using Scripts.UI.Services.Windows;

namespace Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadData();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId windowId);
    }
}