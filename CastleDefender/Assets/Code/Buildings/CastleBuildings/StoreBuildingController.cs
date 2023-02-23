using Code.Buildings.ResourcesBuilgings;
using Code.GameServices;
using Code.GameServices.InputService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Buildings.CastleBuildings
{
    public class StoreBuildingController
    {
        private readonly IGameFactory _gameFactory;
        private readonly StoreBuildingView _storeView;
        private readonly GameObject _uiView;
        private readonly ClickHandling _clickHandling;
        private readonly Button _openStore;
        private readonly ResourcesCount _resourcesCount;
        private GameObject _storeWindow;

        public StoreBuildingController(
            IGameFactory factory, 
            StoreBuildingView storeView, 
            GameObject uiView, 
            ClickHandling clickHandling, 
            Button openStore, 
            ResourcesCount resourcesCount)
        {
            _gameFactory = factory;
            _resourcesCount = resourcesCount;
            
            _storeView = storeView;
            _storeView.RefreshResources += UpdateResources;
            
            _uiView = uiView;
            
            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;

            _openStore = openStore;
            _openStore.onClick.AddListener(OpenStore);
        }

        private void OpenStore()
        {
            if (_storeWindow == null)
            {
                _storeWindow = _gameFactory.CreateUIStoreWindow();
            }
            _storeWindow.SetActive(true);
        }

        private void OnClick()
        {
            _uiView.SetActive(true);
        }

        private void OffClick()
        {
            _uiView.SetActive(false);
        }

        private void UpdateResources(ResourcesType type, float count)
        {
            _resourcesCount.AddResourcesCount(type, count);
        }
    }
}