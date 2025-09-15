using CodeBase.Data.Skills;
using UnityEngine;

namespace CodeBase.StaticData.SkillsData
{
    [CreateAssetMenu(fileName = "SkillConfig", menuName = "Configs/Skills/SkillConfig")]
    public class SkillConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 SkillTreePosition { get; private set; }
        [field: SerializeField] public SkillId Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public SkillId[] LinkedSkills { get; private set; }
    }
}
