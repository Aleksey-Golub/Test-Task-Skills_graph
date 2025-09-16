using System;
using CodeBase.Data.Skills;

namespace CodeBase.UI.Windows.Views
{
    public interface ISelectedSkillPanel
    {
        public SkillId SelectedSkillId { get; }
        
        event Action<SkillId> CloseClicked;
        event Action<SkillId> LearnClicked;
        event Action<SkillId> ForgetClicked;
        event Action<SkillId> ForgetAllClicked;
        
        void Show();
        void Hide();
        void SetData(SkillId skillId, bool canBeLearned, bool canBeForgot, string nameText, string costText);
    }
}