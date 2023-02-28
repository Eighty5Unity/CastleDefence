using Code.Buildings.ResourcesBuilgings;
using Code.GameServices.SaveLoadProgress;
using UnityEngine;

namespace Code.GameBalance
{
    public class CraftDevelopment : ISaveProgress, ILoadProgress
    {
        private float _craftTimeFood = 10f;
        private float _craftTimeWood = 10f;
        private float _craftTimeStone = 10f;
        private float _craftTimeIron = 10f;

        private float _craftCountFood = 10f;
        private float _craftCountWood = 10f;
        private float _craftCountStone = 10f;
        private float _craftCountIron = 10f;

        private float _craftTimeResult;
        private float _craftCountResult;

        public float CraftTime(ResourcesType type)
        {
            switch (type)
            {
                case ResourcesType.Food:
                    _craftTimeResult = _craftTimeFood;
                    break;
                case ResourcesType.Wood:
                    _craftTimeResult = _craftTimeWood;
                    break;
                case ResourcesType.Stone:
                    _craftTimeResult = _craftTimeStone;
                    break;
                case ResourcesType.Iron:
                    _craftTimeResult = _craftTimeIron;
                    break;
                default:
                    break;
            }
            return _craftTimeResult;
        }

        public float CraftCount(ResourcesType type)
        {
            switch (type)
            {
                case ResourcesType.Food:
                    _craftCountResult = _craftCountFood;
                    break;
                case ResourcesType.Wood:
                    _craftCountResult = _craftCountWood;
                    break;
                case ResourcesType.Stone:
                    _craftCountResult = _craftCountStone;
                    break;
                case ResourcesType.Iron:
                    _craftCountResult = _craftCountIron;
                    break;
                default:
                    break;
            }
            return _craftCountResult;
        }

        public void UpgradeCraft(ResourcesType type)
        {
            switch (type)
            {
                case ResourcesType.Food:
                    _craftTimeFood = CheckCraftTime(_craftTimeFood);
                    _craftCountFood++;
                    break;
                case ResourcesType.Wood:
                    _craftTimeWood = CheckCraftTime(_craftTimeWood);
                    _craftCountWood++;
                    break;
                case ResourcesType.Stone:
                    _craftTimeStone = CheckCraftTime(_craftTimeStone);
                    _craftCountStone++;
                    break;
                case ResourcesType.Iron:
                    _craftTimeIron = CheckCraftTime(_craftTimeIron);
                    _craftCountIron++;
                    break;
                default:
                    break;
            }
        }

        private float CheckCraftTime(float craftTime)
        {
            craftTime--;
            if (craftTime < 5f)
            {
                craftTime = 5f;
            }
            Debug.Log(craftTime);
            return craftTime;
        }

        public void SaveProgress(GameProgress progress)
        {
            progress.CraftProgress.CraftTimeFood = _craftTimeFood;
            progress.CraftProgress.CraftTimeWood = _craftTimeWood;
            progress.CraftProgress.CraftTimeStone = _craftTimeStone;
            progress.CraftProgress.CraftTimeIron = _craftTimeIron;

            progress.CraftProgress.CraftCountFood = _craftCountFood;
            progress.CraftProgress.CraftCountWood = _craftCountWood;
            progress.CraftProgress.CraftCountStone = _craftCountStone;
            progress.CraftProgress.CraftCountIron = _craftCountIron;
        }

        public void LoadProgress(GameProgress progress)
        {
            _craftTimeFood = progress.CraftProgress.CraftTimeFood;
            _craftTimeWood = progress.CraftProgress.CraftTimeWood;
            _craftTimeStone = progress.CraftProgress.CraftTimeStone;
            _craftTimeIron = progress.CraftProgress.CraftTimeIron;

            _craftCountFood = progress.CraftProgress.CraftCountFood;
            _craftCountWood = progress.CraftProgress.CraftCountWood;
            _craftCountStone = progress.CraftProgress.CraftCountStone;
            _craftCountIron = progress.CraftProgress.CraftCountIron;
        }
    }
}