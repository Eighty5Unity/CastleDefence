using UnityEngine;

namespace Code.GameServices.Pool
{
    public interface IPoolServices : IService
    {
        T Instantiate<T>(GameObject prefab, Vector3 at) where T : class;
        void Destroy(GameObject prefab);
    }
}