using System;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class WaveUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _count;
        public event Action TimeIsOver;
        private float _timer;
        private bool _startTimer;

        private void Update()
        {
            if (_startTimer)
            {
                if (_timer > 0)
                {
                    UpdateTimer(_timer);
                    _timer -= Time.deltaTime;
                }
                else
                {
                    _startTimer = false;
                    TimeIsOver?.Invoke();
                }
            }
        }

        public void StartWave(float timer, int count)
        {
            _timer = timer;
            _count.text = $"{count}";
            _startTimer = true;
        }

        private void UpdateTimer(float timer)
        {
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);

            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}