namespace Code.Architecture
{
    public class RegisterServicesState : IEnterState
    {
        private readonly IStatesMachine _statesMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServicesSingleton _services;

        public RegisterServicesState(IStatesMachine statesMachine, SceneLoader sceneLoader, AllServicesSingleton services)
        {
            _statesMachine = statesMachine;
            _sceneLoader = sceneLoader;
            _services = services;
        }

        public void EnterState()
        {
            
        }

        public void ExitState()
        {
            
        }
    }
}