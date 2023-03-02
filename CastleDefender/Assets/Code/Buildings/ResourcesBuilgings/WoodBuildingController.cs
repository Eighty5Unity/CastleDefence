using Code.GameServices.InputService;
using UnityEngine;

namespace Code.Buildings.ResourcesBuilgings
{
    public class WoodBuildingController
    {
        private readonly GameObject _uiView;
        private readonly WoodBuildingView _woodView;
        private readonly ClickHandling _clickHandling;

        public WoodBuildingController(GameObject downPanelView, ClickHandling clickHandling, WoodBuildingView woodView)
        {
            _uiView = downPanelView;
            _woodView = woodView;
            
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