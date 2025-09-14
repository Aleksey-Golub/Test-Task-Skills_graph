using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.EntryPoints
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner _coroutineRunnerPrefab;
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        public override void Install(IContainerBuilder builder)
        {
            Debug.Log($"[GameInstaller] Install called...");
        
            builder.Register<StatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab<CoroutineRunner>(_coroutineRunnerPrefab, Lifetime.Singleton).As<ICoroutineRunner>();
            builder.RegisterComponentInNewPrefab<LoadingCurtain>(_loadingCurtainPrefab, Lifetime.Singleton);
            builder.Register<Game>(Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);

            builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);

            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
        }
    }
}
