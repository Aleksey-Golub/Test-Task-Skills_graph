using CodeBase.UI.Services.UIController;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        [Inject] private readonly GameStateMachine _stateMachine;
        [Inject] private readonly IUIController _uiController;

        public void Enter()
        {
            _uiController.Open(WindowId.MainScreen);
        }

        public void Exit()
        {
        }
    }
}