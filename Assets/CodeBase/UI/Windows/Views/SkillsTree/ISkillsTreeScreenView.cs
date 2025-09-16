using System;
using System.Collections.Generic;
using CodeBase.Data.Skills;
using CodeBase.StaticData.SkillsData;

namespace CodeBase.UI.Windows.Views
{
    public interface ISkillsTreeScreenView : ILayoutView
    {
        ISelectedSkillPanel SelectedSkillPanel { get; }
        Dictionary<SkillId, ISkillView> Views { get; }

        event Action CloseClicked;
        event Action AddSkillPointClicked;
        
        void SetSkillPointsText(string text);
        void DrawTree(List<SkillConfig> skillGraph);
    }
}
