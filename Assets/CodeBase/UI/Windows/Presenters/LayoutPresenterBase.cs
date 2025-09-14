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
        [Inject] private readonly IUIController _controller;
        [Inject] private readonly IUIFactory _uiFactory;
        
        public abstract WindowId WindowId { get; }
        
        public virtual void Initialize()
        {
            _controller.AddPresenter(this);
        }

        public virtual void Dispose()
        {
            _controller.RemovePresenter(this);
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