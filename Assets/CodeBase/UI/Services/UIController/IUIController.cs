using CodeBase.UI.Windows.Presenters;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.UIController
{
    public interface IUIController
    {
        void AddPresenter(ILayoutPresenter targetPresenter);
        void RemovePresenter(ILayoutPresenter targetPresenter);
        UniTask Open(WindowId windowId);
        bool IsOpened(WindowId windowId);
        UniTask Close(WindowId windowId);
    }
}