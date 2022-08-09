using Scripts.Infrastructure.Services;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreatedShop();
        void CreateUIRoot();
    }
}