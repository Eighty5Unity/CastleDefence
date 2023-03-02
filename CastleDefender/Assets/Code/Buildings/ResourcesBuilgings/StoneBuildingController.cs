using Code.GameServices.InputService;
using UnityEngine;

namespace Code.Buildings.ResourcesBuilgings
{
    public class StoneBuildingController
    {
        private readonly GameObject _uiView;
        private readonly StoneBuildingView _stoneView;
        private readonly ClickHandling _clickHandling;

        public StoneBuildingController(GameObject downPanelView, ClickHandling clickHandling, StoneBuildingView stoneView)
        {
            _uiView = downPanelView;
            _stoneView = stoneView;
            
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