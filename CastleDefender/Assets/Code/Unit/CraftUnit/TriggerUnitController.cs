using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using Code.GameBalance;
using Code.GameServices.Pool;
using UnityEngine;

namespace Code.Unit.CraftUnit
{
    public class TriggerUnitController
    {
        public float ResourcesCount;
        public ResourcesType ResourcesType;

        private bool _isCrafting;
        private bool _onStore;
        private bool _movingToStore;
        private Vector3 _craftingPosition;
        private readonly OnTriggerHandlingUnit _triggerHandling;
        private readonly MoveUnitController _moveUnitController;
        private readonly CraftDevelopment _craftDevelopment;
        private readonly MoveUnitView _moveUnitView;
        private readonly GameObject _unitGameObject;
        private readonly CraftUnitView _craftUnitView;
        private readonly IPoolServices _poolServices;

        public TriggerUnitController(
            IPoolServices poolServices,
            OnTriggerHandlingUnit triggerHandling,
            MoveUnitController move,
            MoveUnitView moveUnitView,
            CraftDevelopment craftDevelopment,
            GameObject unit,
            CraftUnitView craftUnitView)
        {
            _moveUnitController = move;
            _moveUnitView = moveUnitView;
            _craftDevelopment = craftDevelopment;
            _unitGameObject = unit;
            _poolServices = poolServices;
            _craftUnitView = craftUnitView;
            _craftUnitView.CraftFinish += CraftFinish;
            
            _triggerHandling = triggerHandling;
            _triggerHandling.CraftResoucesEnter += EnterResources;
            _triggerHandling.CraftResoucesExit += ExitResources;
            _triggerHandling.StoreBuildingEnter += EnterStore;
            _triggerHandling.StoreBuildingExit += ExitStore;

            _triggerHandling.BarracksBuildingEnter += EnterBarracks;
            _triggerHandling.BarracksBuildingExit += ExitBarracks;

            ResourcesType = ResourcesType.Unknow;
        }

        private void EnterBarracks(BarracksBuildingView barracksView)
        {
            barracksView.UnitToDefender++;
            _poolServices.Destroy(_unitGameObject);
            _moveUnitController.OnDestroy();
            OnDestroy();
        }

        private void ExitBarracks(BarracksBuildingView barracksView)
        {
        }

        private void EnterResources(CraftResourcesBuilding resources)
        {
            if (_isCrafting || _movingToStore)
            {
                return;
            }

            if (resources.ResourcesType != _moveUnitController.CraftResourcesType)
            {
                return;
            }

            _isCrafting = true;
            ResourcesType = resources.ResourcesType;
            _craftUnitView.StartCoroutineCraft(_craftDevelopment.CraftTime(ResourcesType));
        }

        private void ExitResources(CraftResourcesBuilding resources)
        {
            _isCrafting = false;
            if (!_movingToStore)
            {
                _craftUnitView.StopCoroutineCraft(_craftDevelopment.CraftTime(ResourcesType));
            }
        }

        private void EnterStore(StoreBuildingView store)
        {
            if (_onStore)
            {
                return;
            }

            if (_movingToStore)
            {
                _onStore = true;
                store.SetResources(ResourcesType, ResourcesCount);
                ResourcesCount = 0f;
                _moveUnitView.Move(_craftingPosition);
            }
        }

        private void ExitStore(StoreBuildingView store)
        {
            _onStore = false;
            _movingToStore = false;
        }

        private void CraftFinish()
        {
            ResourcesCount = _craftDevelopment.CraftCount(ResourcesType);
            _craftingPosition = _craftUnitView.transform.position;
            _movingToStore = true;
            _moveUnitController.MoveToStore();
        }

        private void OnDestroy()
        {
            _triggerHandling.CraftResoucesEnter -= EnterResources;
            _triggerHandling.CraftResoucesExit -= ExitResources;
            _triggerHandling.StoreBuildingEnter -= EnterStore;
            _triggerHandling.StoreBuildingExit -= ExitStore;
            _triggerHandling.BarracksBuildingEnter -= EnterBarracks;
            _triggerHandling.BarracksBuildingExit -= ExitBarracks;
        }
    }
}