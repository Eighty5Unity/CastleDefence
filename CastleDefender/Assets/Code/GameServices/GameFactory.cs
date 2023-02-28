using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Buildings;
using Code.Buildings.CastleBuildings;
using Code.GameBalance;
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
        
        private BarracksBuildingView _barracksBuildingView;
        private BarracksBuildingController _barracksController;

        private SmithyController _smithyController;
        
        private readonly ResourcesCount _resourcesCount;
        private readonly CraftDevelopment _craftDevelopment;

        public GameFactory(IStaticDataService staticData, IAssetLoader assetLoader)
        {
            _staticData = staticData;
            _assetLoader = assetLoader;

            _resourcesCount = new ResourcesCount();//TODO register save/load
            _craftDevelopment = new CraftDevelopment();//TODO register save/load
        }

        public async Task LoadAddressableAssets()
        {
            await _assetLoader.Load<GameObject>(AssetAddress.UNIT);
            await _assetLoader.Load<GameObject>(AssetAddress.STORE);
            await _assetLoader.Load<GameObject>(AssetAddress.CASTLE);
            await _assetLoader.Load<GameObject>(AssetAddress.BARRACKS);
            await _assetLoader.Load<GameObject>(AssetAddress.SMITHY);
            
            await _assetLoader.Load<GameObject>(AssetAddress.UI_UP_CANVAS);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CANVAS);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_UP_CONTAINER);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CONTAINER);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);

            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_STORE);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_CASTLE);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_BARRACKS);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_SMITHY);
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
            craftResources.Constructor(triggerHandling, move, _craftDevelopment);
        }

        public async Task CreateBuildings()
        {
            await CreateStore();
            await CreateCastle();
            await CreateBarracks();
            await CreateSmithy();
        }

        private async Task CreateCastle()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.CASTLE);
            GameObject castle = Object.Instantiate(prefab, BuildingsPositionInWorld.CastlePosition, Quaternion.identity);
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
            
            downPanelView.SetActive(false);
            
            _castleController = new CastleBuildingController(this, _castleBuildingView, downPanelView, clickHandling, buttonCreateUnitView.Button, _resourcesCount);
        }

        private async Task CreateStore()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.STORE);
            GameObject store = Object.Instantiate(prefab, BuildingsPositionInWorld.StorePosition, Quaternion.identity);
            _storeBuildingView = store.GetComponent<StoreBuildingView>();
            ClickHandling clickHandling = store.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_STORE);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;
            
            GameObject buttonSellFoodPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonSellFood = Object.Instantiate(buttonSellFoodPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonSellFoodView = buttonSellFood.GetComponent<DownUIButtonView>();
            
            GameObject buttonSellWoodPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonSellWood = Object.Instantiate(buttonSellWoodPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonSellWoodView = buttonSellWood.GetComponent<DownUIButtonView>();
            
            GameObject buttonSellStonePrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonSellStone = Object.Instantiate(buttonSellStonePrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonSellStoneView = buttonSellStone.GetComponent<DownUIButtonView>();
            
            GameObject buttonSellIronPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonSellIron = Object.Instantiate(buttonSellIronPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonSellIronView = buttonSellIron.GetComponent<DownUIButtonView>();

            foreach (UIButtonInfo buttonInfo in prefabStaticData.Buttons)
            {
                if (buttonInfo.ButtonName == "SellFood")
                {
                    buttonSellFoodView.ButtonName.text = buttonInfo.ButtonName;
                    buttonSellFoodView.Icon.sprite = buttonInfo.ButtonIcon;
                }
                else if (buttonInfo.ButtonName == "SellWood")
                {
                    buttonSellWoodView.ButtonName.text = buttonInfo.ButtonName;
                    buttonSellWoodView.Icon.sprite = buttonInfo.ButtonIcon;
                }
                else if(buttonInfo.ButtonName == "SellStone")
                {
                    buttonSellStoneView.ButtonName.text = buttonInfo.ButtonName;
                    buttonSellStoneView.Icon.sprite = buttonInfo.ButtonIcon;
                }
                else if (buttonInfo.ButtonName == "SellIron")
                {
                    buttonSellIronView.ButtonName.text = buttonInfo.ButtonName;
                    buttonSellIronView.Icon.sprite = buttonInfo.ButtonIcon;
                }
            }
            
            downPanelView.SetActive(false);

            _storeController = new StoreBuildingController(_storeBuildingView, downPanelView, clickHandling, buttonSellFoodView.Button, buttonSellWoodView.Button, buttonSellStoneView.Button, buttonSellIronView.Button, _resourcesCount);
        }

        private async Task CreateBarracks()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.BARRACKS);
            GameObject barracks = Object.Instantiate(prefab, BuildingsPositionInWorld.BarracksPosition, Quaternion.identity);
            _barracksBuildingView = barracks.GetComponent<BarracksBuildingView>();
            ClickHandling clickHandling = barracks.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_BARRACKS);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;

            GameObject buttonCreateDefenderPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonCreateDefender = Object.Instantiate(buttonCreateDefenderPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonCreateDefenderView = buttonCreateDefender.GetComponent<DownUIButtonView>();

            foreach (UIButtonInfo buttonInfo in prefabStaticData.Buttons)
            {
                if (buttonInfo.ButtonName == "CreateDefender")
                {
                    buttonCreateDefenderView.ButtonName.text = buttonInfo.ButtonName;
                    buttonCreateDefenderView.Icon.sprite = buttonInfo.ButtonIcon;
                }
            }

            downPanelView.SetActive(false);

            _barracksController = new BarracksBuildingController(this, _barracksBuildingView, downPanelView, clickHandling, buttonCreateDefenderView.Button, _resourcesCount);
        }

        private async Task CreateSmithy()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.SMITHY);
            GameObject smithy = Object.Instantiate(prefab, BuildingsPositionInWorld.SmithyPosition, Quaternion.identity);
            ClickHandling clickHandling = smithy.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_SMITHY);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;

            GameObject buttonUpgradeFoodPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonUpgradeFood = Object.Instantiate(buttonUpgradeFoodPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonUpgradeFoodView = buttonUpgradeFood.GetComponent<DownUIButtonView>();
            
            GameObject buttonUpgradeWoodPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonUpgradeWood = Object.Instantiate(buttonUpgradeWoodPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonUpgradeWoodView = buttonUpgradeWood.GetComponent<DownUIButtonView>();
            
            GameObject buttonUpgradeStonePrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonUpgradeStone = Object.Instantiate(buttonUpgradeStonePrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonUpgradeStoneView = buttonUpgradeStone.GetComponent<DownUIButtonView>();
            
            GameObject buttonUpgradeIronPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonUpgradeIron = Object.Instantiate(buttonUpgradeIronPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonUpgradeIronView = buttonUpgradeIron.GetComponent<DownUIButtonView>();

            foreach (UIButtonInfo buttonInfo in prefabStaticData.Buttons)
            {
                if (buttonInfo.ButtonName == "UpdateFood")
                {
                    buttonUpgradeFoodView.ButtonName.text = buttonInfo.ButtonName;
                    buttonUpgradeFoodView.Icon.sprite = buttonInfo.ButtonIcon;
                }
                else if (buttonInfo.ButtonName == "UpdateWood")
                {
                    buttonUpgradeWoodView.ButtonName.text = buttonInfo.ButtonName;
                    buttonUpgradeWoodView.Icon.sprite = buttonInfo.ButtonIcon;
                }
                else if (buttonInfo.ButtonName == "UpdateStone")
                {
                    buttonUpgradeStoneView.ButtonName.text = buttonInfo.ButtonName;
                    buttonUpgradeStoneView.Icon.sprite = buttonInfo.ButtonIcon;
                }
                else if (buttonInfo.ButtonName == "UpdateIron")
                {
                    buttonUpgradeIronView.ButtonName.text = buttonInfo.ButtonName;
                    buttonUpgradeIronView.Icon.sprite = buttonInfo.ButtonIcon;
                }
            }
            
            downPanelView.SetActive(false);

            _smithyController = new SmithyController(downPanelView, clickHandling, _resourcesCount, buttonUpgradeFoodView.Button, buttonUpgradeWoodView.Button, buttonUpgradeStoneView.Button, buttonUpgradeIronView.Button, _craftDevelopment);
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

            resourcesUICount.Constructor(_resourcesCount);
        }

        public async Task<GameObject> CreateUIDownView()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CONTAINER);
            GameObject uiDownView = Object.Instantiate(prefab);
            return uiDownView;
        }

        public void CreateDefender()
        {
            Debug.Log("Create Defender");
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