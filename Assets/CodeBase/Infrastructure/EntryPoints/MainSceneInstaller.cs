using CodeBase.Infrastructure.EntryPoints.Extensions;
using CodeBase.StaticData;
using CodeBase.UI.Windows.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.EntryPoints
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private LayoutsRepository _layoutsRepository;
        
        public override void Install(IContainerBuilder builder)
        {
            RegisterPresentation(builder);
        }

        private void RegisterPresentation(IContainerBuilder builder)
        {
            builder.RegisterLayouts(_layoutsRepository);
            builder.RegisterEntryPoint<MainScreenPresenter>();
        }
    }
}