using CodeBase.Data.Skills;
using CodeBase.StaticData.SkillsData;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService
    {
        SkillTreeConfig SkillTreeConfig { get; }
        
        void Load();
        SkillConfig GetConfigFor(SkillId type);
    }
}