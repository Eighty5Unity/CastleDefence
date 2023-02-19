namespace Code.Architecture.States
{
    public class LoadProgressState : IEnterState
    {
        private readonly IStatesMachine _stateMachine;

        public LoadProgressState(IStatesMachine statesMachine)
        {
            _stateMachine = statesMachine;
        }

        public void EnterState()
        {
            _stateMachine.EnterState<LoadLevelState, string>("GameLevel");
        }

        public void ExitState()
        {
            
        }
    }
}