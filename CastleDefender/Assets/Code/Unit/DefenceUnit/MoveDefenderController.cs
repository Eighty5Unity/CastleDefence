using Code.Buildings;
using Code.Buildings.WallAndTowerBuildings;
using Code.GameServices.InputService;
using UnityEngine;

namespace Code.Unit.DefenceUnit
{
    public class MoveDefenderController
    {
        private readonly ClickHandling _clickHandling;
        private readonly MoveUnitView _moveUnitView;

        public MoveDefenderController(ClickHandling clickHandling, MoveUnitView moveUnitView)
        {
            _moveUnitView = moveUnitView;
            _clickHandling = clickHandling;
            _clickHandling.MoveHappend += MoveToDefence;
        }

        private void MoveToDefence(ClickHandling building)
        {
            if (building.BuildingType == BuildingType.Tower)
            {
                Vector3 moveTo = building.transform.parent.GetComponent<TowerBuildingView>().DefenderMovePoint.position;;
                _moveUnitView.Move(moveTo);
            }
            else if (building.BuildingType == BuildingType.Wall)
            {
                Vector3 moveTo = building.transform.parent.GetComponent<WallBuildingView>().DefenderMovePoint.position;
                _moveUnitView.Move(moveTo);
            }
            else if (building.BuildingType == BuildingType.Gate)
            {
                Vector3 moveTo = building.transform.parent.GetComponent<GateBuildingView>().DefenderMovePoint.position;
                _moveUnitView.Move(moveTo);
            }
        }
    }
}