using System.Threading.Tasks;
using Code.GameServices;
using Code.GameServices.SaveLoadProgress;

namespace Code.Architecture.States
{
    public class LoadProgressState : IEnterState
    {
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
            _stateMachine.EnterState<LoadLevelState, string>("GameLevel");
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
            GameProgress progress = new GameProgress();
            
            ResourcesProgress resources = progress.ResourcesProgress;
            resources.Food = 0f;
            resources.Wood = 0f;
            resources.Stone = 0f;
            resources.Iron = 0f;
            resources.Money = 0f;
            
            return progress;
        }
    }
}