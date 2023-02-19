namespace Code.Architecture.States
{
    public interface IEnterNameState<TNameScene> : IState
    {
        void EnterState(TNameScene nameScene);
    }
}