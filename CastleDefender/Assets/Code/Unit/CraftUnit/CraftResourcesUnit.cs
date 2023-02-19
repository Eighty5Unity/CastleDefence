using System.Collections;
using Code.Architecture;
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
        private bool _moveToStore;

        private void Start()
        {
            _moveUnit = GetComponent<MoveUnit>();
            
            _triggerHandling = GetComponent<OnTriggerHandlingUnit>();
            _triggerHandling.CraftResoucesEnter += EnterResources;
            _triggerHandling.CraftResoucesExit += ExitResources;
            _triggerHandling.StoreBuildingEnter += EnterStore;
            _triggerHandling.StoreBuildingExit += ExitStore;
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
            StopCoroutine(Craft());
        }

        private void EnterStore(StoreBuilding store)
        {
            if (_onStore && !_movingToStore)
            {
                return;
            }

            _onStore = true;
            store.UpdateResouces(this);
            ResourcesCount = 0f;
            _moveUnit.Move(_craftingPosition);
        }

        private void ExitStore(StoreBuilding store)
        {
            _onStore = false;
            _movingToStore = false;
        }

        private IEnumerator Craft()
        {
            yield return new WaitForSeconds(5f);
            
            ResourcesCount = 10f;
            _craftingPosition = transform.position;
            _moveToStore = true;
            _moveUnit.MoveToStore();
        }
    }
}