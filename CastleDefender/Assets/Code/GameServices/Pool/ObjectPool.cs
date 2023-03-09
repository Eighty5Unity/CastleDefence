using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Unit;
using UnityEngine;

namespace Code.GameServices.Pool
{
    public class ObjectPool : IDisposable
    {
        private readonly Stack<GameObject> _stack = new Stack<GameObject>();
        private readonly GameObject _prefab;
        private readonly Transform _rootPool;
        private readonly GameFactory _gameFactory;

        public ObjectPool(GameObject prefab, GameFactory factory)
        {
            _prefab = prefab;
            _gameFactory = factory;
            _rootPool = new GameObject($"{_prefab.name}").transform;
        }

        public async Task<GameObject> Pop(Vector3 at)
        {
            GameObject result;
            if (_stack.Count == 0)
            {
                result = await _gameFactory.CreateUnit(at);
                result.name = _prefab.name;
            }
            else
            {
                result = _stack.Pop();
            }

            result.transform.SetParent(null);
            result.GetComponentInChildren<MoveUnitView>().transform.position = at;
            result.SetActive(true);
            return result;
        }

        public void Push(GameObject prefab)
        {
            _stack.Push(prefab);
            prefab.SetActive(false);
            prefab.transform.SetParent(_rootPool);
        }

        public void Dispose()
        {
            Debug.Log("ObjectPool Dispose");
        }
    }
}