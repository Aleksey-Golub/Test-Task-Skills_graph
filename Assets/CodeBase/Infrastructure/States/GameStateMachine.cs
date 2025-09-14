using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private readonly StatesFactory _statesFactory;
        private IExitableState _activeState;

        public GameStateMachine(StatesFactory statesFactory)
        {
            _states = new();
            _statesFactory = statesFactory;
        }

        public void SetStates()
        {
            _states[typeof(BootstrapState)] = _statesFactory.Create<BootstrapState>();
            _states[typeof(LoadLevelState)] = _statesFactory.Create<LoadLevelState>();
            _states[typeof(LoadProgressState)] = _statesFactory.Create<LoadProgressState>();
            _states[typeof(GameLoopState)] = _statesFactory.Create<GameLoopState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}