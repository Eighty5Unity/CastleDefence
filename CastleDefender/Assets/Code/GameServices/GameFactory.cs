using System.Collections.Generic;
using System.Threading.Tasks;
using Code.GameServices.AssetService;
using Code.GameServices.InputService;
using Code.GameServices.SaveLoadProgress;
using Code.Unit;
using Code.Unit.CraftUnit;
using UnityEngine;

namespace Code.GameServices
{
    public class GameFactory : IGameFactory
    {
        public List<ISaveProgress> SaveProgress { get; } = new List<ISaveProgress>();
        public List<ILoadProgress> LoadProgress { get; } = new List<ILoadProgress>();
        
        private readonly IStaticDataService _staticData;
        private readonly IAssetLoader _assetLoader;

        public GameFactory(IStaticDataService staticData, IAssetLoader assetLoader)
        {
            _staticData = staticData;
            _assetLoader = assetLoader;
        }

        public async Task LoadAddressableAssets()
        {
            await _assetLoader.Load<GameObject>(AssetAddress.UNIT);
            await _assetLoader.Load<GameObject>(AssetAddress.STORE);
        }

        public async Task CreateUnit(Vector3 at)
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.UNIT);
            GameObject unit = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgress(unit);

            ClickHandling clickHandling = unit.GetComponentInChildren<ClickHandling>();
            OnTriggerHandlingUnit triggerHandling = unit.GetComponentInChildren<OnTriggerHandlingUnit>();
            
            MoveUnit move = unit.GetComponentInChildren<MoveUnit>();
            move.Counstructor(_staticData, clickHandling);

            CraftResourcesUnit craftResources = unit.GetComponentInChildren<CraftResourcesUnit>();
            craftResources.Constructor(triggerHandling, move);
        }

        public async Task CreateBuildings()
        {
            await CreateStore();
        }

        private async Task CreateStore()
        {
            Vector3 at = new Vector3(10, 0, 0);
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.STORE);
            GameObject store = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgress(store);
        }

        public void CreateDefender()
        {
            
        }

        public void CreateEnemy()
        {
            
        }

        public void Cleanup()
        {
            _assetLoader.Cleanup();
        }

        private void RegisterProgress(GameObject gameObject)
        {
            foreach (ISaveProgress save in gameObject.GetComponentsInChildren<ISaveProgress>())
            {
                SaveProgress.Add(save);
            }

            foreach (ILoadProgress load in gameObject.GetComponentsInChildren<ILoadProgress>())
            {
                LoadProgress.Add(load);
            }
        }
    }
}