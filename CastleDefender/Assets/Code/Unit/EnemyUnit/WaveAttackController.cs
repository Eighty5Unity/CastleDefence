using Code.GameBalance;
using Code.GameServices;
using Code.UI;

namespace Code.Unit.EnemyUnit
{
    public class WaveAttackController
    {
        private readonly IGameFactory _factory;
        private readonly WaveUIView _waveAttackView;
        private SpawnEnemyPositions _spawnEnemy;

        public WaveAttackController(IGameFactory factory)
        {
            _factory = factory;
            _spawnEnemy = new SpawnEnemyPositions(_factory);
            
            _waveAttackView = _factory.WaveUIView;
            _waveAttackView.StartWave(WaveAttack.TimeToAttack, WaveAttack.CountEnemy);
            _waveAttackView.TimeIsOver += StartAttack;
        }

        private void StartAttack()
        {
            _spawnEnemy.SpawnEnemy(WaveAttack.CountEnemy);
            WaveAttack.CountEnemy += 10;
            _waveAttackView.StartWave(WaveAttack.TimeToAttack, WaveAttack.CountEnemy);
        }
    }
}