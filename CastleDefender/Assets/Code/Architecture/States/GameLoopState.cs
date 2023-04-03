using Code.GameServices;
using Code.Unit.EnemyUnit;

namespace Code.Architecture.States
{
    public class GameLoopState : IEnterState
    {
        private readonly IGameFactory _factory;
        private WaveAttackController _waveAttackController;

        public GameLoopState(IGameFactory factory)
        {
            _factory = factory;
        }
        public void EnterState()
        {
            _waveAttackController = new WaveAttackController(_factory);
        }

        public void ExitState()
        {
            
        }
    }
}