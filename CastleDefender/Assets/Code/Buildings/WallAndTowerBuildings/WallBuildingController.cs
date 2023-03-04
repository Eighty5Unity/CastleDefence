using Code.GameServices.InputService;
using UnityEngine;

namespace Code.Buildings.WallAndTowerBuildings
{
    public class WallBuildingController
    {
        private readonly GameObject _uiView;
        private readonly WallBuildingView _wallView;
        private readonly ClickHandling _clickHandling;

        public WallBuildingController(GameObject uiView, ClickHandling clickHandling, WallBuildingView wallView)
        {
            _uiView = uiView;
            _wallView = wallView;

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