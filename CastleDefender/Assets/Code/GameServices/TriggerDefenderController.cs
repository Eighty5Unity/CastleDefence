using Code.Buildings.WallAndTowerBuildings;
using Code.Unit;
using Code.Unit.DefenceUnit;
using UnityEngine;

namespace Code.GameServices
{
    public class TriggerDefenderController
    {
        private readonly OnTriggerHandlingDefender _triggerHandling;
        private readonly MoveUnitView _moveView;

        public TriggerDefenderController(OnTriggerHandlingDefender triggerHandling, MoveUnitView moveView)
        {
            _moveView = moveView;
            _triggerHandling = triggerHandling;
            _triggerHandling.WallEnter += WallEnter;
            _triggerHandling.WallExit += WallExit;
            _triggerHandling.TowerEnter += TowerEnter;
            _triggerHandling.TowerExit += TowerExit;
        }

        private void WallEnter(WallBuildingView wallView)
        {
            if (_moveView.NavMesh.remainingDistance < 1.5f)
            {
                if(wallView.IsAvailable())
                {
                    _moveView.NavMesh.enabled = false;
                    _moveView.transform.position = wallView.ChooseDefenderPosition();
                }
            }
        }

        private void TowerEnter(TowerBuildingView towerView)
        {
            if (_moveView.NavMesh.remainingDistance < 1.5f)
            {
                if (towerView.IsAvailable())
                {
                    _moveView.NavMesh.enabled = false;
                    _moveView.transform.position = towerView.ChooseDefenderPosition();
                }
            }
        }

        private void WallExit(WallBuildingView wallView)
        {
            
        }

        private void TowerExit(TowerBuildingView towerView)
        {
            
        }
    }
}