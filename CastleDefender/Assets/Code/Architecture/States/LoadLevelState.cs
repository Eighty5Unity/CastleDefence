using System.Threading.Tasks;
using Code.Buildings.CastleBuildings;
using Code.GameServices;

namespace Code.Architecture.States
{
    public class LoadLevelState : IEnterNameState<string>
    {
        private readonly IStatesMachine _statesMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(IStatesMachine statesMachine, SceneLoader sceneLoader, IStaticDataService staticData, IGameFactory gameFactory)
        {
            _statesMachine = statesMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _gameFactory = gameFactory;
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
            _statesMachine.EnterState<GameLoopState>();
        }

        private async Task InitGameWorld()
        {
            await _gameFactory.CreateUnit(_staticData.GetBuildingsData().CastleSpawnPoinUnit);
        }

        public void ExitState()
        {
            
        }
    }
}