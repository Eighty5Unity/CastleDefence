using System.Threading.Tasks;
using Code.GameServices;
using Code.GameServices.SaveLoadProgress;

namespace Code.Architecture.States
{
    public class LoadLevelState : IEnterNameState<string>
    {
        private readonly IStatesMachine _statesMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;

        public LoadLevelState(IStatesMachine statesMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IProgressService progress)
        {
            _statesMachine = statesMachine;
            _sceneLoader = sceneLoader;
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
            InitUICanvas();
            await InitGameWorld();
            InformProgressLoaders();
            InitUIResourcesView();

            _statesMachine.EnterState<GameLoopState>();
        }

        private async void InitUICanvas()
        {
            await _gameFactory.CreateUpUI();
            await _gameFactory.CreateDownUI();
        }

        private async Task InitGameWorld()
        {
            await _gameFactory.CreateBuildings();
            await _gameFactory.CreateWallAndTower();
            await _gameFactory.CreateResources();
        }

        private void InformProgressLoaders()
        {
            foreach (ILoadProgress loadProgress in _gameFactory.LoadProgress)
            {
                loadProgress.LoadProgress(_progressService.Progress);
            }
        }

        private void InitUIResourcesView()
        {
            _gameFactory.CreateUIResourcesView();
        }

        public void ExitState()
        {
            
        }
    }
}