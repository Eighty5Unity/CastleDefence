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
        public event Action<StoreBuildingView> StoreBuildingEnter;
        public event Action<StoreBuildingView> StoreBuildingExit;
        public event Action<BarracksBuildingView> BarracksBuildingEnter; 
        public event Action<BarracksBuildingView> BarracksBuildingExit;

        private CraftResourcesBuilding _resourcesBuilding;
        private StoreBuildingView _storeBuildingView;
        private BarracksBuildingView _barracksBuildingView;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CraftResourcesBuilding>(out _resourcesBuilding))
            {
                CraftResoucesEnter?.Invoke(_resourcesBuilding);
            }
            else if (other.TryGetComponent<StoreBuildingView>(out _storeBuildingView))
            {
                StoreBuildingEnter?.Invoke(_storeBuildingView);
            }
            else if (other.TryGetComponent<BarracksBuildingView>(out _barracksBuildingView))
            {
                BarracksBuildingEnter?.Invoke(_barracksBuildingView);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CraftResourcesBuilding>(out _resourcesBuilding))
            {
                CraftResoucesExit?.Invoke(_resourcesBuilding);
            }
            else if (other.TryGetComponent<StoreBuildingView>(out _storeBuildingView))
            {
                StoreBuildingExit?.Invoke(_storeBuildingView);
            }
            else if (other.TryGetComponent<BarracksBuildingView>(out _barracksBuildingView))
            {
                BarracksBuildingExit?.Invoke(_barracksBuildingView);
            }
        }
    }
}