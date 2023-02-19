using Code.StaticData;
using UnityEngine;

namespace Code.GameServices
{
    public class StaticDataService : IStaticDataService
    {
        private BuildingsStaticData _buildingsData;
        
        public void LoadBuildings()
        {
            _buildingsData = Resources.Load<BuildingsStaticData>("StaticData/Buildings");
            Debug.Log($"{_buildingsData.IronCraftPoint}");
        }

        public BuildingsStaticData GetBuildingsData()
        {
            return _buildingsData;
        }
    }
}