using Code.GameServices;
using Code.GameServices.InputService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Buildings.CastleBuildings
{
    public class CastleBuildingController
    {
        private readonly IGameFactory _gameFactory;
        private readonly CastleBuildingView _castleView;
        private readonly GameObject _uiView;
        private readonly ClickHandling _clickHandling;
        private readonly Button _createUnitButton;

        public CastleBuildingController(IGameFactory factory, CastleBuildingView castleView, GameObject uiView, ClickHandling clickHandling, Button createUnit)
        {
            _gameFactory = factory;
            _castleView = castleView;
            _uiView = uiView;
            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;
            _createUnitButton = createUnit;
            _createUnitButton.onClick.AddListener(CreateUnit);
        }

        private void CreateUnit()
        {
            _gameFactory.CreateUnit(_castleView.SpawnUnitPoint.position);
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