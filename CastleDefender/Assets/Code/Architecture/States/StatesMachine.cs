using System;
using System.Collections.Generic;
using Code.GameServices;
using Code.GameServices.SaveLoadProgress;

namespace Code.Architecture.States
{
    public class StatesMachine : IStatesMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;
        
        public StatesMachine(SceneLoader sceneLoader, AllServicesSingleton services)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(RegisterServicesState)] = new RegisterServicesState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.GetService<IProgressService>(), services.GetService<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.GetService<IStaticDataService>(), services.GetService<IGameFactory>(), services.GetService<IProgressService>()),
                [typeof(GameLoopState)] = new GameLoopState(),
            };
        }

        public void EnterState<TState>() where TState : class, IEnterState
        {
            TState state = ChangeState<TState>();
            state.EnterState();
        }

        public void EnterState<TState, TNameScene>(TNameScene nameScene) where TState : class, IEnterNameState<TNameScene>
        {
            TState state = ChangeState<TState>();
            state.EnterState(nameScene);
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.ExitState();
            TState state = _states[typeof(TState)] as TState;
            _activeState = state;
            return state;
        }
    }
}