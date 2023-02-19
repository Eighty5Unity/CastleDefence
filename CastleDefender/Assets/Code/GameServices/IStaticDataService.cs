using Code.StaticData;

namespace Code.GameServices
{
    public interface IStaticDataService : IService
    {
        void LoadBuildings();
        BuildingPointsStaticData GetBuildingsData();
    }
}