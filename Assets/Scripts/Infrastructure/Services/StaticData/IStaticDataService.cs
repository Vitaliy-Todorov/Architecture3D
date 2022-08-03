using Scripts.StaticData;

namespace Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadMonster();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
    }
}