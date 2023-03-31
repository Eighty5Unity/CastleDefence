using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Buildings;
using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using Code.Buildings.WallAndTowerBuildings;
using Code.GameBalance;
using Code.GameServices.AssetService;
using Code.GameServices.InputService;
using Code.GameServices.Pool;
using Code.GameServices.SaveLoadProgress;
using Code.StaticData;
using Code.UI;
using Code.Unit;
using Code.Unit.CraftUnit;
using Code.Unit.DefenceUnit;
using Code.Unit.EnemyUnit;
using UnityEngine;

namespace Code.GameServices
{
    public class GameFactory : IGameFactory
    {
        public List<ISaveProgress> SaveProgress { get; } = new List<ISaveProgress>();
        public List<ILoadProgress> LoadProgress { get; } = new List<ILoadProgress>();
        
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
        
        private List<TowerBuildingView> _towersView = new List<TowerBuildingView>(4);
        private List<TowerBuildingController> _towersBuildingController = new List<TowerBuildingController>(4);
        private List<WallBuildingView> _wallsView = new List<WallBuildingView>(11);
        private List<WallBuildingController> _wallsBuildingController = new List<WallBuildingController>(11);
        private GateBuildingView _gateView;
        private GateBuildingController _gateBuildingController;
        
        private readonly IPoolServices _poolServices;

        public GameFactory(IAssetLoader assetLoader, IPoolServices poolServices)
        {
            _assetLoader = assetLoader;
            _poolServices = poolServices;

            _resourcesCount = new ResourcesCount();//TODO register save/load
            _craftDevelopment = new CraftDevelopment();//TODO register save/load
        }

