using CodeBase.Data.Skills;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.SkillsData;
using CodeBase.UI.Services.UIController;
using CodeBase.UI.Windows.Views;
using CodeBase.UseCases.SkillTree;
using UniRx;
using VContainer;

namespace CodeBase.UI.Windows.Presenters
{
    public class SkillsTreePresenter : LayoutPresenterBase<ISkillsTreeScreenView>, ISkillsTreePresenter
    {
        [Inject] private readonly IStaticDataService _staticDataService;
        [Inject] private readonly PlayerSkillsModel _playerSkills;
        [Inject] private readonly SkillTreeModel _skillTreeModel;
        [Inject] private readonly SkillTreeUseCase _skillTreeUseCase;
        
        private readonly CompositeDisposable _disposable = new();
        
        public override WindowId WindowId => WindowId.SkillsTreeScreen;

        private ISelectedSkillPanel SelectedSkillPanel => LayoutView.SelectedSkillPanel;
        
        public override void Initialize()
        {
            base.Initialize();

            LayoutView.CloseClicked += CloseScreen;
            LayoutView.AddSkillPointClicked += OnAddSkillPointClicked;
            
            _playerSkills.SkillPoints.Subscribe(SkillPointsChanged).AddTo(_disposable);
            _skillTreeModel.Filled.Subscribe(OnSkillTreeModelFilled).AddTo(_disposable);
            
        }

        public override void Dispose()
        {
            base.Dispose();
            _disposable.Dispose();
            
            LayoutView.CloseClicked -= CloseScreen;
            LayoutView.AddSkillPointClicked -= OnAddSkillPointClicked;
        }

        private void OnSkillTreeModelFilled(Unit _)
        {
            LayoutView.DrawTree(_staticDataService.SkillTreeConfig.SkillGraph);
            SelectedSkillPanel.Hide();
            UpdateSkillViews();
            
            SelectedSkillPanel.CloseClicked += OnPanelClosed;
            SelectedSkillPanel.LearnClicked += OnLearnSkill;
            SelectedSkillPanel.ForgetClicked += OnForgetSkill;
            SelectedSkillPanel.ForgetAllClicked += OnForgetAllSkills;

            foreach (var skillView in LayoutView.Views.Values) 
                skillView.Clicked += OnSkillClicked;
        }
        
        private void SkillPointsChanged(int newValue)
        {
            LayoutView.SetSkillPointsText($"Skill Points: {newValue}");
        }
        
        private void CloseScreen()
        {
            SelectedSkillPanel.CloseClicked -= OnPanelClosed;
            SelectedSkillPanel.LearnClicked -= OnLearnSkill;
            SelectedSkillPanel.ForgetClicked -= OnForgetSkill;
            SelectedSkillPanel.ForgetAllClicked -= OnForgetAllSkills;

            foreach (var skillView in LayoutView.Views.Values) 
                skillView.Clicked -= OnSkillClicked;
            
            Controller.Close(WindowId);
        }
        
        private void OnAddSkillPointClicked()
        {
            _skillTreeUseCase.HandleAddSkillPointsClicked();
            OnSkillClicked(SelectedSkillPanel.SelectedSkillId);
        }

        private void OnLearnSkill(SkillId skillId)
        {
            _skillTreeUseCase.LearnSkill(skillId);
            OnSkillClicked(skillId);
            UpdateSkillViews();
        }

        private void OnForgetSkill(SkillId skillId)
        {
            _skillTreeUseCase.ForgetSkill(skillId);
            OnSkillClicked(skillId);
            UpdateSkillViews();
        }

        private void OnForgetAllSkills(SkillId skillId)
        {
            _skillTreeUseCase.ForgetAllSkills();
            OnSkillClicked(skillId);
            UpdateSkillViews();
        }

        private void OnPanelClosed(SkillId selectedSkillId)
        {
            UnSelectAllSkills();
            SelectedSkillPanel.Hide();
        }

        private void OnSkillClicked(SkillId selectedSkillId)
        {
            UnSelectAllSkills();

            LayoutView.Views[selectedSkillId].SetSelected(true);
            SelectedSkillPanel.Show();

            var skillConfig = _staticDataService.GetConfigFor(selectedSkillId);
            string nameText = $"Skill Name: {skillConfig.Name}";
            string costText = $"Cost: {skillConfig.Cost.ToString()}";
            bool canBeLearned = CanLearnSkill(selectedSkillId, skillConfig);
            bool canBeForgot = CanForgetSkill(selectedSkillId);
            SelectedSkillPanel.SetData(selectedSkillId, canBeLearned, canBeForgot, nameText, costText);
        }

        private void UnSelectAllSkills()
        {
            foreach (var skillView in LayoutView.Views.Values)
                skillView.SetSelected(false);
        }

        private void UpdateSkillViews()
        {
            foreach (var kvp in LayoutView.Views) 
                kvp.Value.SetLearned(_skillTreeModel.IsLearned(kvp.Key));
        }
        
        private bool CanLearnSkill(SkillId selectedSkillId, SkillConfig skillConfig)
        {
            return _skillTreeModel.CanOpen(selectedSkillId) && _playerSkills.SkillPoints.Value >= skillConfig.Cost;
        }
        
        private bool CanForgetSkill(SkillId selectedSkillId)
        {
            return _skillTreeModel.CanClose(selectedSkillId);
        }
    }
}