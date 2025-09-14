using System;

namespace CodeBase.UI.Windows.Views
{
    public interface IMainScreenView : ILayoutView
    {
        event Action OpenSkillsClicked;
    }
}