using Code.Buildings.ResourcesBuilgings;
using Code.GameServices.InputService;
using Code.GameServices.SaveLoadProgress;
using UnityEngine;

namespace Code.Buildings.CastleBuildings
{
    public class StoreBuildingController : ISaveProgress, ILoadProgress
    {
        private float _food;
        private float _wood;
        private float _stone;
        private float _iron;
        private float _money;
        private readonly StoreBuildingView _storeView;
        private readonly GameObject _uiView;
        private readonly ClickHandling _clickHandling;

        public StoreBuildingController(StoreBuildingView storeView, GameObject uiView, ClickHandling clickHandling)
        {
            _storeView = storeView;
            _storeView.RefreshResources += UpdateResources;
            
            _uiView = uiView;
            
            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;
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
            switch (type)
            {
                case ResourcesType.Food:
                    _food += count;
                    break;
                case ResourcesType.Wood:
                    _wood += count;
                    break;
                case ResourcesType.Stone:
                    _stone += count;
                    break;
                case ResourcesType.Iron:
                    _iron += count;
                    break;
                default:
                    break;
            }
        }

        public void SaveProgress(GameProgress progress)
        {
            progress.ResourcesProgress.Food = _food;
            progress.ResourcesProgress.Wood = _wood;
            progress.ResourcesProgress.Stone = _stone;
            progress.ResourcesProgress.Iron = _iron;
            progress.ResourcesProgress.Money = _money;
        }

        public void LoadProgress(GameProgress progress)
        {
            _food = progress.ResourcesProgress.Food;
            _wood = progress.ResourcesProgress.Wood;
            _stone = progress.ResourcesProgress.Stone;
            _iron = progress.ResourcesProgress.Iron;
            _money = progress.ResourcesProgress.Money;
        }
    }
}