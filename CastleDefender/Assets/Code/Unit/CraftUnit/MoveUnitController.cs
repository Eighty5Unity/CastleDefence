using Code.Buildings;
using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using Code.Buildings.WallAndTowerBuildings;
using Code.GameServices.InputService;
using UnityEngine;

namespace Code.Unit.CraftUnit
{
    public class MoveUnitController
    {
        public ResourcesType CraftResourcesType;
        
        private readonly Vector3 _craftFoodPoint;
        private readonly Vector3 _craftWoodPoint;
        private readonly Vector3 _craftStonePoint;
        private readonly Vector3 _craftIronPoint;
        
        private readonly Vector3 _storePoint;
        private readonly Vector3 _barracksPoint;
        private readonly Vector3 _castlePoint;

        private readonly ClickHandling _clickHandling;
        private readonly MoveUnitView _moveUnitView;
        private readonly GateBuildingController _gateController;
        private bool _isGateOpen;

        public MoveUnitController(
            ClickHandling clickHandling, 
            MoveUnitView moveView,
            CastleBuildingView castleView,
            StoreBuildingView storeView,
            BarracksBuildingView barracksView,
            FoodBuildingView foodView,
            WoodBuildingView woodView,
            StoneBuildingView stoneView,
            IronBuildingView ironView,
            GateBuildingController gateController)
        {
            _craftFoodPoint = foodView.CraftPoint.position;
            _craftWoodPoint = woodView.CraftPoint.position;
            _craftStonePoint = stoneView.CraftPoint.position;
            _craftIronPoint = ironView.CraftPoint.position;
            
            _storePoint = storeView.StorePointPosition.position;
            _barracksPoint = barracksView.SpawnDefenderPoint.position;
            _castlePoint = castleView.SpawnUnitPoint.position;

            _clickHandling = clickHandling;
            _clickHandling.MoveHappend += ChooseBuildingToMove;

            _gateController = gateController;
            _gateController.GateOpen += GateOpen;

            _moveUnitView = moveView;
        }

        private void GateOpen(bool result)
        {
            if (result)
            {
                _moveUnitView.Resume();
            }
            else if (!result)
            {
                _moveUnitView.Stop();
            }
        }

        private void ChooseBuildingToMove(ClickHandling building)
        {
            switch (building.BuildingType)
            {
                case BuildingType.Castle:
                    MoveToCastle();
                    break;
                case BuildingType.Smithy:
                    MoveToSmithy();
                    break;
                case BuildingType.Barracks:
                    MoveToBarracks();
                    break;
                case BuildingType.Store:
                    MoveToStore();
                    break;
                case BuildingType.Tower:
                    MoveToTower(building);
                    break;
                case BuildingType.Wall:
                    MoveToWall(building);
                    break;
                case BuildingType.Food:
                    MoveToCraftFood();
                    break;
                case BuildingType.Wood:
                    MoveToCraftWood();
                    break;
                case BuildingType.Stone:
                    MoveToCraftStone();
                    break;
                case BuildingType.Iron:
                    MoveToCraftIron();
                    break;
                default:
                    break;
            }
        }
        
        private void MoveToCraftIron()
        {
            CraftResourcesType = ResourcesType.Iron;
            _moveUnitView.Move(_craftIronPoint);
        }

        private void MoveToCraftStone()
        {
            CraftResourcesType = ResourcesType.Stone;
            _moveUnitView.Move(_craftStonePoint);
        }

        private void MoveToCraftWood()
        {
            CraftResourcesType = ResourcesType.Wood;
            _moveUnitView.Move(_craftWoodPoint);
        }

        private void MoveToCraftFood()
        {
            CraftResourcesType = ResourcesType.Food;
            _moveUnitView.Move(_craftFoodPoint);
        }

        private void MoveToWall(ClickHandling building)
        {
            Debug.Log($"MoveToWall {building.transform.position}");
        }

        private void MoveToTower(ClickHandling building)
        {
            Debug.Log($"MoveToTower {building.transform.position}");
        }

        public void MoveToStore()
        {
            _moveUnitView.Move(_storePoint);
        }

        private void MoveToBarracks()
        {
            _moveUnitView.Move(_barracksPoint);
        }

        private void MoveToSmithy()
        {
            Debug.Log("MoveToSmithy");
        }

        private void MoveToCastle()
        {
            Debug.Log("MoveToCastle");
        }

        public void OnDestroy()
        {
            _clickHandling.MoveHappend -= ChooseBuildingToMove;
            _gateController.GateOpen -= GateOpen;
        }
    }
}