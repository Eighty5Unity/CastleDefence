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
        
        private TowerBuildingView _towerView01;
        private TowerBuildingView _towerView02;
        private TowerBuildingView _towerView03;
        private TowerBuildingView _towerView04;
        private TowerBuildingController _towerBuildingController01;
        private TowerBuildingController _towerBuildingController02;
        private TowerBuildingController _towerBuildingController03;
        private TowerBuildingController _towerBuildingController04;
        
        private WallBuildingView _wallView01;
        private WallBuildingView _wallView02;
        private WallBuildingView _wallView03;
        private WallBuildingView _wallView04;
        private WallBuildingView _wallView05;
        private WallBuildingView _wallView06;
        private WallBuildingView _wallView07;
        private WallBuildingView _wallView08;
        private WallBuildingView _wallView09;
        private WallBuildingView _wallView10;
        private WallBuildingView _wallView11;
        private WallBuildingController _wallBuildingController01;
        private WallBuildingController _wallBuildingController02;
        private WallBuildingController _wallBuildingController03;
        private WallBuildingController _wallBuildingController04;
        private WallBuildingController _wallBuildingController05;
        private WallBuildingController _wallBuildingController06;
        private WallBuildingController _wallBuildingController07;
        private WallBuildingController _wallBuildingController08;
        private WallBuildingController _wallBuildingController09;
        private WallBuildingController _wallBuildingController10;
        private WallBuildingController _wallBuildingController11;
        
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

        private async Task<GameObject> CreateUnitPrefab(string assetAddress)
        {
            GameObject prefab = await _assetLoader.LoadUnits<GameObject>(assetAddress);
            return prefab;
        }
        
        private async Task<GameObject> CreateBuildingPrefab(string assetAddress)
        {
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(assetAddress);
            return prefab;
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

        private async Task CreateCastle()
        {
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.CASTLE);
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
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.STORE);
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
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.BARRACKS);
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
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.SMITHY);
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
            //TODO Refactoring!
            
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.WALL);

            GameObject wall01 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition01, Quaternion.Euler(BuildingsPositionInWorld.WallRotation01));
            GameObject wall02 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition02, Quaternion.Euler(BuildingsPositionInWorld.WallRotation02));
            GameObject wall03 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition03, Quaternion.Euler(BuildingsPositionInWorld.WallRotation03));
            GameObject wall04 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition04, Quaternion.Euler(BuildingsPositionInWorld.WallRotation04));
            GameObject wall05 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition05, Quaternion.Euler(BuildingsPositionInWorld.WallRotation05));
            GameObject wall06 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition06, Quaternion.Euler(BuildingsPositionInWorld.WallRotation06));
            GameObject wall07 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition07, Quaternion.Euler(BuildingsPositionInWorld.WallRotation07));
            GameObject wall08 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition08, Quaternion.Euler(BuildingsPositionInWorld.WallRotation08));
            GameObject wall09 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition09, Quaternion.Euler(BuildingsPositionInWorld.WallRotation09));
            GameObject wall10 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition10, Quaternion.Euler(BuildingsPositionInWorld.WallRotation10));
            GameObject wall11 = Object.Instantiate(prefab, BuildingsPositionInWorld.WallPosition11, Quaternion.Euler(BuildingsPositionInWorld.WallRotation11));

            ClickHandling clickHandling01 = wall01.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling02 = wall02.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling03 = wall03.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling04 = wall04.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling05 = wall05.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling06 = wall06.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling07 = wall07.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling08 = wall08.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling09 = wall09.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling10 = wall10.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling11 = wall11.GetComponentInChildren<ClickHandling>();

            _wallView01 = wall01.GetComponent<WallBuildingView>();
            _wallView02 = wall02.GetComponent<WallBuildingView>();
            _wallView03 = wall03.GetComponent<WallBuildingView>();
            _wallView04 = wall04.GetComponent<WallBuildingView>();
            _wallView05 = wall05.GetComponent<WallBuildingView>();
            _wallView06 = wall06.GetComponent<WallBuildingView>();
            _wallView07 = wall07.GetComponent<WallBuildingView>();
            _wallView08 = wall08.GetComponent<WallBuildingView>();
            _wallView09 = wall09.GetComponent<WallBuildingView>();
            _wallView10 = wall10.GetComponent<WallBuildingView>();
            _wallView11 = wall11.GetComponent<WallBuildingView>();

            GameObject downPanelView01 = await CreateUIDownView();
            GameObject downPanelView02 = await CreateUIDownView();
            GameObject downPanelView03 = await CreateUIDownView();
            GameObject downPanelView04 = await CreateUIDownView();
            GameObject downPanelView05 = await CreateUIDownView();
            GameObject downPanelView06 = await CreateUIDownView();
            GameObject downPanelView07 = await CreateUIDownView();
            GameObject downPanelView08 = await CreateUIDownView();
            GameObject downPanelView09 = await CreateUIDownView();
            GameObject downPanelView10 = await CreateUIDownView();
            GameObject downPanelView11 = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_WALL);

            DownInformationUI infoView01 = downPanelView01.GetComponent<DownInformationUI>();
            DownInformationUI infoView02 = downPanelView02.GetComponent<DownInformationUI>();
            DownInformationUI infoView03 = downPanelView03.GetComponent<DownInformationUI>();
            DownInformationUI infoView04 = downPanelView04.GetComponent<DownInformationUI>();
            DownInformationUI infoView05 = downPanelView05.GetComponent<DownInformationUI>();
            DownInformationUI infoView06 = downPanelView06.GetComponent<DownInformationUI>();
            DownInformationUI infoView07 = downPanelView07.GetComponent<DownInformationUI>();
            DownInformationUI infoView08 = downPanelView08.GetComponent<DownInformationUI>();
            DownInformationUI infoView09 = downPanelView09.GetComponent<DownInformationUI>();
            DownInformationUI infoView10 = downPanelView10.GetComponent<DownInformationUI>();
            DownInformationUI infoView11 = downPanelView11.GetComponent<DownInformationUI>();

            infoView01.Icon.sprite = prefabStaticData.Icon;
            infoView02.Icon.sprite = prefabStaticData.Icon;
            infoView03.Icon.sprite = prefabStaticData.Icon;
            infoView04.Icon.sprite = prefabStaticData.Icon;
            infoView05.Icon.sprite = prefabStaticData.Icon;
            infoView06.Icon.sprite = prefabStaticData.Icon;
            infoView07.Icon.sprite = prefabStaticData.Icon;
            infoView08.Icon.sprite = prefabStaticData.Icon;
            infoView09.Icon.sprite = prefabStaticData.Icon;
            infoView10.Icon.sprite = prefabStaticData.Icon;
            infoView11.Icon.sprite = prefabStaticData.Icon;
            infoView01.Name.text = prefabStaticData.Name;
            infoView02.Name.text = prefabStaticData.Name;
            infoView03.Name.text = prefabStaticData.Name;
            infoView04.Name.text = prefabStaticData.Name;
            infoView05.Name.text = prefabStaticData.Name;
            infoView06.Name.text = prefabStaticData.Name;
            infoView07.Name.text = prefabStaticData.Name;
            infoView08.Name.text = prefabStaticData.Name;
            infoView09.Name.text = prefabStaticData.Name;
            infoView10.Name.text = prefabStaticData.Name;
            infoView11.Name.text = prefabStaticData.Name;
            infoView01.Description.text = prefabStaticData.Descriptions;
            infoView02.Description.text = prefabStaticData.Descriptions;
            infoView03.Description.text = prefabStaticData.Descriptions;
            infoView04.Description.text = prefabStaticData.Descriptions;
            infoView05.Description.text = prefabStaticData.Descriptions;
            infoView06.Description.text = prefabStaticData.Descriptions;
            infoView07.Description.text = prefabStaticData.Descriptions;
            infoView08.Description.text = prefabStaticData.Descriptions;
            infoView09.Description.text = prefabStaticData.Descriptions;
            infoView10.Description.text = prefabStaticData.Descriptions;
            infoView11.Description.text = prefabStaticData.Descriptions;

            downPanelView01.transform.parent = _canvasDown;
            downPanelView02.transform.parent = _canvasDown;
            downPanelView03.transform.parent = _canvasDown;
            downPanelView04.transform.parent = _canvasDown;
            downPanelView05.transform.parent = _canvasDown;
            downPanelView06.transform.parent = _canvasDown;
            downPanelView07.transform.parent = _canvasDown;
            downPanelView08.transform.parent = _canvasDown;
            downPanelView09.transform.parent = _canvasDown;
            downPanelView10.transform.parent = _canvasDown;
            downPanelView11.transform.parent = _canvasDown;
            
            downPanelView01.SetActive(false);
            downPanelView02.SetActive(false);
            downPanelView03.SetActive(false);
            downPanelView04.SetActive(false);
            downPanelView05.SetActive(false);
            downPanelView06.SetActive(false);
            downPanelView07.SetActive(false);
            downPanelView08.SetActive(false);
            downPanelView09.SetActive(false);
            downPanelView10.SetActive(false);
            downPanelView11.SetActive(false);

            _wallBuildingController01 = new WallBuildingController(downPanelView01, clickHandling01, _wallView01);
            _wallBuildingController02 = new WallBuildingController(downPanelView02, clickHandling02, _wallView02);
            _wallBuildingController03 = new WallBuildingController(downPanelView03, clickHandling03, _wallView03);
            _wallBuildingController04 = new WallBuildingController(downPanelView04, clickHandling04, _wallView04);
            _wallBuildingController05 = new WallBuildingController(downPanelView05, clickHandling05, _wallView05);
            _wallBuildingController06 = new WallBuildingController(downPanelView06, clickHandling06, _wallView06);
            _wallBuildingController07 = new WallBuildingController(downPanelView07, clickHandling07, _wallView07);
            _wallBuildingController08 = new WallBuildingController(downPanelView08, clickHandling08, _wallView08);
            _wallBuildingController09 = new WallBuildingController(downPanelView09, clickHandling09, _wallView09);
            _wallBuildingController10 = new WallBuildingController(downPanelView10, clickHandling10, _wallView10);
            _wallBuildingController11 = new WallBuildingController(downPanelView11, clickHandling11, _wallView11);
        }

        private async Task CreateTowers()
        {
            //TODO Refactoring!
            
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.TOWER);

            GameObject tower01 = Object.Instantiate(prefab, BuildingsPositionInWorld.TowerPosition01, Quaternion.Euler(BuildingsPositionInWorld.TowerRotation01));
            GameObject tower02 = Object.Instantiate(prefab, BuildingsPositionInWorld.TowerPosition02, Quaternion.Euler(BuildingsPositionInWorld.TowerRotation02));
            GameObject tower03 = Object.Instantiate(prefab, BuildingsPositionInWorld.TowerPosition03, Quaternion.Euler(BuildingsPositionInWorld.TowerRotation03));
            GameObject tower04 = Object.Instantiate(prefab, BuildingsPositionInWorld.TowerPosition04, Quaternion.Euler(BuildingsPositionInWorld.TowerRotation04));

            ClickHandling clickHandling01 = tower01.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling02 = tower02.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling03 = tower03.GetComponentInChildren<ClickHandling>();
            ClickHandling clickHandling04 = tower04.GetComponentInChildren<ClickHandling>();

            _towerView01 = tower01.GetComponent<TowerBuildingView>();
            _towerView02 = tower02.GetComponent<TowerBuildingView>();
            _towerView03 = tower03.GetComponent<TowerBuildingView>();
            _towerView04 = tower04.GetComponent<TowerBuildingView>();

            GameObject downPanelView01 = await CreateUIDownView();
            GameObject downPanelView02 = await CreateUIDownView();
            GameObject downPanelView03 = await CreateUIDownView();
            GameObject downPanelView04 = await CreateUIDownView();
            DownInformationStaticData prefabStaticData = await _assetLoader.LoadBuildings<DownInformationStaticData>(AssetAddress.STATIC_DATA_TOWER);

            DownInformationUI infoView01 = downPanelView01.GetComponent<DownInformationUI>();
            DownInformationUI infoView02 = downPanelView02.GetComponent<DownInformationUI>();
            DownInformationUI infoView03 = downPanelView03.GetComponent<DownInformationUI>();
            DownInformationUI infoView04 = downPanelView04.GetComponent<DownInformationUI>();

            infoView01.Icon.sprite = prefabStaticData.Icon;
            infoView02.Icon.sprite = prefabStaticData.Icon;
            infoView03.Icon.sprite = prefabStaticData.Icon;
            infoView04.Icon.sprite = prefabStaticData.Icon;
            infoView01.Name.text = prefabStaticData.Name;
            infoView02.Name.text = prefabStaticData.Name;
            infoView03.Name.text = prefabStaticData.Name;
            infoView04.Name.text = prefabStaticData.Name;
            infoView01.Description.text = prefabStaticData.Descriptions;
            infoView02.Description.text = prefabStaticData.Descriptions;
            infoView03.Description.text = prefabStaticData.Descriptions;
            infoView04.Description.text = prefabStaticData.Descriptions;

            downPanelView01.transform.parent = _canvasDown;
            downPanelView02.transform.parent = _canvasDown;
            downPanelView03.transform.parent = _canvasDown;
            downPanelView04.transform.parent = _canvasDown;
            
            downPanelView01.SetActive(false);
            downPanelView02.SetActive(false);
            downPanelView03.SetActive(false);
            downPanelView04.SetActive(false);

            _towerBuildingController01 = new TowerBuildingController(downPanelView01, clickHandling01, _towerView01);
            _towerBuildingController02 = new TowerBuildingController(downPanelView02, clickHandling02, _towerView02);
            _towerBuildingController03 = new TowerBuildingController(downPanelView03, clickHandling03, _towerView03);
            _towerBuildingController04 = new TowerBuildingController(downPanelView04, clickHandling04, _towerView04);
        }

        private async Task CreateGate()
        {
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.GATE);
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
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.FOOD);
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
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.WOOD);
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
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.STONE);
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
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.IRON);
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

        public async Task<GameObject> CreateUIDownView()
        {
            GameObject prefab = await _assetLoader.LoadBuildings<GameObject>(AssetAddress.UI_DOWN_CONTAINER);
            GameObject uiDownView = Object.Instantiate(prefab);
            return uiDownView;
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