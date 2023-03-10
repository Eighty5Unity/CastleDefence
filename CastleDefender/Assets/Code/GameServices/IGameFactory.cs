using System.Collections.Generic;
using System.Threading.Tasks;
using Code.GameServices.SaveLoadProgress;
using UnityEngine;

namespace Code.GameServices
{
    public interface IGameFactory : IService
    {
        List<ISaveProgress> SaveProgress { get; }
        List<ILoadProgress> LoadProgress { get; }
        void CreateEnemy();
        Task LoadAddressableAssets();
        void Cleanup();
        void CleanupBuildings();
        Task<GameObject> CreateUnit(Vector3 at);
        Task<GameObject> CreateDefender(Vector3 at);
        Task CreateBuildings();
        Task CreateWallAndTower();
        Task CreateResources();
        Task CreateUpUI();
        Task CreateDownUI();
        void CreateUIResourcesView();
        Task<GameObject> CreateUIDownView();
    }
}