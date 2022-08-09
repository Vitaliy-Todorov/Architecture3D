using Scripts.Infrastructure.Services;
using UnityEngine;

namespace Scripts.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Intantiate(string path);
        GameObject Intantiate(string path, Vector3 at);
    }
}