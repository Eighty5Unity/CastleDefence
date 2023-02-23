using Code.Architecture.States;
using UnityEngine;

namespace Code.Architecture
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private GameStarter _gameStarter;
        private void Awake()
        {
            _gameStarter = new GameStarter(this);
            _gameStarter.StateMachine.EnterState<RegisterServicesState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
