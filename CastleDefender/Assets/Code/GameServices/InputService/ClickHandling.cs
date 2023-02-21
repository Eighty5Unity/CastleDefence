using System;
using Code.Buildings;
using UnityEngine;

namespace Code.GameServices.InputService
{
    public class ClickHandling : MonoBehaviour
    {
        public ClickHandlingType ClickHandlingType;
        public BuildingType BuildingType;
        public event Action OnClickHappend;
        public event Action OffClickHappend;
        public event Action<ClickHandling> MoveHappend;
        private Outline _outline;

        private void Start()
        {
            _outline = GetComponent<Outline>();
            _outline.OutlineWidth = 0f;
        }

        public void OnClick()
        {
            _outline.OutlineWidth = 5f;
            OnClickHappend?.Invoke();
        }

        public void OffClick()
        {
            _outline.OutlineWidth = 0f;
            OffClickHappend?.Invoke();
        }

        public void MoveUnit(ClickHandling building)
        {
            MoveHappend?.Invoke(building);
        }
    }
}