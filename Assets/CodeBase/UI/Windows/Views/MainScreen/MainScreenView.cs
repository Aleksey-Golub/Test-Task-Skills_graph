using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Views
{
    public class MainScreenView : LayoutViewBase, IMainScreenView
    {
        [SerializeField] private Button OpenSkillsButton;
        
        public event Action OpenSkillsClicked;

        private void Awake()
        {
            OpenSkillsButton.onClick.AddListener(OnOpenClicked);
        }

        private void OnDestroy()
        {
            OpenSkillsButton.onClick.RemoveListener(OnOpenClicked);
        }

        private void OnOpenClicked()
        {
            OpenSkillsClicked?.Invoke();
        }
    }
}