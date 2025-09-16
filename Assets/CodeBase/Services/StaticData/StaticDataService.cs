using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Skills;
using CodeBase.StaticData.SkillsData;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string SKILL_TREE_CONFIG_PATH = "Static Data/Skills/SkillTreeConfig";
        
        private Dictionary<SkillId, SkillConfig> _skillsConfigs;
        
        public SkillTreeConfig SkillTreeConfig { get; private set; }
        
        public void Load()
        {
            SkillTreeConfig = Resources.Load<SkillTreeConfig>(SKILL_TREE_CONFIG_PATH);
            _skillsConfigs = SkillTreeConfig.SkillGraph.ToDictionary(r => r.Id, r => r);
        }
        
        public SkillConfig GetConfigFor(SkillId skillId) => _skillsConfigs[skillId];
    }
}