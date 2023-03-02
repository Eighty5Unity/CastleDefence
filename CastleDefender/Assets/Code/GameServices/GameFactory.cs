using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Buildings;
using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
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
        
        private FoodBuildingView _foodView;
        private FoodBuildingController _foodBuildingController;
        private WoodBuildingView _woodView;
        private WoodBuildingController _woodBuildingController;
        private StoneBuildingView _stoneView;
        private StoneBuildingController _stoneBuildingController;
        private IronBuildingView _ironView;
        private IronBuildingController _ironBuildingController;

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

            await _assetLoader.Load<GameObject>(AssetAddress.FOOD);
            await _assetLoader.Load<GameObject>(AssetAddress.WOOD);
            await _assetLoader.Load<GameObject>(AssetAddress.STONE);
            await _assetLoader.Load<GameObject>(AssetAddress.IRON);
            
            await _assetLoader.Load<GameObject>(AssetAddress.UI_UP_CANVAS);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CANVAS);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_UP_CONTAINER);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_CONTAINER);
            await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);

            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_STORE);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_CASTLE);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_BARRACKS);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_SMITHY);
            
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_FOOD);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_WOOD);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_STONE);
            await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_IRON);
        }

        public async Task CreateUnit(Vector3 at)
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.UNIT);
            GameObject unit = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgress(unit);

            ClickHandling clickHandling = unit.GetComponentInChildren<ClickHandling>();
            OnTriggerHandlingUnit triggerHandling = unit.GetComponentInChildren<OnTriggerHandlingUnit>();

            MoveUnitView moveUnitView = unit.GetComponentInChildren<MoveUnitView>();
            CraftUnitView craftUnitView = unit.GetComponentInChildren<CraftUnitView>();
            
            MoveUnitController moveUnitController = new MoveUnitController(_staticData, clickHandling, moveUnitView, _castleBuildingView, _storeBuildingView, _barracksBuildingView);

            TriggerUnitController triggerUnitController = new TriggerUnitController(triggerHandling, moveUnitController, moveUnitView, _craftDevelopment, unit, craftUnitView);
        }

        public void CreateDefender()
        {
            Debug.Log("Create Defender");
        }

        public void CreateEnemy()
        {
            
        }

        public async Task CreateBuildings()
        {
            await CreateStore();
            await CreateCastle();
            await CreateBarracks();
            await CreateSmithy();
        }

        public async Task CreateResources()
        {
            await CreateFood();
            await CreateWood();
            await CreateStone();
            await CreateIron();
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
            
            DownUIButtonView buttonCreateUnitView = await CreateButtonView(infoView);
            
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
            
            DownUIButtonView buttonSellFoodView = await CreateButtonView(infoView);
            DownUIButtonView buttonSellWoodView = await CreateButtonView(infoView);
            DownUIButtonView buttonSellStoneView = await CreateButtonView(infoView);
            DownUIButtonView buttonSellIronView = await CreateButtonView(infoView);

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
            
            DownUIButtonView buttonCreateDefenderView = await CreateButtonView(infoView);

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

            DownUIButtonView buttonUpgradeFoodView = await CreateButtonView(infoView);
            DownUIButtonView buttonUpgradeWoodView = await CreateButtonView(infoView);
            DownUIButtonView buttonUpgradeStoneView = await CreateButtonView(infoView);
            DownUIButtonView buttonUpgradeIronView = await CreateButtonView(infoView);

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

        private async Task CreateFood()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.FOOD);
            GameObject food = Object.Instantiate(prefab, BuildingsPositionInWorld.FoodPosition, Quaternion.identity);
            ClickHandling clickHandling = food.GetComponentInChildren<ClickHandling>();
            _foodView = food.GetComponent<FoodBuildingView>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_FOOD);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;

            downPanelView.SetActive(false);

            _foodBuildingController = new FoodBuildingController(downPanelView, clickHandling, _foodView);
        }

        private async Task CreateWood()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.WOOD);
            GameObject wood = Object.Instantiate(prefab, BuildingsPositionInWorld.WoodPosition, Quaternion.identity);
            ClickHandling clickHandling = wood.GetComponentInChildren<ClickHandling>();
            _woodView = wood.GetComponent<WoodBuildingView>();
            
            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_WOOD);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;

            downPanelView.SetActive(false);

            _woodBuildingController = new WoodBuildingController(downPanelView, clickHandling, _woodView);
        }

        private async Task CreateStone()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.STONE);
            GameObject stone = Object.Instantiate(prefab, BuildingsPositionInWorld.StonePosition, Quaternion.identity);
            ClickHandling clickHandling = stone.GetComponentInChildren<ClickHandling>();
            _stoneView = stone.GetComponent<StoneBuildingView>();
            
            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_STONE);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;

            downPanelView.SetActive(false);

            _stoneBuildingController = new StoneBuildingController(downPanelView, clickHandling, _stoneView);
        }

        private async Task CreateIron()
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.IRON);
            GameObject iron = Object.Instantiate(prefab, BuildingsPositionInWorld.IronPosition, Quaternion.identity);
            ClickHandling clickHandling = iron.GetComponentInChildren<ClickHandling>();
            _ironView = iron.GetComponent<IronBuildingView>();
            
            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.Load<DownInformationStaticData>(AssetAddress.STATIC_DATA_IRON);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;

            downPanelView.SetActive(false);

            _ironBuildingController = new IronBuildingController(downPanelView, clickHandling, _ironView);
        }

        private async Task<DownUIButtonView> CreateButtonView(DownInformationUI infoView)
        {
            GameObject buttonUpgradeFoodPrefab = await _assetLoader.Load<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonUpgradeFood = Object.Instantiate(buttonUpgradeFoodPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonUpgradeFoodView = buttonUpgradeFood.GetComponent<DownUIButtonView>();
            return buttonUpgradeFoodView;
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