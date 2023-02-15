namespace Code.Architecture
{
    public class GameStarter
    {
        public IStatesMachine StateMachine;
        
        private SceneLoader _sceneLoader;

        public GameStarter(ICoroutineRunner coroutineRunner)
        {
            _sceneLoader = new SceneLoader(coroutineRunner);
            StateMachine = new StatesMachine(_sceneLoader, AllServicesSingleton.Container);
        }
    }
}