using Code.Buildings.WallAndTowerBuildings;
using Code.Unit.DefenceUnit;
using UnityEngine;

namespace Code.Unit.EnemyUnit
{
    public class TriggerEnemyController
    {
        private readonly OnTriggerHandlingDefender _triggerHandling;
        private readonly MoveUnitView _moveView;

        public TriggerEnemyController(OnTriggerHandlingDefender triggerHandling, MoveUnitView moveView)
        {
            _moveView = moveView;
            _triggerHandling = triggerHandling;
            _triggerHandling.WallEnter += AttackWall;
            _triggerHandling.TowerEnter += AttackTower;
            _triggerHandling.GateEnter += AttackGate;
        }

        private void AttackGate(GateBuildingView obj)
        {
            _moveView.Stop();
            Debug.Log("Attacked Gate");
        }

        private void AttackTower(TowerBuildingView obj)
        {
            _moveView.Stop();
            Debug.Log("Attacked Tower");
        }

        private void AttackWall(WallBuildingView obj)
        {
            _moveView.Stop();
            Debug.Log("Attacked Wall");
        }
    }
}