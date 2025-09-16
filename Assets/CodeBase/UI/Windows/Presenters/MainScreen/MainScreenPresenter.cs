using CodeBase.UI.Services.UIController;
using CodeBase.UI.Windows.Views;
using CodeBase.UseCases.SkillTree;
using VContainer;

namespace CodeBase.UI.Windows.Presenters
{
    public class MainScreenPresenter : LayoutPresenterBase<IMainScreenView>, IMainScreenPresenter
    {
        [Inject] private readonly SkillTreeUseCase _skillTreeUseCase;
        
        public override WindowId WindowId => WindowId.MainScreen;

        public override void Initialize()
        {
            base.Initialize();

            LayoutView.OpenSkillsClicked += OnOpenSkillsClicked;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            LayoutView.OpenSkillsClicked -= OnOpenSkillsClicked;
        }

        private void OnOpenSkillsClicked()
        {
            _skillTreeUseCase.HandleOpenSkillsClicked();
        }
    }
}