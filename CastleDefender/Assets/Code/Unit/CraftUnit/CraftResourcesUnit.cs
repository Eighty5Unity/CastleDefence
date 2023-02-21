using System.Collections;
using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
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

        public void Constructor(OnTriggerHandlingUnit triggerHandling, MoveUnit move)
        {
            _moveUnit = move;
            
            _triggerHandling = triggerHandling;
            _triggerHandling.CraftResoucesEnter += EnterResources;
            _triggerHandling.CraftResoucesExit += ExitResources;
            _triggerHandling.StoreBuildingEnter += EnterStore;
            _triggerHandling.StoreBuildingExit += ExitStore;

            ResourcesType = ResourcesType.Unknow;
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
            StartCoroutine(Craft());
        }

        private void ExitResources(CraftResourcesBuilding resources)
        {
            _isCrafting = false;
            if (!_movingToStore)
            {
                StopCoroutine(Craft());
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

        private IEnumerator Craft()
        {
            yield return new WaitForSeconds(5f);
            
            ResourcesCount = 10f;
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
        }
    }
}