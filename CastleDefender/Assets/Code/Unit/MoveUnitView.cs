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
            NavMesh.Resume();
        }

        public void Stop()
        {
            NavMesh.Stop();
        }
    }
}