using System;

namespace Code.GameServices.SaveLoadProgress
{
    [Serializable]
    public class ResourcesProgress
    {
        public float Food = 200f;
        public float Wood;
        public float Stone;
        public float Iron;
        public float Money;
    }
}