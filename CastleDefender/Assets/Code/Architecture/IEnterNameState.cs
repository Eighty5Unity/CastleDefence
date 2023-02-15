namespace Code.Architecture
{
    public interface IEnterNameState<TNameScene> : IState
    {
        void EnterState(TNameScene nameScene);
    }
}