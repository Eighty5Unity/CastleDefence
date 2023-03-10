using System;
using Code.Buildings.WallAndTowerBuildings;
using UnityEngine;

namespace Code.Unit.DefenceUnit
{
    public class OnTriggerHandlingDefender : MonoBehaviour
    {
        public event Action<WallBuildingView> WallEnter;
        public event Action<WallBuildingView> WallExit;
        public event Action<TowerBuildingView> TowerEnter;
        public event Action<TowerBuildingView> TowerExit;

        private WallBuildingView _wallView;
        private TowerBuildingView _towerView;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<WallBuildingView>(out _wallView))
            {
                WallEnter?.Invoke(_wallView);
            }
            else if (other.TryGetComponent<TowerBuildingView>(out _towerView))
            {
                TowerEnter?.Invoke(_towerView);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<WallBuildingView>(out _wallView))
            {
                WallExit?.Invoke(_wallView);
            }
            else if (other.TryGetComponent<TowerBuildingView>(out _towerView))
            {
                TowerExit?.Invoke(_towerView);
            }
        }
    }
}