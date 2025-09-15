using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.SkillsData
{
    [CreateAssetMenu(fileName = "SkillTreeConfig", menuName = "Configs/Skills/SkillTreeConfig")]
    public class SkillTreeConfig : ScriptableObject
    {
        [field: SerializeField] public List<SkillConfig> SkillGraph { get; private set; }
    }
}