using Code.GameServices.InputService;
using UnityEngine;

namespace Code.Buildings.WallAndTowerBuildings
{
    public class TowerBuildingController
    {
        private readonly GameObject _uiView;
        private readonly TowerBuildingView _towerView;
        private readonly ClickHandling _clickHandling;

        public TowerBuildingController(GameObject uiView, ClickHandling clickHandling, TowerBuildingView towerView)
        {
            _uiView = uiView;
            _towerView = towerView;
            
            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;
        }

        private void OnClick()
        {
            _uiView.SetActive(true);
        }

        private void OffClick()
        {
            _uiView.SetActive(false);
        }
    }
}