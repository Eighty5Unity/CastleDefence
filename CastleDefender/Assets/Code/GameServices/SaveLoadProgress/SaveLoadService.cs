using UnityEngine;

namespace Code.GameServices.SaveLoadProgress
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS_KEY = "Progress";
        private readonly IProgressService _progress;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IProgressService progress, IGameFactory factory)
        {
            _progress = progress;
            _gameFactory = factory;
        }
        
        public void SaveProgress()
        {
            foreach (ISaveProgress save in _gameFactory.SaveProgress)
            {
                save.SaveProgress(_progress.Progress);
            }
            PlayerPrefs.SetString(PROGRESS_KEY, _progress.Progress.ToJson());
        }

        public GameProgress LoadProgress()
        {
            return PlayerPrefs.GetString(PROGRESS_KEY)?.ToDeserialize<GameProgress>();
        }
    }
}