        public async Task LoadAddressableAssets()
        {
            await _assetLoader.LoadUnits<GameObject>(AssetAddress.UNIT);
            await _assetLoader.LoadUnits<GameObject>(AssetAddress.DEFENDER);
            await _assetLoader.LoadUnits<GameObject>(AssetAddress.ENEMY);
            
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.STORE);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.CASTLE);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.BARRACKS);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.SMITHY);
            
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.WALL);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.TOWER);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.GATE);

            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.FOOD);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.WOOD);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.STONE);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.IRON);
            
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_UP_CANVAS);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_DOWN_CANVAS);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_UP_CONTAINER);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_DOWN_CONTAINER);
            await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_DOWN_BUTTON);

            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_STORE);
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_CASTLE);
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_BARRACKS);
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_SMITHY);
            
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_WALL);
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_TOWER);
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_GATE);
            
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_FOOD);
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_WOOD);
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_STONE);
            await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_IRON);
        }

        public async Task<GameObject> CreateUnit(Vector3 at)
        {
            GameObject prefab = await CreateUnitPrefab(AssetAddress.UNIT);
            GameObject unit = _poolServices.Instantiate(prefab, at);
            if (unit == null)
            {
                unit = Object.Instantiate(prefab, at, Quaternion.identity);
                unit.name = prefab.name;
            }
            RegisterProgress(unit);

            ClickHandling clickHandling = unit.GetComponentInChildren<ClickHandling>();
            OnTriggerHandlingUnit triggerHandling = unit.GetComponentInChildren<OnTriggerHandlingUnit>();

            MoveUnitView moveUnitView = unit.GetComponentInChildren<MoveUnitView>();
            CraftUnitView craftUnitView = unit.GetComponentInChildren<CraftUnitView>();
            
            MoveUnitController moveUnitController = new MoveUnitController(clickHandling, moveUnitView, _castleBuildingView, _storeBuildingView, _barracksBuildingView, _foodView, _woodView, _stoneView, _ironView, _gateBuildingController);
            TriggerUnitController triggerUnitController = new TriggerUnitController(_poolServices, triggerHandling, moveUnitController, moveUnitView, _craftDevelopment, unit, craftUnitView);

            return unit;
        }

        public async Task<GameObject> CreateDefender(Vector3 at)
        {
            GameObject prefab = await CreateUnitPrefab(AssetAddress.DEFENDER);
            GameObject defender = _poolServices.Instantiate(prefab, at);
            if (defender == null)
            {
                defender = Object.Instantiate(prefab, at, Quaternion.identity);
                defender.name = prefab.name;
            }
            RegisterProgress(defender);

            ClickHandling clickHandling = defender.GetComponentInChildren<ClickHandling>();
            OnTriggerHandlingDefender triggerHandling = defender.GetComponentInChildren<OnTriggerHandlingDefender>();
            MoveUnitView moveDefenderView = defender.GetComponentInChildren<MoveUnitView>();

            MoveDefenderController moveDefenderController = new MoveDefenderController(clickHandling, moveDefenderView);
            TriggerDefenderController triggerDefenderController = new TriggerDefenderController(triggerHandling, moveDefenderView);

            return defender;
        }

        public async Task<GameObject> CreateEnemy(Vector3 at)
        {
            GameObject prefab = await CreateUnitPrefab(AssetAddress.ENEMY);
            GameObject enemy = _poolServices.Instantiate(prefab, at);
            if (enemy == null)
            {
                enemy = Object.Instantiate(prefab, at, Quaternion.identity);
                enemy.name = prefab.name;
            }

            OnTriggerHandlingDefender triggerHandling = enemy.GetComponentInChildren<OnTriggerHandlingDefender>();

            MoveUnitView moveUnitView = enemy.GetComponentInChildren<MoveUnitView>();

            MoveEnemyController moveEnemyController = new MoveEnemyController(moveUnitView, _castleBuildingView);
            TriggerEnemyController enemyAttackController = new TriggerEnemyController(triggerHandling, moveUnitView);
            
            return enemy;
        }

        public async Task CreateBuildings()
        {
            await CreateStore();
            await CreateCastle();
            await CreateBarracks();
            await CreateSmithy();
        }

        public async Task CreateWallAndTower()
        {
            await CreateWalls();
            await CreateTowers();
            await CreateGate();
        }

        public async Task CreateResources()
        {
            await CreateFood();
            await CreateWood();
            await CreateStone();
            await CreateIron();
        }

        public async Task CreateUpUI()
        {
            GameObject prefabUp = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_UP_CANVAS);
            GameObject canvasUp = Object.Instantiate(prefabUp);
            _canvasUp = canvasUp.transform;
        }

        public async Task CreateDownUI()
        {
            GameObject prefabDown = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_DOWN_CANVAS);
            GameObject canvasDown = Object.Instantiate(prefabDown);
            _canvasDown = canvasDown.transform;
        }

        public async void CreateUIResourcesView()
        {
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_UP_CONTAINER);
            GameObject uiResourcesView = Object.Instantiate(prefab, _canvasUp);
            RegisterProgress(uiResourcesView);

            ResourcesUICount resourcesUICount = uiResourcesView.GetComponent<ResourcesUICount>();

            resourcesUICount.Constructor(_resourcesCount);
        }

        public void CleanupBuildings()
        {
            _assetLoader.CleanupBuildings();
        }

        public void Cleanup()
        {
            _assetLoader.CleanupBuildings();
            _assetLoader.CleanupUnits();
        }

        private async Task<GameObject> CreateBuildingPrefab(string assetAddress)
        {
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(assetAddress);
            return prefab;
        }

        private async Task<GameObject> CreateUnitPrefab(string assetAddress)
        {
            GameObject prefab = await _assetLoader.LoadUnits<GameObject>(assetAddress);
            return prefab;
        }

        private async Task CreateCastle()
        {
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.CASTLE);
            GameObject castle = Object.Instantiate(prefab, BuildingsPositionInWorld.CastlePosition, Quaternion.identity);
            _castleBuildingView = castle.GetComponent<CastleBuildingView>();
            ClickHandling clickHandling = castle.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_CASTLE);
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
            
            _castleController = new CastleBuildingController(_poolServices,this, _castleBuildingView, downPanelView, clickHandling, buttonCreateUnitView.Button, _resourcesCount);
        }

        private async Task CreateStore()
        {
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.STORE);
            GameObject store = Object.Instantiate(prefab, BuildingsPositionInWorld.StorePosition, Quaternion.identity);
            _storeBuildingView = store.GetComponent<StoreBuildingView>();
            ClickHandling clickHandling = store.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_STORE);
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
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.BARRACKS);
            GameObject barracks = Object.Instantiate(prefab, BuildingsPositionInWorld.BarracksPosition, Quaternion.identity);
            _barracksBuildingView = barracks.GetComponent<BarracksBuildingView>();
            ClickHandling clickHandling = barracks.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_BARRACKS);
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
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.SMITHY);
            GameObject smithy = Object.Instantiate(prefab, BuildingsPositionInWorld.SmithyPosition, Quaternion.identity);
            ClickHandling clickHandling = smithy.GetComponentInChildren<ClickHandling>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_SMITHY);
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

        private async Task CreateWalls()
        {
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.WALL);
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_WALL);

            for (int i = 0; i < BuildingsPositionInWorld.WallsPosition.Length; i++)
            {
                GameObject wall = Object.Instantiate(prefab, BuildingsPositionInWorld.WallsPosition[i], Quaternion.Euler(BuildingsPositionInWorld.WallsRotation[i]));
                ClickHandling clickHandling = wall.GetComponentInChildren<ClickHandling>();
                WallBuildingView wallView = wall.GetComponent<WallBuildingView>();
                
                GameObject downPanelView = await CreateUIDownView();
                DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
                infoView.Icon.sprite = prefabStaticData.Icon;
                infoView.Name.text = prefabStaticData.Name;
                infoView.Description.text = prefabStaticData.Descriptions;
                downPanelView.transform.parent = _canvasDown;
                downPanelView.SetActive(false);
                
                WallBuildingController wallBuildingController = new WallBuildingController(downPanelView, clickHandling, wallView);
                
                _wallsView.Add(wallView);
                _wallsBuildingController.Add(wallBuildingController);
            }
        }

        private async Task CreateTowers()
        {
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.TOWER);
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_TOWER);

            for (int i = 0; i < BuildingsPositionInWorld.TowersPosition.Length; i++)
            {
                GameObject tower = Object.Instantiate(prefab, BuildingsPositionInWorld.TowersPosition[i], Quaternion.Euler(BuildingsPositionInWorld.TowersRotation[i]));
                ClickHandling clickHandling = tower.GetComponentInChildren<ClickHandling>();
                TowerBuildingView towerView = tower.GetComponent<TowerBuildingView>();
                
                GameObject downPanelView = await CreateUIDownView();
                DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
                infoView.Icon.sprite = prefabStaticData.Icon;
                infoView.Name.text = prefabStaticData.Name;
                infoView.Description.text = prefabStaticData.Descriptions;
                downPanelView.transform.parent = _canvasDown;
                downPanelView.SetActive(false);
                
                TowerBuildingController towerController = new TowerBuildingController(downPanelView, clickHandling, towerView);
                
                _towersView.Add(towerView);
                _towersBuildingController.Add(towerController);
            }
        }

        private async Task CreateGate()
        {
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.GATE);
            GameObject gate = Object.Instantiate(prefab, BuildingsPositionInWorld.GatePosition, Quaternion.Euler(BuildingsPositionInWorld.GateRotation));
            ClickHandling clickHandling = gate.GetComponentInChildren<ClickHandling>();
            _gateView = gate.GetComponent<GateBuildingView>();
            Animation openCloseAnimation = gate.GetComponentInChildren<Animation>();
            
            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_GATE);
            DownInformationUI infoView = downPanelView.GetComponent<DownInformationUI>();
            infoView.Icon.sprite = prefabStaticData.Icon;
            infoView.Name.text = prefabStaticData.Name;
            infoView.Description.text = prefabStaticData.Descriptions;
            downPanelView.transform.parent = _canvasDown;
            
            DownUIButtonView buttonOpenCloseView = await CreateButtonView(infoView);
            foreach (UIButtonInfo buttonInfo in prefabStaticData.Buttons)
            {
                if (buttonInfo.ButtonName == "OpenClose")
                {
                    buttonOpenCloseView.ButtonName.text = buttonInfo.ButtonName;
                    buttonOpenCloseView.Icon.sprite = buttonInfo.ButtonIcon;
                }
            }

            downPanelView.SetActive(false);

            _gateBuildingController = new GateBuildingController(downPanelView, clickHandling, _gateView, buttonOpenCloseView.Button, openCloseAnimation);
        }

        private async Task CreateFood()
        {
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.FOOD);
            GameObject food = Object.Instantiate(prefab, BuildingsPositionInWorld.FoodPosition, Quaternion.identity);
            ClickHandling clickHandling = food.GetComponentInChildren<ClickHandling>();
            _foodView = food.GetComponent<FoodBuildingView>();

            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_FOOD);
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
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.WOOD);
            GameObject wood = Object.Instantiate(prefab, BuildingsPositionInWorld.WoodPosition, Quaternion.identity);
            ClickHandling clickHandling = wood.GetComponentInChildren<ClickHandling>();
            _woodView = wood.GetComponent<WoodBuildingView>();
            
            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_WOOD);
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
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.STONE);
            GameObject stone = Object.Instantiate(prefab, BuildingsPositionInWorld.StonePosition, Quaternion.identity);
            ClickHandling clickHandling = stone.GetComponentInChildren<ClickHandling>();
            _stoneView = stone.GetComponent<StoneBuildingView>();
            
            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_STONE);
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
            GameObject prefab = await CreateBuildingPrefab(AssetAddress.IRON);
            GameObject iron = Object.Instantiate(prefab, BuildingsPositionInWorld.IronPosition, Quaternion.identity);
            ClickHandling clickHandling = iron.GetComponentInChildren<ClickHandling>();
            _ironView = iron.GetComponent<IronBuildingView>();
            
            GameObject downPanelView = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_IRON);
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
            GameObject buttonUpgradeFoodPrefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_DOWN_BUTTON);
            GameObject buttonUpgradeFood = Object.Instantiate(buttonUpgradeFoodPrefab, infoView.ButtonsPosition);
            DownUIButtonView buttonUpgradeFoodView = buttonUpgradeFood.GetComponent<DownUIButtonView>();
            return buttonUpgradeFoodView;
        }

        private async Task<GameObject> CreateUIDownView()
        {
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_DOWN_CONTAINER);
            GameObject uiDownView = Object.Instantiate(prefab);
            return uiDownView;
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