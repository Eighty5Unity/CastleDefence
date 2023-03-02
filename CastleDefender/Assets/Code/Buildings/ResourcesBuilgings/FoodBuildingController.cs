using Code.GameServices.InputService;
using UnityEngine;

namespace Code.Buildings.ResourcesBuilgings
{
    public class FoodBuildingController
    {
        private readonly GameObject _uiView;
        private readonly ClickHandling _clickHandling;
        private readonly FoodBuildingView _foodView;

        public FoodBuildingController(GameObject downPanelView, ClickHandling clickHandling, FoodBuildingView foodView)
        {
            _uiView = downPanelView;
            _foodView = foodView;
            
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