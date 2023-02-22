using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Buildings;
using Code.Buildings.CastleBuildings;
using Code.GameServices.AssetService;
using Code.GameServices.InputService;
using Code.GameServices.SaveLoadProgress;
using Code.StaticData;
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
        
        private Transform _canvasUp;
        private Transform _canvasDown;

        private CastleBuildingView _castleBuildingView;
        private CastleBuildingController _castleController;

        private StoreBuildingView _storeBuildingView;
        private StoreBuildingController _storeController;

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
            
            await _assetLoader.Load<GameObject>(AssetAddress.UI_UP_CANVAS);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CANVAS);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_UP_CONTAINER);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CONTAINER);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);

            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_STORE);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_CASTLE);
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
            // await CreateBarracks();
        }

        private async Task CreateCastle()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.CASTLE);
            GameObject castle = Object.Instantiate(prefab, BuildingsPositionInWorld.CastlePosition, Quaternion.identity);
            RegisterProgress(castle);
            _castleBuildingView = castle.GetComponent<CastleBuildingView>();
            ClickHandling clickHandling = castle.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_CASTLE);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;

            GameObject buttonCreateUnitPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonCreateUnit = Object.Instantiate(buttonCreateUnitPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonCreateUnitView = buttonCreateUnit.GetComponent<DownUIButtonView>();
            
            foreach (UIButtonInfo buttonInfo in prefabStaticData.Buttons)
            {
                if (buttonInfo.ButtonName == "CreateUnit")
                {
                    buttonCreateUnitView.ButtonName.text = buttonInfo.ButtonName;
                    buttonCreateUnitView.Icon.sprite = buttonInfo.ButtonIcon;
                }
            }
            
            // buttonCreateUnit.transform.parent = infoView.ButtonsPosition;
            
            downPanelView.SetActive(false);
            
            _castleController = new CastleBuildingController(this, _castleBuildingView, downPanelView, clickHandling, buttonCreateUnitView.Button);
        }

        private async Task CreateStore()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.STORE);
            GameObject store = Object.Instantiate(prefab, BuildingsPositionInWorld.StorePosition, Quaternion.identity);
            RegisterProgress(store);
            _storeBuildingView = store.GetComponent<StoreBuildingView>();
            ClickHandling clickHandling = store.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_STORE);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;
            downPanelView.SetActive(false);

            _storeController = new StoreBuildingController(_storeBuildingView, downPanelView, clickHandling);
        }

        private async Task CreateBarracks()
        {
        }

        public async Task CreateUpUI()
        {
            GameObject prefabUp = await _assetLoader.Load<GameObject>(AssetAddress.UI_UP_CANVAS);
            GameObject canvasUp = Object.Instantiate(prefabUp);
            _canvasUp = canvasUp.transform;
        }

        public async Task CreateDownUI()
        {
            GameObject prefabDown = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CANVAS);
            GameObject canvasDown = Object.Instantiate(prefabDown);
            _canvasDown = canvasDown.transform;
        }

        public async void CreateUIResourcesView()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_UP_CONTAINER);
            GameObject uiResourcesView = Object.Instantiate(prefab, _canvasUp);
            RegisterProgress(uiResourcesView);

            ResourcesUICount resourcesUICount = uiResourcesView.GetComponent<ResourcesUICount>();

            resourcesUICount.Constructor(_storeBuildingView);
        }

        public async Task<GameObject> CreateUIDownView()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CONTAINER);
            GameObject uiDownView = Object.Instantiate(prefab);
            return uiDownView;
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