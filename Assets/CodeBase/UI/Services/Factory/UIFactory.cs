using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UI_ROOT_PATH = "UI/UIRoot";
        
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _asset;
        
        public Transform UIRoot { get; private set; }

        public UIFactory(
            IAssetProvider assets,
            IStaticDataService staticData,
            IAssetProvider asset)
        {
            _assets = assets;
            _staticData = staticData;
            _asset = asset;
        }

        public void CreateUIRoot()
        {
            if (UIRoot)
                return;
            
            GameObject root = _assets.Instantiate(UI_ROOT_PATH);
            UIRoot = root.transform;
        }
    }
}