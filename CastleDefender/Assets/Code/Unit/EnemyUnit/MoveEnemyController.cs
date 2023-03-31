using Code.Buildings.CastleBuildings;

namespace Code.Unit.EnemyUnit
{
    public class MoveEnemyController
    {
        private readonly MoveUnitView _moveView;

        public MoveEnemyController(MoveUnitView moveView, CastleBuildingView castleBuildingView)
        {
            _moveView = moveView;
            _moveView.Move(castleBuildingView.transform.position);
        }
    }
}