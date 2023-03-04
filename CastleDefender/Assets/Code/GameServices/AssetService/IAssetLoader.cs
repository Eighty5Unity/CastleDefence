using System.Threading.Tasks;

namespace Code.GameServices.AssetService
{
    public interface IAssetLoader : IService
    {
        void Initialize();
        Task<T> LoadBuildings<T>(string prefab) where T : class;
        Task<T> LoadUnits<T>(string prefab) where T : class;
        void CleanupBuildings();
        void CleanupUnits();
    }
}