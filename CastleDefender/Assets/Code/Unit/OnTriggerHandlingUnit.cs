using System;
using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using UnityEngine;

namespace Code.Unit
{
    public class OnTriggerHandlingUnit : MonoBehaviour
    {
        public event Action<CraftResourcesBuilding> CraftResoucesEnter;
        public event Action<CraftResourcesBuilding> CraftResoucesExit;
        public event Action<StoreBuilding> StoreBuildingEnter;
        public event Action<StoreBuilding> StoreBuildingExit;
        
        private CraftResourcesBuilding _resourcesBuilding;
        private StoreBuilding _storeBuilding;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CraftResourcesBuilding>(out _resourcesBuilding))
            {
                CraftResoucesEnter?.Invoke(_resourcesBuilding);
            }
            else if (other.TryGetComponent<StoreBuilding>(out _storeBuilding))
            {
                StoreBuildingEnter?.Invoke(_storeBuilding);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CraftResourcesBuilding>(out _resourcesBuilding))
            {
                CraftResoucesExit?.Invoke(_resourcesBuilding);
            }
            else if (other.TryGetComponent<StoreBuilding>(out _storeBuilding))
            {
                StoreBuildingExit?.Invoke(_storeBuilding);
            }
        }
    }
}