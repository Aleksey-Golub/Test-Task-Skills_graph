using System;
using CodeBase.Data.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Views
{
    public class SelectedSkillPanel : MonoBehaviour, ISelectedSkillPanel 
    {
        [SerializeField] private TextMeshProUGUI NameText;
        [SerializeField] private TextMeshProUGUI CostText;
        [SerializeField] private Button CloseButton;
        [SerializeField] private Button LearnButton;
        [SerializeField] private Button ForgetButton;
        [SerializeField] private Button ForgetAllButton;

        public SkillId SelectedSkillId { get; private set; }

        public event Action<SkillId> CloseClicked;
        public event Action<SkillId> LearnClicked;
        public event Action<SkillId> ForgetClicked;
        public event Action<SkillId> ForgetAllClicked;
        
        private void Awake()
        {
            CloseButton.onClick.AddListener(OnCloseClicked);
            LearnButton.onClick.AddListener(OnLearnClicked);
            ForgetButton.onClick.AddListener(OnForgetClicked);
            ForgetAllButton.onClick.AddListener(OnForgetAllClicked);
        }

        private void OnDestroy()
        {
            CloseButton.onClick.RemoveListener(OnCloseClicked);
            LearnButton.onClick.RemoveListener(OnLearnClicked);
            ForgetButton.onClick.RemoveListener(OnForgetClicked);
            ForgetAllButton.onClick.RemoveListener(OnForgetAllClicked);
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        
        public void SetData(SkillId skillId, bool canBeLearned, bool canBeForgot, string nameText, string costText)
        {
            SelectedSkillId = skillId;
            LearnButton.interactable = canBeLearned;
            ForgetButton.interactable = canBeForgot;
            NameText.text = nameText;
            CostText.text = costText;
        }

        private void OnCloseClicked() => CloseClicked?.Invoke(SelectedSkillId);
        private void OnLearnClicked() => LearnClicked?.Invoke(SelectedSkillId);
        private void OnForgetClicked() => ForgetClicked?.Invoke(SelectedSkillId);
        private void OnForgetAllClicked() => ForgetAllClicked?.Invoke(SelectedSkillId);
    }
}