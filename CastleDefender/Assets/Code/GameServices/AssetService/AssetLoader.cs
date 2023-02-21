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

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }
        
        public async Task<T> Load<T>(string prefab) where T : class
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

        public void Cleanup()
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
    }
}