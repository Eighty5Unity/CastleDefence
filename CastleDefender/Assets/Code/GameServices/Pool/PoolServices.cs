using System.Collections.Generic;
using UnityEngine;

namespace Code.GameServices.Pool
{
    public class PoolServices : IPoolServices
    {
        private readonly Dictionary<string, ObjectPool> _viewCache = new Dictionary<string, ObjectPool>(12);
        
        public GameObject Instantiate(GameObject prefab, Vector3 at)
        {
            if (!_viewCache.TryGetValue(prefab.name, out ObjectPool viewPool))
            {
                viewPool = new ObjectPool(prefab);
                _viewCache[prefab.name] = viewPool;
            }
            GameObject result = viewPool.Pop(at).Result;
            return result;
        }

        public void Destroy(GameObject prefab)
        {
            _viewCache[prefab.name].Push(prefab);
        }
    }
}