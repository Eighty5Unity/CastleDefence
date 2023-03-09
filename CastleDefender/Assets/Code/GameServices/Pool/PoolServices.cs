using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.GameServices.Pool
{
    public class PoolServices : IPoolServices
    {
        private readonly Dictionary<string, ObjectPool> _viewCache = new Dictionary<string, ObjectPool>(12);
        private readonly GameFactory _gameFactory;

        public PoolServices(GameFactory factory)
        {
            _gameFactory = factory;
        }
        public T Instantiate<T>(GameObject prefab, Vector3 at) where T : class
        {
            if (!_viewCache.TryGetValue(prefab.name, out ObjectPool viewPool))
            {
                viewPool = new ObjectPool(prefab, _gameFactory);
                _viewCache[prefab.name] = viewPool;
            }
            GameObject result = viewPool.Pop(at).Result;
            return result as T;
        }

        public void Destroy(GameObject prefab)
        {
            _viewCache[prefab.name].Push(prefab);
        }
    }
}