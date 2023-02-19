using Code.GameServices;
using Code.GameServices.AssetService;
using UnityEngine;

namespace Code.Architecture.States
{
    public class RegisterServicesState : IEnterState
    {
        private const string GAME_START_SCENE = "GameStart";
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
            _sceneLoader.Load(GAME_START_SCENE, onLoaded: EnterLoadLevel);
        }

        public void ExitState()
        {
            
        }

        private void RegisterServices()
        {
            RegisterAndLoadStaticData();
            
            _services.RegisterService<IStatesMachine>(_statesMachine);
            _services.RegisterService<IAssetLoader>(new AssetLoader());
            _services.RegisterService<IGameFactory>(new GameFactory(_services.GetService<IStaticDataService>(), _services.GetService<IAssetLoader>()));
        }

        private void RegisterAndLoadStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadBuildings();
            _services.RegisterService<IStaticDataService>(staticData);
        }

        private void EnterLoadLevel()
        {
            _statesMachine.EnterState<LoadProgressState>();
        }
    }
}