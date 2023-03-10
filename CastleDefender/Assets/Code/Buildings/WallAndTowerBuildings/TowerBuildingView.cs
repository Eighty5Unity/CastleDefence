using UnityEngine;

namespace Code.Buildings.WallAndTowerBuildings
{
    public class TowerBuildingView : MonoBehaviour
    {
        public Transform DefenderMovePoint;
        
        [SerializeField] private Transform _firstDefenderPosition;
        [SerializeField] private Transform _secondDefenderPosition;
        [SerializeField] private Transform _thirdDefenderPosition;
        private bool _firstPositionOccupied;
        private bool _secondPositionOccupied;
        private bool _thirdPositionOccupied;

        public Vector3 ChooseDefenderPosition()
        {
            if (!_firstPositionOccupied)
            {
                _firstPositionOccupied = true;
                return _firstDefenderPosition.position;
            }
            else if (!_secondPositionOccupied)
            {
                _secondPositionOccupied = true;
                return _secondDefenderPosition.position;
            }
            else
            {
                _thirdPositionOccupied = true;
                return _thirdDefenderPosition.position;
            }
        }

        public bool IsAvailable()
        {
            if (_firstPositionOccupied && _secondPositionOccupied && _thirdPositionOccupied)
            {
                return false;
            }

            return true;
        }
    }
}