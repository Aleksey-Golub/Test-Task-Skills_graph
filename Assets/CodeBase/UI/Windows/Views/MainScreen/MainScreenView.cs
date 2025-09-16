using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Views
{
    public class MainScreenView : LayoutViewBase, IMainScreenView
    {
        [SerializeField] private Button _openSkillsButton;
        
        public event Action OpenSkillsClicked;

        private void Awake()
        {
            _openSkillsButton.onClick.AddListener(OnOpenClicked);
        }

        private void OnDestroy()
        {
            _openSkillsButton.onClick.RemoveListener(OnOpenClicked);
        }

        private void OnOpenClicked()
        {
            OpenSkillsClicked?.Invoke();
        }
    }
}