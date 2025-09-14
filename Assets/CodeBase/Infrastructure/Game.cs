using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(GameStateMachine gameStateMachine)
        {
            StateMachine = gameStateMachine;
        }
    }
}