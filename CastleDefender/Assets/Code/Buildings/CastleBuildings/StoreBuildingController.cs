using Code.Buildings.ResourcesBuilgings;
using Code.GameBalance;
using Code.GameServices.InputService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Buildings.CastleBuildings
{
    public class StoreBuildingController
    {
        private readonly StoreBuildingView _storeView;
        private readonly GameObject _uiView;
        private readonly ClickHandling _clickHandling;
        private readonly ResourcesCount _resourcesCount;
        private GameObject _storeWindow;
        private readonly Button _sellFood;
        private readonly Button _sellWood;
        private readonly Button _sellStone;
        private readonly Button _sellIron;

        public StoreBuildingController(
            StoreBuildingView storeView, 
            GameObject uiView, 
            ClickHandling clickHandling, 
            Button sellFood,
            Button sellWood,
            Button sellStone,
            Button sellIron, 
            ResourcesCount resourcesCount)
        {
            _resourcesCount = resourcesCount;
            
            _storeView = storeView;
            _storeView.RefreshResources += UpdateResources;
            
            _uiView = uiView;
            
            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;

            _sellFood = sellFood;
            _sellFood.onClick.AddListener(SellFood);
            _sellWood = sellWood;
            _sellWood.onClick.AddListener(SellWood);
            _sellStone = sellStone;
            _sellStone.onClick.AddListener(SellStone);
            _sellIron = sellIron;
            _sellIron.onClick.AddListener(SellIron);
        }

        private void SellFood()
        {
            if (_resourcesCount.Food >= ResourcesExchange.Food)
            {
                _resourcesCount.RemoveResourcesCount(ResourcesType.Food, ResourcesExchange.Food);
                _resourcesCount.AddResourcesCount(ResourcesType.Money, ResourcesExchange.MoneyForFood);
            }
        }

        private void SellWood()
        {
            if (_resourcesCount.Wood >= ResourcesExchange.Wood)
            {
                _resourcesCount.RemoveResourcesCount(ResourcesType.Wood, ResourcesExchange.Wood);
                _resourcesCount.AddResourcesCount(ResourcesType.Money, ResourcesExchange.MoneyForWood);
            }
        }

        private void SellStone()
        {
            if (_resourcesCount.Stone >= ResourcesExchange.Stone)
            {
                _resourcesCount.RemoveResourcesCount(ResourcesType.Stone, ResourcesExchange.Stone);
                _resourcesCount.AddResourcesCount(ResourcesType.Money, ResourcesExchange.MoneyForStone);
            }
        }

        private void SellIron()
        {
            if (_resourcesCount.Iron >= ResourcesExchange.Iron)
            {
                _resourcesCount.RemoveResourcesCount(ResourcesType.Iron, ResourcesExchange.Iron);
                _resourcesCount.AddResourcesCount(ResourcesType.Money, ResourcesExchange.MoneyForIron);
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

        private void UpdateResources(ResourcesType type, float count)
        {
            _resourcesCount.AddResourcesCount(type, count);
        }
    }
}