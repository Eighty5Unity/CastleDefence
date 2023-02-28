using Code.GameServices.SaveLoadProgress;

namespace Code.Buildings.ResourcesBuilgings
{
    public class CraftDevelopment : ISaveProgress, ILoadProgress
    {
        private float _craftTimeFood = 5f;
        private float _craftTimeWood = 5f;
        private float _craftTimeStone = 5f;
        private float _craftTimeIron = 5f;

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
                    _craftTimeFood -= 4f;
                    _craftCountFood += 10f;
                    break;
                case ResourcesType.Wood:
                    _craftTimeWood -= 4f;
                    _craftCountWood += 10f;
                    break;
                case ResourcesType.Stone:
                    _craftTimeStone -= 4f;
                    _craftCountStone += 10f;
                    break;
                case ResourcesType.Iron:
                    _craftTimeIron -= 4f;
                    _craftCountIron += 10f;
                    break;
                default:
                    break;
            }
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