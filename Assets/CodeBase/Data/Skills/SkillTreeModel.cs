using UniRx;

namespace CodeBase.Data.Skills
{
    public class SkillTreeModel
    {
        private SkillsGraph _skillsGraph;

        public ReactiveCommand<Unit> Filled { get; private set; } = new();
        
        public void LearnSkill(SkillId skillId)
        {
            _skillsGraph.Open(skillId);
        }

        public void ForgetSkill(SkillId skillId)
        {
            _skillsGraph.Close(skillId);
        }
        
        public void ForgetAllSkill()
        {
            _skillsGraph.Reset();
        }
        
        public void Fill(SkillsGraph skillsGraph)
        {
            _skillsGraph = skillsGraph;
            
            Filled.Execute(Unit.Default);
        }

        public bool IsLearned(SkillId skillId) => _skillsGraph.IsOpen(skillId);
        public bool CanOpen(SkillId skillId) => _skillsGraph.CanOpen(skillId);
        public bool CanClose(SkillId skillId) => _skillsGraph.CanClose(skillId);
    }
}