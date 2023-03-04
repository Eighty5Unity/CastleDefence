using System;
using Code.GameServices.InputService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Buildings.WallAndTowerBuildings
{
    public class GateBuildingController
    {
        public event Action<bool> GateOpen;
        
        private const string OPEN_GATE_ANIMATION = "OpenGate";
        private const string CLOSE_GATE_ANIMATION = "CloseGate";
        private readonly GameObject _uiView;
        private readonly GateBuildingView _gateView;
        private readonly ClickHandling _clickHandling;
        private readonly Button _openCloseButton;
        private bool _isOpen;
        private readonly Animation _animationOpenClose;

        public GateBuildingController(GameObject uiView, ClickHandling clickHandling, GateBuildingView gateView, Button openClose, Animation openCloseAnimation)
        {
            _uiView = uiView;
            _gateView = gateView;
            _animationOpenClose = openCloseAnimation;
            _clickHandling = clickHandling;
            _clickHandling.OnClickHappend += OnClick;
            _clickHandling.OffClickHappend += OffClick;

            _openCloseButton = openClose;
            _openCloseButton.onClick.AddListener(OpenClose);
        }

        private void OpenClose()
        {
            if (_isOpen)
            {
                _isOpen = false;
                CloseGate();
            }
            else if (!_isOpen)
            {
                _isOpen = true;
                OpenGate();
            }
        }

        private void OpenGate()
        {
            _animationOpenClose.Play(OPEN_GATE_ANIMATION);
            GateOpen?.Invoke(true);
        }

        private void CloseGate()
        {
            _animationOpenClose.Play(CLOSE_GATE_ANIMATION);
            GateOpen?.Invoke(false);
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