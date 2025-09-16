using CodeBase.Data.Skills;
using CodeBase.Infrastructure.EntryPoints.Extensions;
using CodeBase.StaticData;
using CodeBase.UI.Windows.Presenters;
using CodeBase.UseCases.SkillTree;
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
            RegisterUseCases(builder);
            RegisterModels(builder);
            RegisterPresentation(builder);
        }

        private void RegisterUseCases(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SkillTreeUseCase>(Lifetime.Singleton).AsSelf();
        }

        private void RegisterModels(IContainerBuilder builder)
        {
            builder.Register<SkillTreeModel>(Lifetime.Singleton);
        }

        private void RegisterPresentation(IContainerBuilder builder)
        {
            builder.RegisterLayouts(_layoutsRepository);
            builder.RegisterEntryPoint<MainScreenPresenter>();
            builder.RegisterEntryPoint<SkillsTreePresenter>();
        }
    }
}