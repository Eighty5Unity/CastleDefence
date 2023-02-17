namespace Code.Architecture
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
            
        }

        private void EnterLoadLevel()
        {
            _statesMachine.EnterState<LoadProgressState>();
        }
    }
}