using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Unit;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.GameServices.Pool
{
    public class ObjectPool : IDisposable
    {
        private readonly Stack<GameObject> _stack = new Stack<GameObject>();
        private readonly GameObject _prefab;
        private readonly Transform _rootPool;

        public ObjectPool(GameObject prefab)
        {
            _prefab = prefab;
            _rootPool = new GameObject($"{_prefab.name}").transform;
        }

        public async Task<GameObject> Pop(Vector3 at)
        {
            GameObject result;
            if (_stack.Count == 0)
            {
                return null;
            }
            else
            {
                result = _stack.Pop();
                result.transform.SetParent(null);
                result.GetComponentInChildren<MoveUnitView>().transform.position = at;
                result.SetActive(true);
                return result;
            }
        }

        public void Push(GameObject prefab)
        {
            _stack.Push(prefab);
            prefab.SetActive(false);
            prefab.transform.SetParent(_rootPool);
        }

        public void Dispose()
        {
            for (var i = 0; i < _stack.Count; i++)
            {
                var gameObject = _stack.Pop();
                Object.Destroy(gameObject);
            }
            Object.Destroy(_rootPool.gameObject);
        }
    }
}