using Code.Buildings.WallAndTowerBuildings;

namespace Code.Unit.DefenceUnit
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
            _triggerHandling.GateEnter += GateEnter;
            _triggerHandling.GateExit += GateExit;
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

        private void GateEnter(GateBuildingView gateView)
        {
            if (_moveView.NavMesh.remainingDistance < 1.5f)
            {
                if (gateView.IsAvailable())
                {
                    _moveView.NavMesh.enabled = false;
                    _moveView.transform.position = gateView.ChooseDefenderPosition();
                }
            }
        }

        private void WallExit(WallBuildingView wallView)
        {
            
        }

        private void TowerExit(TowerBuildingView towerView)
        {
            
        }

        private void GateExit(GateBuildingView gateView)
        {
            
        }
    }
}