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
        void CreateDefender();
        void CreateEnemy();
        Task LoadAddressableAssets();
        void Cleanup();
        Task CreateUnit(Vector3 at);
        Task CreateBuildings();
        Task CreateWallAndTower();
        Task CreateResources();
        Task CreateUpUI();
        Task CreateDownUI();
        void CreateUIResourcesView();
        Task<GameObject> CreateUIDownView();
    }
}