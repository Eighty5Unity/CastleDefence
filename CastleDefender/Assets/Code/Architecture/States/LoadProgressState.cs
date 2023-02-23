using Code.GameServices.SaveLoadProgress;

namespace Code.Architecture.States
{
    public class LoadProgressState : IEnterState
    {
        private const string LEVEL_NAME = "GameLevel";
        private readonly IStatesMachine _stateMachine;
        private readonly IProgressService _progress;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IStatesMachine statesMachine, IProgressService progress, ISaveLoadService saveLoadService)
        {
            _stateMachine = statesMachine;
            _progress = progress;
            _saveLoadService = saveLoadService;
        }

        public  void EnterState()
        {
            LoadProgress();
            _stateMachine.EnterState<LoadLevelState, string>(LEVEL_NAME);
        }

        public void ExitState()
        {
            
        }

        private void LoadProgress()
        {
            _progress.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private GameProgress NewProgress()
        {
            return new GameProgress();
        }
    }
}