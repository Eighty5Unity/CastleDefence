using System.Threading.Tasks;
using UnityEngine;

namespace Code.GameServices
{
    public interface IGameFactory : IService
    {
        void CreateDefender();
        void CreateEnemy();
        Task LoadAddressableAssets();
        void Cleanup();
        Task CreateUnit(Vector3 at);
    }
}