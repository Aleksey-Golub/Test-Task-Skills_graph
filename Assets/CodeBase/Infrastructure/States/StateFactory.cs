using VContainer;

namespace CodeBase.Infrastructure.States
{
    public class StatesFactory
    {
        private readonly IObjectResolver _container;

        public StatesFactory(IObjectResolver container) =>
            _container = container;

        public TState Create<TState>() where TState : IExitableState
        {
            return (TState)_container.Resolve(new RegistrationBuilder(typeof(TState), Lifetime.Transient).Build());
        }
    }
}