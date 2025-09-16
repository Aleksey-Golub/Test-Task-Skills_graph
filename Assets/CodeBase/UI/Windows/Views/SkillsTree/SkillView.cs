using System;
using CodeBase.Data.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Views
{
    public class SkillView : MonoBehaviour, ISkillView
    {
        [SerializeField] private Color _learnedColor;
        [SerializeField] private Color _defaultColor;
        
        [SerializeField] private Image _image;
        [SerializeField] private Image _border;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Button _button;
        
        private SkillId _skillId;

        public event Action<SkillId> Clicked;

        private void Awake()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void Init(SkillId skillId, string name)
        {
            _skillId = skillId;
            _nameText.text = name;
        }

        public void Destroy() => Destroy(gameObject);
        public void SetSelected(bool isSelected) => _border.enabled = isSelected;
        public void SetLearned(bool isLearned) => _image.color = isLearned ? _learnedColor : _defaultColor;

        private void OnClicked() => Clicked?.Invoke(_skillId);
    }
}