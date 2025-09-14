using CodeBase.Infrastructure.States;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        [Inject]
        private void Construct(Game game)
        {
            Debug.Log($"[GameBootstrapper] Construct called...");
            _game = game;
        }

        private void Awake()
        {
            Debug.Log($"[GameBootstrapper] Awake called...");
            _game.StateMachine.SetStates();
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}