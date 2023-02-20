using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Buildings.CastleBuildings;
using Code.GameServices.AssetService;
using Code.GameServices.InputService;
using Code.GameServices.SaveLoadProgress;
using Code.UI;
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
        private Transform _uiDinamicRoot;
        private StoreBuilding _storeBuilding;

        public GameFactory(IStaticDataService staticData, IAssetLoader assetLoader)
        {
            _staticData = staticData;
            _assetLoader = assetLoader;
        }

        public async Task LoadAddressableAssets()
        {
            await _assetLoader.Load<GameObject>(AssetAddress.UNIT);
            await _assetLoader.Load<GameObject>(AssetAddress.STORE);
            await _assetLoader.Load<GameObject>(AssetAddress.CASTLE);
            
            await _assetLoader.Load<GameObject>(AssetAddress.UIDINAMIC);
            await _assetLoader.Load<GameObject>(AssetAddress.UIDINAMICUPCONTAINER);
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
            await CreateCastle();
        }

        public async Task CreateUIDinamic()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.UIDINAMIC);
            GameObject uiDinamic = Object.Instantiate(prefab);
            _uiDinamicRoot = uiDinamic.transform;
        }

        private async Task CreateStore()
        {
            Vector3 at = new Vector3(10, 0, 0);
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.STORE);
            GameObject store = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgress(store);
            
            _storeBuilding = store.GetComponent<StoreBuilding>();
        }

        private async Task CreateCastle()
        {
            Vector3 at = new Vector3(0, 0, -10);
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.CASTLE);
            GameObject castle = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgress(castle);

            CastleBuilding castleBuilding = castle.GetComponent<CastleBuilding>();
            castleBuilding.Constructor(this);
            castleBuilding.SpawnUnit = false;
        }

        public async Task CreateUIResourcesView()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.UIDINAMICUPCONTAINER);
            GameObject uiResourcesView = Object.Instantiate(prefab, _uiDinamicRoot);
            RegisterProgress(uiResourcesView);
            
            ResourcesUICount resourcesUICount = uiResourcesView.GetComponent<ResourcesUICount>();
            // resourcesUICount.Constructor(_storeBuilding);
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