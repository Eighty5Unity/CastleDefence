using Code.Buildings;
using Code.Buildings.ResourcesBuilgings;
using Code.GameServices;
using Code.GameServices.InputService;
using Code.StaticData;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Unit
{
    public class MoveUnit : MonoBehaviour
    {
        public NavMeshAgent NavMesh;
        public ResourcesType CraftResourcesType;
        
        private Transform _buildingPoint;
        private ClickHandling _clickHandling;

        private Vector3 _craftFoodPoint;
        private Vector3 _craftWoodPoint;
        private Vector3 _craftStonePoint;
        private Vector3 _craftIronPoint;
        private Vector3 _storePoint;

        public void Counstructor(IStaticDataService staticData)
        {
            BuildingsStaticData points = staticData.GetBuildingsData();
            _craftFoodPoint = points.FoodCraftPoint;
            _craftWoodPoint = points.WoodCraftPoint;
            _craftStonePoint = points.StoneCraftPoint;
            _craftIronPoint = points.IronCraftPoint;
            _storePoint = points.StorePoint;

        }


        private void Start()
        {
            _clickHandling = GetComponent<ClickHandling>();
            _clickHandling.MoveHappend += ChooseBuildingToMove;
        }

        public void ChooseBuildingToMove(ClickHandling building)
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
            Move(_craftIronPoint);
            Debug.Log($"MoveToIron {_craftIronPoint}");
        }

        private void MoveToCraftStone()
        {
            CraftResourcesType = ResourcesType.Stone;
            Move(_craftStonePoint);
            Debug.Log("MoveToStone");
        }

        private void MoveToCraftWood()
        {
            CraftResourcesType = ResourcesType.Wood;
            Move(_craftWoodPoint);
            Debug.Log("MoveToWood");
        }

        private void MoveToCraftFood()
        {
            CraftResourcesType = ResourcesType.Food;
            Move(_craftFoodPoint);
            Debug.Log("MoveToFood");
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
            Move(_storePoint);
            Debug.Log("MoveToStore");
        }

        private void MoveToBarracks()
        {
            Debug.Log("MoveToBarracks");
        }

        private void MoveToSmithy()
        {
            Debug.Log("MoveToSmithy");
        }

        private void MoveToCastle()
        {
            Debug.Log("MoveToCastle");
        }

        public void Move(Vector3 position)
        {
            NavMesh.SetDestination(position);
        }

        private void OnDestroy()
        {
            _clickHandling.MoveHappend -= ChooseBuildingToMove;
        }
    }
}