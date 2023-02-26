using System.Collections.Generic;
using Code.Buildings.ResourcesBuilgings;

namespace Code.Buildings
{
    public static class CostEverything
    {
        public static Dictionary<ResourcesType, float> Unit = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Food] = 10f,
        };

        public static Dictionary<ResourcesType, float> UpgrateFoodCraft = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Wood] = 100f,
            [ResourcesType.Iron] = 50f,
            [ResourcesType.Money] = 10f,
        };

        public static Dictionary<ResourcesType,float> Defender = new Dictionary<ResourcesType, float>()
        {
            [ResourcesType.Food] = 10f,
            [ResourcesType.Wood] = 10f,
            [ResourcesType.Iron] = 10f,
            [ResourcesType.Money] = 1f,
        };
    }
}