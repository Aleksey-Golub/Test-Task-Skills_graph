using CodeBase.Data.Skills;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.UIController;
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
            
            RegisterServices(builder);
            RegisterModels(builder);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<StatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab<CoroutineRunner>(_coroutineRunnerPrefab, Lifetime.Singleton)
                .As<ICoroutineRunner>();
            builder.RegisterComponentInNewPrefab<LoadingCurtain>(_loadingCurtainPrefab, Lifetime.Singleton);
            builder.Register<Game>(Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);

            builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);

            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<IUIController, UIController>(Lifetime.Singleton);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
        }

        private void RegisterModels(IContainerBuilder builder)
        {
            builder.Register<PlayerSkillsModel>(Lifetime.Singleton);
        }
    }
}
