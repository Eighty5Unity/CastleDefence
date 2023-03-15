using System;
using Code.Buildings.WallAndTowerBuildings;
using UnityEngine;

namespace Code.Unit.EnemyUnit
{
    public class EnemyAttackView : MonoBehaviour
    {
        public event Action<Vector3> FoundAttackBuilding; 
        public Vector3 AttackBuildingPosition;
        private bool _isAttackBuildingFound;
        private float _rotateSpeed = 10f;
        private void FixedUpdate()
        {
            if (!_isAttackBuildingFound)
            {
                FoundTarget();
            }
        }

        private void FoundTarget()
        {
            if (ChooseAttackBuilding(out AttackBuildingPosition))
            {
                _isAttackBuildingFound = true;
                FoundAttackBuilding?.Invoke(AttackBuildingPosition);
            }
            else
            {
                transform.parent.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
            }
        }

        private bool ChooseAttackBuilding(out Vector3 result)
        {
            Ray ray = new Ray(transform.parent.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                AttackedBuilding attackedBuilding;
                if (hit.transform.TryGetComponent<AttackedBuilding>(out attackedBuilding))
                {
                    result = attackedBuilding.GetTransform();
                    return true;
                }
                result = Vector3.zero;
                return false;
            }
            result = Vector3.zero;
            return false;
        }
    }
}