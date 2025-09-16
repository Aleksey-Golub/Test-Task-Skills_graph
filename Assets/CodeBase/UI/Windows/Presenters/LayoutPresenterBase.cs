using System;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.UIController;
using CodeBase.UI.Windows.Views;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace CodeBase.UI.Windows.Presenters
{
    public abstract class LayoutPresenterBase<TView> : ILayoutPresenter, IInitializable, IDisposable where TView : ILayoutView
    {
        [Inject] protected TView LayoutView;
        [Inject] protected readonly IUIController Controller;
        [Inject] private readonly IUIFactory _uiFactory;
        
        public abstract WindowId WindowId { get; }
        
        public virtual void Initialize()
        {
            Controller.AddPresenter(this);
        }

        public virtual void Dispose()
        {
            Controller.RemovePresenter(this);
        }

        public virtual async UniTask ActivateAsync()
        {
            (LayoutView as LayoutViewBase).SetRoot(_uiFactory.UIRoot);
            await LayoutView.ShowAsync();
        }

        public virtual async UniTask DeactivateAsync()
        {
            await LayoutView.HideAsync();
        }
    }
}