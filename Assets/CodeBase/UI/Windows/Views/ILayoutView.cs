using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Windows.Views
{
    public interface ILayoutView
    {
        UniTask ShowAsync();
        UniTask HideAsync();
        void Hide();
        void Show();
    }
}