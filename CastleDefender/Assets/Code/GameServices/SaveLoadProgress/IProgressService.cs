namespace Code.GameServices.SaveLoadProgress
{
    public interface IProgressService : IService
    {
        GameProgress Progress { get; set; }
    }
}