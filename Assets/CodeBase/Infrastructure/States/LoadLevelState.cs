using CodeBase.Infrastructure.Factory;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _loadingCurtain.Hide();

        private async void OnLoaded()
        {
            InitUIRoot();
            await InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() => _uiFactory.CreateUIRoot();

        private UniTask InitGameWorld()
        {
            return default;
        }
    }
}