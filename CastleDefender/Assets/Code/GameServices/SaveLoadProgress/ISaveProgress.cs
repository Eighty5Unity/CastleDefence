using Code.Architecture.States;

namespace Code.GameServices.SaveLoadProgress
{
    public interface ISaveProgress
    {
        void SaveProgress(GameProgress progress);
    }
}