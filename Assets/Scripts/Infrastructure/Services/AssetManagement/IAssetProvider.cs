using Scripts.Infrastructure.Services;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Intantiate(string path);
        GameObject Intantiate(string path, Vector3 at);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void CleanUp();
    }
}