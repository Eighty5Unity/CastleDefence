using Code.Buildings;
using Code.Buildings.ResourcesBuilgings;
using Code.GameServices.InputService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.GameServices
{
    public class SmithyController
    {
        private readonly GameObject _uiView;
        private readonly ResourcesCount _resourcesCount;
        private readonly ClickHandling _clickHandling;
        private readonly Button _upgradeFoodButton;
        private readonly Button _upgradeWoodButton;
        private readonly Button _upgradeStoneButton;
        private readonly Button _upgradeIronButton;
        private readonly CraftDevelopment _craftDevelopment;

        public SmithyController(
            GameObject uiView, 
            ClickHandling clickHandling, 
            ResourcesCount resourcesCount, 
            Button food, 
            Button wood, 
            Button stone, 
            Button iron,
            CraftDevelopment craftDevelopment)
        {
            _uiView = uiView;
            _resourcesCount = resourcesCount;
            _craftDevelopment = craftDevelopment;

            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;

            _upgradeFoodButton = food;
            _upgradeFoodButton.onClick.AddListener(UpgradeFood);
            _upgradeWoodButton = wood;
            _upgradeWoodButton.onClick.AddListener(UpgradeWood);
            _upgradeStoneButton = stone;
            _upgradeStoneButton.onClick.AddListener(UpgradeStone);
            _upgradeIronButton = iron;
            _upgradeIronButton.onClick.AddListener(UpgradeIron);
        }

        private void UpgradeFood()
        {
            if (_resourcesCount.CheckEnoughResources(CostEverything.UpgrateFoodCraft))
            {
                _resourcesCount.RemoveResourcesCount(CostEverything.UpgrateFoodCraft);
                _craftDevelopment.UpgradeCraft(ResourcesType.Food);
            }
        }

        private void UpgradeWood()
        {
            if (_resourcesCount.CheckEnoughResources(CostEverything.UpgrateWoodCraft))
            {
                _resourcesCount.RemoveResourcesCount(CostEverything.UpgrateWoodCraft);
                _craftDevelopment.UpgradeCraft(ResourcesType.Wood);
            }
        }

        private void UpgradeStone()
        {
            if (_resourcesCount.CheckEnoughResources(CostEverything.UpgrateStoneCraft))
            {
                _resourcesCount.RemoveResourcesCount(CostEverything.UpgrateStoneCraft);
                _craftDevelopment.UpgradeCraft(ResourcesType.Stone);
            }
        }

        private void UpgradeIron()
        {
            if (_resourcesCount.CheckEnoughResources(CostEverything.UpgrateIronCraft))
            {
                _resourcesCount.RemoveResourcesCount(CostEverything.UpgrateIronCraft);
                _craftDevelopment.UpgradeCraft(ResourcesType.Iron);
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