using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.EntryPoints
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}
