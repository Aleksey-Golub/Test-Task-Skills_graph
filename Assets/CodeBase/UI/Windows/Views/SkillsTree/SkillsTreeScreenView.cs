using System;
using System.Collections.Generic;
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
        [SerializeField] private RectTransform _parent;
        
        [SerializeField] private TextMeshProUGUI _skillPointsText;
        [SerializeField] private Button _addSkillPointsButton;
        [SerializeField] private SelectedSkillPanel _selectedSkillPanel;

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
        
        private void OnCloseClicked()
        {
            CloseClicked?.Invoke();
            
            foreach (ISkillView view in Views.Values) 
                view.Destroy();
            
            Views.Clear();
        }

        private void OnAddSkillPointsClicked() => AddSkillPointClicked?.Invoke();
    }
}