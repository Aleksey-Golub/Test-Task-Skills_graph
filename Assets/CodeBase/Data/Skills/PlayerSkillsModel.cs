using ObservableCollections;
using UniRx;

namespace CodeBase.Data.Skills
{
    public class PlayerSkillsModel
    {
        public ReactiveProperty<int> SkillPoints { get; private set; } = new();
        public ObservableList<SkillId> LearnedSkills { get; } = new();
    }
}