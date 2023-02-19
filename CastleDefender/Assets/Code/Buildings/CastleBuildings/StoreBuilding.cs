using System;
using Code.Buildings.ResourcesBuilgings;
using Code.GameServices.SaveLoadProgress;
using UnityEngine;

namespace Code.Buildings.CastleBuildings
{
    public class StoreBuilding : MonoBehaviour, ISaveProgress, ILoadProgress
    {
        public event Action<ResourcesType, float> RefreshResources;
        public Transform StorePointPosition;

        private float _food;
        private float _wood;
        private float _stone;
        private float _iron;
        private float _gold;

        public void UpdateResouces(ResourcesType type, float count)
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
            RefreshResources?.Invoke(type, count);
        }

        public void SaveProgress(GameProgress progress)
        {
            progress.ResourcesProgress.Food = _food;
            progress.ResourcesProgress.Wood = _wood;
            progress.ResourcesProgress.Stone = _stone;
            progress.ResourcesProgress.Iron = _iron;
        }

        public void LoadProgress(GameProgress progress)
        {
            _food = progress.ResourcesProgress.Food;
            _wood = progress.ResourcesProgress.Wood;
            _stone = progress.ResourcesProgress.Stone;
            _iron = progress.ResourcesProgress.Iron;
        }
    }
}