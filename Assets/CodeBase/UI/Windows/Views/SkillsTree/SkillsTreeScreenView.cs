using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Skills;
using CodeBase.StaticData.SkillsData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Views
{
    public class SkillsTreeScreenView : LayoutViewBase, ISkillsTreeScreenView
    {
        [SerializeField] private Button _closeButton;
        
        [SerializeField] private SkillView _skillViewPrefab;
        [SerializeField] private GameObject _connectionPrefab;
        [SerializeField] private RectTransform _parent;
        
        [SerializeField] private TextMeshProUGUI _skillPointsText;
        [SerializeField] private Button _addSkillPointsButton;
        [SerializeField] private SelectedSkillPanel _selectedSkillPanel;

        private readonly List<GameObject> _connectors = new();
        
        public Dictionary<SkillId, ISkillView> Views { get; private set; } = new();
        public ISelectedSkillPanel SelectedSkillPanel => _selectedSkillPanel;
        
        public event Action CloseClicked;
        public event Action AddSkillPointClicked;

        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseClicked);
            _addSkillPointsButton.onClick.AddListener(OnAddSkillPointsClicked);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseClicked);
            _addSkillPointsButton.onClick.RemoveListener(OnAddSkillPointsClicked);
        }

        public void SetSkillPointsText(string text) => _skillPointsText.text = text;
        
        public void DrawTree(List<SkillConfig> skillGraph)
        {
            foreach (var skillConfig in skillGraph)
            {
                SkillView view = CreateSkillView(skillConfig);
                view.SetSelected(false);
                Views[skillConfig.Id] = view;

                foreach (SkillId linkedSkill in skillConfig.LinkedSkills)
                {
                    if (Views.ContainsKey(linkedSkill))
                        continue;
                    
                    var connector = CreateConnection(skillConfig.SkillTreePosition, skillGraph.First(c => c.Id == linkedSkill).SkillTreePosition);
                    _connectors.Add(connector);
                }
            }
        }

        private SkillView CreateSkillView(SkillConfig config)
        {
            var view = Instantiate(_skillViewPrefab, _parent);
            var rect = view.GetComponent<RectTransform>();
            rect.anchoredPosition = config.SkillTreePosition;

            view.Init(config.Id, config.Name);
            return view;
        }
        
        private GameObject CreateConnection(Vector2 start, Vector2 end)
        {
            GameObject connector = Instantiate(_connectionPrefab, _parent);
            var rectTransform = connector.GetComponent<RectTransform>();
            rectTransform.SetSiblingIndex(0);

            Vector2 direction = end - start;
            float distance = direction.magnitude;

            rectTransform.sizeDelta = new Vector2(distance, rectTransform.sizeDelta.y);
            rectTransform.anchoredPosition = (start + direction / 2f);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rectTransform.localRotation = Quaternion.Euler(0, 0, angle);

            return connector;
        }
        
        private void OnCloseClicked()
        {
            CloseClicked?.Invoke();
            
            foreach (ISkillView view in Views.Values) 
                view.Destroy();

            foreach (GameObject connector in _connectors) 
                Destroy(connector);

            Views.Clear();
        }

        private void OnAddSkillPointsClicked() => AddSkillPointClicked?.Invoke();
    }
}