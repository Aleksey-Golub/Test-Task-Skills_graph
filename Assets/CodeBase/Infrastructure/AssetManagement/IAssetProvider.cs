using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        void Initialize();
        T Load<T>(string path) where T : UnityEngine.Object;
        GameObject Instantiate(string path);
        void CleanUp();
    }
}