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

        public void Resume()
        {
            NavMesh.isStopped = false;
        }

        public void Stop()
        {
            NavMesh.isStopped = true;
        }
    }
}