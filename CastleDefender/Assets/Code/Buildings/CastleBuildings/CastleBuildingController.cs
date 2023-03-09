using Code.Buildings.ResourcesBuilgings;
using Code.GameBalance;
using Code.GameServices;
using Code.GameServices.InputService;
using Code.GameServices.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Buildings.CastleBuildings
{
    public class CastleBuildingController
    {
        private readonly IGameFactory _gameFactory;
        private readonly CastleBuildingView _castleView;
        private readonly GameObject _uiView;
        private readonly ClickHandling _clickHandling;
        private readonly Button _createUnitButton;
        private readonly ResourcesCount _resourcesCount;
        private readonly IPoolServices _poolServices;

        public CastleBuildingController(
            IPoolServices poolServices,
            IGameFactory factory, 
            CastleBuildingView castleView, 
            GameObject uiView, 
            ClickHandling clickHandling, 
            Button createUnit, 
            ResourcesCount resourcesCount)
        {
            _poolServices = poolServices;
            _gameFactory = factory;
            _castleView = castleView;
            _uiView = uiView;
            _resourcesCount = resourcesCount;
            
            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;
            
            _createUnitButton = createUnit;
            _createUnitButton.onClick.AddListener(CreateUnit);
        }

        private void CreateUnit()
        {
            if (_resourcesCount.CheckEnoughResources(CostEverything.Unit))
            {
               _poolServices.Instantiate<GameObject>(_gameFactory.CreateUnitPrefab().Result, _castleView.SpawnUnitPoint.position);
               _resourcesCount.RemoveResourcesCount(CostEverything.Unit);
            }
            else
            {
                Debug.Log("Not resources to make a Unit!");
            }
        }

        private void OnClick()
        {
            _uiView.SetActive(true);
        }

        private void OffClick()
        {
            _uiView.SetActive(false);
        }
    }
}