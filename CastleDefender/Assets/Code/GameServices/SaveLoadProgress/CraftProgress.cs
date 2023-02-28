using System;

namespace Code.GameServices.SaveLoadProgress
{
    [Serializable]
    public class CraftProgress
    {
        public float CraftTimeFood;
        public float CraftTimeWood;
        public float CraftTimeStone;
        public float CraftTimeIron;

        public float CraftCountFood;
        public float CraftCountWood;
        public float CraftCountStone;
        public float CraftCountIron;
    }
}