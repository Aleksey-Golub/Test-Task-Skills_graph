using CodeBase.UI.Services.UIController;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Windows.Presenters
{
    public interface ILayoutPresenter
    {
        public WindowId WindowId { get; }
        public UniTask ActivateAsync();
        public UniTask DeactivateAsync();
    }
}