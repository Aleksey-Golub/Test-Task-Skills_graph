using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string INITIAL = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public BootstrapState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IAssetProvider assets,
            IStaticDataService staticData
        )
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _assets = assets;
            _staticData = staticData;
        }

        public void Enter()
        {
            InitializeServices();

            _sceneLoader.Load(INITIAL, onLoaded: EnterLoadLevel);
        }

        private void InitializeServices()
        {
            _assets.Initialize();
            _staticData.Load();
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}