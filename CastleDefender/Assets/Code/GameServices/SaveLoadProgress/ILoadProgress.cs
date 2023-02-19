using Code.Architecture.States;

namespace Code.GameServices.SaveLoadProgress
{
    public interface ILoadProgress
    {
        void LoadProgress(GameProgress progress);
    }
}