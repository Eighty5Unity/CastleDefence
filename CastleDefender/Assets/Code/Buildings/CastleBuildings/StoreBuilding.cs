using Code.Architecture;
using Code.Buildings.ResourcesBuilgings;
using Code.Unit.CraftUnit;
using UnityEngine;

namespace Code.Buildings.CastleBuildings
{
    public class StoreBuilding : MonoBehaviour
    {
        public float Food;
        public float Wood;
        public float Stone;
        public float Iron;
        public float Money;

        public Transform StorePointPosition;
        // private float _coefFoodCost;

        public void UpdateResouces(CraftResourcesUnit craftResourcesUnit)
        {
            switch (craftResourcesUnit.ResourcesType)
            {
                case ResourcesType.Food:
                    Food += craftResourcesUnit.ResourcesCount;
                    break;
                case ResourcesType.Wood:
                    Wood += craftResourcesUnit.ResourcesCount;
                    break;
                case ResourcesType.Stone:
                    Stone += craftResourcesUnit.ResourcesCount;
                    break;
                case ResourcesType.Iron:
                    Iron += craftResourcesUnit.ResourcesCount;
                    break;
                default:
                    break;
            }
        }
    }
}