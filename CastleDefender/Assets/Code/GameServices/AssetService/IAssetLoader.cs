using System.Threading.Tasks;

namespace Code.GameServices.AssetService
{
    public interface IAssetLoader : IService
    {
        Task<T> Load<T>(string prefab) where T : class;
        void Cleanup();
    }
}