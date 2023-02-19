using Code.StaticData;
using UnityEngine;

namespace Code.GameServices
{
    public class StaticDataService : IStaticDataService
    {
        private BuildingPointsStaticData _buildingPointsData;
        
        public void LoadBuildings()
        {
            _buildingPointsData = Resources.Load<BuildingPointsStaticData>("StaticData/Buildings");
        }

        public BuildingPointsStaticData GetBuildingsData()
        {
            return _buildingPointsData;
        }
    }
}