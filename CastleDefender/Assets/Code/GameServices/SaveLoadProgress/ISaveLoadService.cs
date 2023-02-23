namespace Code.GameServices.SaveLoadProgress
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        GameProgress LoadProgress();
    }
}