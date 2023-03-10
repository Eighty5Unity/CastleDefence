using System;
using System.Collections.Generic;
using Code.Buildings.ResourcesBuilgings;
using Code.GameServices.SaveLoadProgress;

namespace Code.GameBalance
{
    public class ResourcesCount : ISaveProgress, ILoadProgress
    {
        public float Food { get; private set; } = 10f;
        public float Wood { get; private set; }
        public float Stone { get; private set; }
        public float Iron { get; private set; }
        public float Money { get; private set; }

        public event Action UpdateResourcesCount;

        public void AddResourcesCount(ResourcesType type, float count)
        {
            switch (type)
            {
                case ResourcesType.Food:
                    Food += count;
                    break;
                case ResourcesType.Wood:
                    Wood += count;
                    break;
                case ResourcesType.Stone:
                    Stone += count;
                    break;
                case ResourcesType.Iron:
                    Iron += count;
                    break;
                case ResourcesType.Money:
                    Money += count;
                    break;
                default:
                    break;
            }
            UpdateResourcesCount?.Invoke();
        }

        public void RemoveResourcesCount(Dictionary<ResourcesType, float> cost)
        {
            foreach (var keyValue in cost)
            {
                switch (keyValue.Key)
                {
                    case ResourcesType.Food:
                        Food -= keyValue.Value;
                        break;
                    case ResourcesType.Wood:
                        Wood -= keyValue.Value;
                        break;
                    case ResourcesType.Stone:
                        Stone -= keyValue.Value;
                        break;
                    case ResourcesType.Iron:
                        Iron -= keyValue.Value;
                        break;
                    case ResourcesType.Money:
                        Money -= keyValue.Value;
                        break;
                    default:
                        break;
                }
            }
            UpdateResourcesCount?.Invoke();
        }

        public void RemoveResourcesCount(ResourcesType type, float count)
        {
            switch (type)
            {
                case ResourcesType.Food:
                    Food -= count;
                    break;
                case ResourcesType.Wood:
                    Wood -= count;
                    break;
                case ResourcesType.Stone:
                    Stone -= count;
                    break;
                case ResourcesType.Iron:
                    Iron -= count;
                    break;
                case ResourcesType.Money:
                    Money -= count;
                    break;
                default:
                    break;
            }
            UpdateResourcesCount?.Invoke();
        }

        public bool CheckEnoughResources(Dictionary<ResourcesType, float> cost)
        {
            bool result = true;
            foreach (var keyValue in cost)
            {
                switch (keyValue.Key)
                {
                    case ResourcesType.Food:
                        if (keyValue.Value > Food)
                        {
                            result = false;
                        }
                        break;
                    case ResourcesType.Wood:
                        if (keyValue.Value > Wood)
                        {
                            result = false;
                        }
                        break;
                    case ResourcesType.Stone:
                        if (keyValue.Value > Stone)
                        {
                            result = false;
                        }
                        break;
                    case ResourcesType.Iron:
                        if (keyValue.Value > Iron)
                        {
                            result = false;
                        }
                        break;
                    case ResourcesType.Money:
                        if (keyValue.Value > Money)
                        {
                            result = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
        public void SaveProgress(GameProgress progress)
        {
            progress.ResourcesProgress.Food = Food;
            progress.ResourcesProgress.Wood = Wood;
            progress.ResourcesProgress.Stone = Stone;
            progress.ResourcesProgress.Iron = Iron;
            progress.ResourcesProgress.Money = Money;
        }

        public void LoadProgress(GameProgress progress)
        {
            Food = progress.ResourcesProgress.Food;
            Wood = progress.ResourcesProgress.Wood;
            Stone = progress.ResourcesProgress.Stone;
            Iron = progress.ResourcesProgress.Iron;
            Money = progress.ResourcesProgress.Money;
        }
    }
}