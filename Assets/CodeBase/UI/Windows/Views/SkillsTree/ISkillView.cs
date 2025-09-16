using System;
using CodeBase.Data.Skills;

namespace CodeBase.UI.Windows.Views
{
    public interface ISkillView
    {
        event Action<SkillId> Clicked;
        
        void SetSelected(bool isSelected);
        void SetLearned(bool isLearned);
        void Destroy();
    }
}