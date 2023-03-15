namespace Code.Unit.EnemyUnit
{
    public class MoveEnemyController
    {
        private readonly MoveUnitView _moveView;
        private readonly EnemyAttackView _attackView;

        public MoveEnemyController(MoveUnitView moveView, EnemyAttackView attackView)
        {
            _moveView = moveView;
            _attackView = attackView;

            _attackView.FoundAttackBuilding += _moveView.Move;
        }
    }
}