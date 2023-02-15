namespace Code.Architecture
{
    public interface IStatesMachine : IService
    {
        void EnterState<TState>() where TState : class, IEnterState;
        void EnterState<TState, TNameScene>(TNameScene nameScene) where TState : class, IEnterNameState<TNameScene>;
    }
}