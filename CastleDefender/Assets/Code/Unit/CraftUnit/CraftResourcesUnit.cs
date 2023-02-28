using System.Collections;
using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using Code.GameBalance;
using UnityEngine;

namespace Code.Unit.CraftUnit
{
    public class CraftResourcesUnit : MonoBehaviour
    {
        public float ResourcesCount;
        public ResourcesType ResourcesType;

        private OnTriggerHandlingUnit _triggerHandling;
        private bool _isCrafting;
        private MoveUnit _moveUnit;
        private bool _onStore;
        private bool _movingToStore;
        private Vector3 _craftingPosition;
        private CraftDevelopment _craftDevelopment;

        public void Constructor(OnTriggerHandlingUnit triggerHandling, MoveUnit move, CraftDevelopment craftDevelopment)
        {
            _moveUnit = move;
            _craftDevelopment = craftDevelopment;
            
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
            Destroy(gameObject);
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

            if (resources.ResourcesType != _moveUnit.CraftResourcesType)
            {
                return;
            }
            _isCrafting = true;
            ResourcesType = resources.ResourcesType;
            StartCoroutine(Craft(ResourcesType));
        }

        private void ExitResources(CraftResourcesBuilding resources)
        {
            _isCrafting = false;
            if (!_movingToStore)
            {
                StopCoroutine(Craft(ResourcesType));
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
                _moveUnit.Move(_craftingPosition);
            }
        }

        private void ExitStore(StoreBuildingView store)
        {
            _onStore = false;
            _movingToStore = false;
        }

        private IEnumerator Craft(ResourcesType type)
        {
            
            yield return new WaitForSeconds(_craftDevelopment.CraftTime(type));
            
            ResourcesCount = _craftDevelopment.CraftCount(type);
            _craftingPosition = transform.position;
            _movingToStore = true;
            _moveUnit.MoveToStore();
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