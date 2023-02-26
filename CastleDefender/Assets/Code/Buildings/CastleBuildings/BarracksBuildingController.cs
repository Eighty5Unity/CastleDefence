using Code.Buildings.ResourcesBuilgings;
using Code.GameServices;
using Code.GameServices.InputService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Buildings.CastleBuildings
{
    public class BarracksBuildingController
    {
        private readonly GameFactory _gameFactory;
        private readonly BarracksBuildingView _barracksView;
        private readonly GameObject _uiView;
        private readonly ResourcesCount _resourcesCount;
        private readonly ClickHandling _clickHandling;
        private readonly Button _createDefenderButton;

        public BarracksBuildingController(
            GameFactory factory, 
            BarracksBuildingView barracksView, 
            GameObject uiView, 
            ClickHandling clickHandling, 
            Button createDefender, 
            ResourcesCount resourcesCount)
        {
            _gameFactory = factory;
            _barracksView = barracksView;
            _uiView = uiView;
            _resourcesCount = resourcesCount;

            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;

            _createDefenderButton = createDefender;
            _createDefenderButton.onClick.AddListener(CreateDefender);
        }

        private void CreateDefender()
        {
            if (_resourcesCount.CheckEnoughResources(CostEverything.Defender) && _barracksView.UnitToDefender > 0)
            {
                _gameFactory.CreateDefender();
                _resourcesCount.RemoveResourcesCount(CostEverything.Defender);
                _barracksView.UnitToDefender--;
            }
            else
            {
                Debug.Log("Not possible");
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