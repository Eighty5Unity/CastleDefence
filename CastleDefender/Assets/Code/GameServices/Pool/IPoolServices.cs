using UnityEngine;

namespace Code.GameServices.Pool
{
    public interface IPoolServices : IService
    {
        GameObject Instantiate(GameObject prefab, Vector3 at);
        void Destroy(GameObject prefab);
    }
}