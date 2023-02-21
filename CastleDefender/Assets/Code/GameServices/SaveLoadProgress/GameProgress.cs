using System;
using System.Collections.Generic;

namespace Code.GameServices.SaveLoadProgress
{
    [Serializable]
    public class GameProgress
    {
        public ResourcesProgress ResourcesProgress;
        public List<UnitProgress> UnitsProgress;

        public GameProgress()
        {
            ResourcesProgress = new ResourcesProgress();
            UnitsProgress = new List<UnitProgress>();
        }
    }
}