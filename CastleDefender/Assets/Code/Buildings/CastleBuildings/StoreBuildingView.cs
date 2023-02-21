using System;
using Code.Buildings.ResourcesBuilgings;
using UnityEngine;

namespace Code.Buildings.CastleBuildings
{
    public class StoreBuildingView : MonoBehaviour
    {
        public event Action<ResourcesType, float> RefreshResources;
        
        public Transform StorePointPosition;
        
        public void SetResources(ResourcesType type, float count)
        {
            RefreshResources?.Invoke(type, count);
        }
    }
}