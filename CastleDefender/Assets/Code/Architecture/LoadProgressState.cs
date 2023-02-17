namespace Code.Architecture
{
    public class LoadProgressState : IEnterState
    {
        private readonly IStatesMachine _stateMachine;

        public LoadProgressState(IStatesMachine statesMachine)
        {
            _stateMachine = statesMachine;
        }

        public void ExitState()
        {
            
        }

        public void EnterState()
        {
            
        }
    }
}