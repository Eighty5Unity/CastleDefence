using System.Collections.Generic;
using Code.Buildings.ResourcesBuilgings;

namespace Code.GameBalance
{
    public static class CostEverything
    {
        public static Dictionary<ResourcesType, float> Unit = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Food] = 10f,
        };

        public static Dictionary<ResourcesType,float> Defender = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Food] = 100f,
            [ResourcesType.Wood] = 50f,
            [ResourcesType.Iron] = 100f,
            [ResourcesType.Money] = 5f,
        };

        public static Dictionary<ResourcesType, float> UpgrateFoodCraft = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Wood] = 100f,
            [ResourcesType.Iron] = 50f,
            [ResourcesType.Money] = 10f,
        };

        public static Dictionary<ResourcesType, float> UpgrateWoodCraft = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Wood] = 100f,
            [ResourcesType.Iron] = 50f,
            [ResourcesType.Money] = 10f,
        };

        public static Dictionary<ResourcesType, float> UpgrateStoneCraft = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Wood] = 100f,
            [ResourcesType.Iron] = 50f,
            [ResourcesType.Money] = 10f,
        };

        public static Dictionary<ResourcesType, float> UpgrateIronCraft = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Wood] = 100f,
            [ResourcesType.Iron] = 50f,
            [ResourcesType.Money] = 10f,
        };
    }
}