using UnityEngine;

namespace Code.Buildings.CastleBuildings
{
    public class CastleBuilding : MonoBehaviour
    {
        public Transform SpawnUnitPoint;

        public Vector3 GetSpawnPosition()
        {
            return SpawnUnitPoint.position;
        }
    }
}