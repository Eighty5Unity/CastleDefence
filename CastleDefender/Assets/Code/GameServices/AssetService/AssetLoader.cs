using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.GameServices.AssetService
{
    public class AssetLoader : IAssetLoader
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completeCache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();
        
        private readonly Dictionary<string, AsyncOperationHandle> _completeUnitsCache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _unitHandles =
            new Dictionary<string, List<AsyncOperationHandle>>();


        public void Initialize()
        {
            Addressables.InitializeAsync();
        }
        
        public async Task<T> LoadBuildings<T>(string prefab) where T : class
        {
            if (_completeCache.TryGetValue(prefab, out AsyncOperationHandle completeHandle))
            {
                return completeHandle.Result as T;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(prefab);
            handle.Completed += completeHandle =>
            {
                _completeCache[prefab] = completeHandle;
            };

            if (!_handles.TryGetValue(prefab, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[prefab] = resourceHandles;
            }
            resourceHandles.Add(handle);

            return await handle.Task;
        }

        public async Task<T> LoadUnits<T>(string prefab) where T : class
        {
            if (_completeUnitsCache.TryGetValue(prefab, out AsyncOperationHandle completeHandle))
            {
                return completeHandle.Result as T;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(prefab);
            handle.Completed += completeHandle =>
            {
                _completeUnitsCache[prefab] = completeHandle;
            };

            if (!_unitHandles.TryGetValue(prefab, out List<AsyncOperationHandle> resourcesHandles))
            {
                resourcesHandles = new List<AsyncOperationHandle>();
                _unitHandles[prefab] = resourcesHandles;
            }
            resourcesHandles.Add(handle);

            return await handle.Task;
        }

        public void CleanupBuildings()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }
            _completeCache.Clear();
            _handles.Clear();
        }
        
        public void CleanupUnits()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _unitHandles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }
            _completeUnitsCache.Clear();
            _unitHandles.Clear();
        }
    }
}