using UnityEngine;

namespace Code.Buildings.WallAndTowerBuildings
{
    public class GateBuildingView : MonoBehaviour
    {
        public Transform DefenderMovePoint;
        
        [SerializeField] private Transform _firstDefenderPosition;
        [SerializeField] private Transform _secondDefenderPosition;
        private bool _firstPositionOccupied;
        private bool _secondPositionOccupied;

        public Vector3 ChooseDefenderPosition()
        {
            if (!_firstPositionOccupied)
            {
                _firstPositionOccupied = true;
                return _firstDefenderPosition.position;
            }
            else
            {
                _secondPositionOccupied = true;
                return _secondDefenderPosition.position;   
            }
        }

        public bool IsAvailable()
        {
            if (_firstPositionOccupied && _secondPositionOccupied)
            {
                return false;
            }

            return true;
        }
    }
}