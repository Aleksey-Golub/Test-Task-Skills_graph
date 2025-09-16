using System.Linq;
using CodeBase.Data.Skills;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.UIController;
using VContainer;

namespace CodeBase.UseCases.SkillTree
{
    public class SkillTreeUseCase
    {
        [Inject] private readonly IStaticDataService _staticDataService;
        [Inject] private readonly IUIController _uiController;
        [Inject] private readonly PlayerSkillsModel _playerSkills;
        [Inject] private readonly SkillTreeModel _skillTreeModel;
        
        public void HandleOpenSkillsClicked()
        {
            _uiController.Open(WindowId.SkillsTreeScreen);
            FillSkillTreeModel();
        }

        public void HandleAddSkillPointsClicked()
        {
            _playerSkills.SkillPoints.Value += 1;
        }

        private void FillSkillTreeModel()
        {
            var skillsGraph = new SkillsGraph(_staticDataService.SkillTreeConfig);
            foreach (var skillId in _playerSkills.LearnedSkills) 
                skillsGraph.Open(skillId);
            
            _skillTreeModel.Fill(skillsGraph);
        }

        public void LearnSkill(SkillId skillId)
        {
            int skillCost = _staticDataService.GetConfigFor(skillId).Cost;
            _playerSkills.SkillPoints.Value -= skillCost;
            _playerSkills.LearnedSkills.Add(skillId);
            _skillTreeModel.LearnSkill(skillId);
        }

        public void ForgetSkill(SkillId skillId)
        {
            int skillCost = _staticDataService.GetConfigFor(skillId).Cost;
            _playerSkills.SkillPoints.Value += skillCost;
            _playerSkills.LearnedSkills.Remove(skillId);
            _skillTreeModel.ForgetSkill(skillId);
        }

        public void ForgetAllSkills()
        {
            foreach (SkillId skillId in _playerSkills.LearnedSkills.ToList())
            {
                if (skillId is SkillId.SkillBase)
                    continue;
                
                int skillCost = _staticDataService.GetConfigFor(skillId).Cost;
                _playerSkills.SkillPoints.Value += skillCost;
                _playerSkills.LearnedSkills.Remove(skillId);
            }
            _skillTreeModel.ForgetAllSkill();
        }
    }
}