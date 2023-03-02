using Code.GameServices;
using Code.GameServices.AssetService;
using Code.GameServices.SaveLoadProgress;

namespace Code.Architecture.States
{
    public class RegisterServicesState : IEnterState
    {
        private const string LEVEL_NAME = "GameStart";
        private readonly IStatesMachine _statesMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServicesSingleton _services;

        public RegisterServicesState(IStatesMachine statesMachine, SceneLoader sceneLoader, AllServicesSingleton services)
        {
            _statesMachine = statesMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void EnterState()
        {
            _sceneLoader.Load(LEVEL_NAME, onLoaded: EnterLoadLevel);
        }

        public void ExitState()
        {
            
        }

        private void RegisterServices()
        {
            RegisterAssetLoader();
            RegisterGameProgress();
            RegisterStateMachine();
            RegisterGameFactory();
            RegisterSaveLoad();
        }

        private void RegisterAssetLoader()
        {
            IAssetLoader assetLoader = new AssetLoader();
            assetLoader.Initialize();
            _services.RegisterService<IAssetLoader>(assetLoader);
        }

        private void RegisterGameProgress()
        {
            _services.RegisterService<IProgressService>(new ProgressService());
        }

        private void RegisterStateMachine()
        {
            _services.RegisterService<IStatesMachine>(_statesMachine);
        }

        private void RegisterGameFactory()
        {
            IGameFactory gameFactory = new GameFactory(_services.GetService<IAssetLoader>());
            _services.RegisterService<IGameFactory>(gameFactory);
        }

        private void RegisterSaveLoad()
        {
            _services.RegisterService<ISaveLoadService>(new SaveLoadService(_services.GetService<IProgressService>(),
                _services.GetService<IGameFactory>()));
        }

        private void EnterLoadLevel()
        {
            _statesMachine.EnterState<LoadProgressState>();
        }
    }
}