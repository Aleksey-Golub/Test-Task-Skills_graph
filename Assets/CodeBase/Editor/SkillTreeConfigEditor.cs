using CodeBase.Data.Skills;
using CodeBase.StaticData.SkillsData;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SkillTreeConfig))]
    public class SkillTreeConfigEditor : UnityEditor.Editor
    {
        private SkillTreeConfig _target;

        private void OnEnable()
        {
            _target = (SkillTreeConfig)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Validate Tree"))
            {
                Debug.Log($"Start SkillTreeConfig Validation");

                // TODO validate other: node links direction, unique ids etc.
                
                SkillsGraph graph = new SkillsGraph(_target);
                if (!graph.IsLinked())
                {
                    Debug.LogError($"SkillTreeConfig Validation failed: graph is not linked");
                    return;
                }
                
                Debug.Log($"End SkillTreeConfig Validation");
            }
        }
    }
}