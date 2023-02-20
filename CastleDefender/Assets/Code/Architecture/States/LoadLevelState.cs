using System.Threading.Tasks;
using Code.GameServices;
using Code.GameServices.SaveLoadProgress;

namespace Code.Architecture.States
{
    public class LoadLevelState : IEnterNameState<string>
    {
        private readonly IStatesMachine _statesMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;

        public LoadLevelState(IStatesMachine statesMachine, SceneLoader sceneLoader, IStaticDataService staticData, IGameFactory gameFactory, IProgressService progress)
        {
            _statesMachine = statesMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _gameFactory = gameFactory;
            _progressService = progress;
        }

        public void EnterState(string nameScene)
        {
            _gameFactory.Cleanup();
            _gameFactory.LoadAddressableAssets();
            _sceneLoader.Load(nameScene, OnLoaded);
        }

        private async void OnLoaded()
        {
            await InitGameWorld();
            await InitUI();
            InformProgressLoaders();
            _statesMachine.EnterState<GameLoopState>();
        }

        private void InformProgressLoaders()
        {
            foreach (ILoadProgress loadProgress in _gameFactory.LoadProgress)
            {
                loadProgress.LoadProgress(_progressService.Progress);
            }
        }

        private async Task InitGameWorld()
        {
            await _gameFactory.CreateBuildings();
            await _gameFactory.CreateUnit(_staticData.GetBuildingsData().CastleSpawnPoinUnit);
        }

        private async Task InitUI()
        {
            await _gameFactory.CreateUIDinamic();
            await _gameFactory.CreateUIResourcesView();
        }

        public void ExitState()
        {
            
        }
    }
}