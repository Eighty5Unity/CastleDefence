using Code.Buildings.ResourcesBuilgings;
using Code.GameBalance;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class ResourcesUICount : MonoBehaviour
    {
        public TextMeshProUGUI FoodCount;
        public TextMeshProUGUI WoodCount;
        public TextMeshProUGUI StoneCount;
        public TextMeshProUGUI IronCount;
        public TextMeshProUGUI MoneyCount;
        public TextMeshProUGUI UnitCount;

        private ResourcesCount _resourcesCount;

        public void Constructor(ResourcesCount resourcesCount)
        {
            _resourcesCount = resourcesCount;
            _resourcesCount.UpdateResourcesCount += RefreshUI;
            RefreshUI();
        }

        private void RefreshUI()
        {
            FoodCount.text = _resourcesCount.Food.ToString();
            WoodCount.text = _resourcesCount.Wood.ToString();
            StoneCount.text = _resourcesCount.Stone.ToString();
            IronCount.text = _resourcesCount.Iron.ToString();
            MoneyCount.text = _resourcesCount.Money.ToString();
            // UnitCount.text = .ToString();
        }

        private void OnDestroy()
        {
            _resourcesCount.UpdateResourcesCount -= RefreshUI;
        }
    }
}