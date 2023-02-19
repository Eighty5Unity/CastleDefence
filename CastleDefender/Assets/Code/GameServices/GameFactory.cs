using System.Threading.Tasks;
using Code.GameServices.AssetService;
using Code.Unit;
using UnityEngine;

namespace Code.GameServices
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IAssetLoader _assetLoader;

        public GameFactory(IStaticDataService staticData, IAssetLoader assetLoader)
        {
            _staticData = staticData;
            _assetLoader = assetLoader;
        }

        public async Task LoadAddressableAssets()
        {
            await _assetLoader.Load<GameObject>(AssetAddress.UNIT);
        }

        public async Task CreateUnit(Vector3 at)
        {
            GameObject prefab = await _assetLoader.Load<GameObject>(AssetAddress.UNIT);
            GameObject unit = Object.Instantiate(prefab, at, Quaternion.identity);

            MoveUnit move = unit.GetComponentInChildren<MoveUnit>();
            move.Counstructor(_staticData);
        }

        public void CreateDefender()
        {
            
        }

        public void CreateEnemy()
        {
            
        }

        public void Cleanup()
        {
            _assetLoader.Cleanup();
        }
    }
}