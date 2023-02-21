using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using Code.GameServices.SaveLoadProgress;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class ResourcesUICount : MonoBehaviour, ILoadProgress
    {
        public TextMeshProUGUI FoodCount;
        public TextMeshProUGUI WoodCount;
        public TextMeshProUGUI StoneCount;
        public TextMeshProUGUI IronCount;
        public TextMeshProUGUI MoneyCount;
        public TextMeshProUGUI UnitCount;

        private float _foodCount;
        private float _woodCount;
        private float _stoneCount;
        private float _ironCount;
        private float _moneyCount;
        private float _unitCount;
        
        private StoreBuildingView _store;

        public void Constructor(StoreBuildingView store)
        {
            _store = store;
            _store.RefreshResources += UpdateUICount;
        }

        private void UpdateUnitCount()
        {
            _unitCount++;
            RefreshUI();
        }

        private void UpdateUICount(ResourcesType type, float count)
        {
            switch (type)
            {
                case ResourcesType.Food:
                    _foodCount += count;
                    break;
                case ResourcesType.Wood:
                    _woodCount += count;
                    break;
                case ResourcesType.Stone:
                    _stoneCount += count;
                    break;
                case ResourcesType.Iron:
                    _ironCount += count;
                    break;
                case ResourcesType.Gold:
                    _moneyCount += count;
                    break;
                default:
                    break;
            }
            RefreshUI();
        }

        public void LoadProgress(GameProgress progress)
        {
            _foodCount = progress.ResourcesProgress.Food;
            _woodCount = progress.ResourcesProgress.Wood;
            _stoneCount = progress.ResourcesProgress.Stone;
            _ironCount = progress.ResourcesProgress.Iron;
            _moneyCount = progress.ResourcesProgress.Money;

            RefreshUI();
        }

        private void RefreshUI()
        {
            FoodCount.text = _foodCount.ToString();
            WoodCount.text = _woodCount.ToString();
            StoneCount.text = _stoneCount.ToString();
            IronCount.text = _ironCount.ToString();
            MoneyCount.text = _moneyCount.ToString();
            UnitCount.text = _unitCount.ToString();
        }

        private void OnDestroy()
        {
            _store.RefreshResources -= UpdateUICount;
        }
    }
}