using Scripts.Infrastructure.Services;
using UnityEngine;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Intantiate(string path);
        GameObject Intantiate(string path, Vector2 at);
    }
}