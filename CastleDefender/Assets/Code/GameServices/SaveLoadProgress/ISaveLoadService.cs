using Code.Architecture.States;

namespace Code.GameServices.SaveLoadProgress
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        GameProgress LoadProgress();
    }
}