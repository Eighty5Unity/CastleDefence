using Code.Architecture.States;

namespace Code.GameServices.SaveLoadProgress
{
    public class ProgressService : IProgressService
    {
        public GameProgress Progress { get; set; }
    }
}