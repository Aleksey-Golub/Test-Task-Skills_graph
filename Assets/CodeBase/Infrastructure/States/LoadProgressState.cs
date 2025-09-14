namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string MAIN_SCENE = "Main";
        
        private readonly GameStateMachine _gameStateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(MAIN_SCENE);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
        }
    }
}