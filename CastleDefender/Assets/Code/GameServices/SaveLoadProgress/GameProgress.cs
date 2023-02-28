using System;
using System.Collections.Generic;

namespace Code.GameServices.SaveLoadProgress
{
    [Serializable]
    public class GameProgress
    {
        public ResourcesProgress ResourcesProgress;
        public CraftProgress CraftProgress;
        public List<UnitProgress> UnitsProgress;

        public GameProgress()
        {
            ResourcesProgress = new ResourcesProgress();
            CraftProgress = new CraftProgress();
            UnitsProgress = new List<UnitProgress>();
        }
    }
}