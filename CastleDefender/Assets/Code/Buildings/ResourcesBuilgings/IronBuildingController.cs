using Code.GameServices.InputService;
using UnityEngine;

namespace Code.Buildings.ResourcesBuilgings
{
    public class IronBuildingController
    {
        private readonly GameObject _uiView;
        private readonly IronBuildingView _ironView;
        private readonly ClickHandling _clickHandling;

        public IronBuildingController(GameObject downPanelView, ClickHandling clickHandling, IronBuildingView ironView)
        {
            _uiView = downPanelView;
            _ironView = ironView;
            
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