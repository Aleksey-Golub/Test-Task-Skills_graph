using System.Linq;
using CodeBase.StaticData;
using CodeBase.UI.Windows.Views;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.EntryPoints.Extensions
{
    public static class LayoutsInstallerExtensions
    {
        public static void RegisterLayouts(this IContainerBuilder builder, LayoutsRepository layoutsRepository)
        {
            // чтобы не писать регистрации в контейнере для каждой View, собираем их всех в цикле
            foreach (var view in layoutsRepository.Views)
            {
                var viewTypes = view
                    .GetType()
                    .GetInterfaces()
                    .Where(IsLayoutViewInterface)
                    .ToArray();
                var result = builder.RegisterComponentInNewPrefab(view, Lifetime.Scoped).As(viewTypes);
            }
        }

        private static bool IsLayoutViewInterface(System.Type @interface)
        {
            return typeof(ILayoutView).IsAssignableFrom(@interface)
                   && !@interface.IsGenericType
                   && @interface != typeof(ILayoutView);
        }
    }
}