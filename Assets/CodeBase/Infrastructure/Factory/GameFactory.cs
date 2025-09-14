using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private GameObject _heroGameObject;
        private readonly IObjectResolver _objectResolver;

        public GameFactory(
            IAssetProvider assets,
            IStaticDataService staticData,
            IObjectResolver objectResolver
        )
        {
            _assets = assets;
            _staticData = staticData;
            _objectResolver = objectResolver;
        }

        public UniTask WarmUp()
        {
            return default;
        }

        public void Cleanup()
        {
            _assets.CleanUp();
        }
    }
}