using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        UniTask WarmUp();
        void Cleanup();
    }
}