using System;

namespace Code.GameServices.SaveLoadProgress
{
    [Serializable]
    public class GameProgress
    {
        public ResourcesProgress ResourcesProgress;

        public GameProgress()
        {
            ResourcesProgress = new ResourcesProgress();
        }
    }
}