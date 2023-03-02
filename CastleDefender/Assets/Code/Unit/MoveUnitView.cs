using Code.Buildings.ResourcesBuilgings;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Unit
{
    public class MoveUnitView : MonoBehaviour
    {
        public NavMeshAgent NavMesh;
        
        public void Move(Vector3 position)
        {
            NavMesh.SetDestination(position);
        }
    }
}