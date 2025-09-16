using CodeBase.Data.Skills;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string MAIN_SCENE = "Main";
        
        [Inject] private readonly GameStateMachine _gameStateMachine;
        [Inject] private readonly PlayerSkillsModel _playerSkillsModel;

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(MAIN_SCENE);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            InitNewProgress();
        }

        private void InitNewProgress()
        {
            _playerSkillsModel.LearnedSkills.Add(SkillId.SkillBase);
        }
    }
}