using System;
using Code.Buildings;
using Code.ShadersTools;
using UnityEngine;

namespace Code.GameServices.InputService
{
    public class ClickHandling : MonoBehaviour
    {
        [SerializeField] private DrawOutline _drawOutline;
        
        public ClickHandlingType ClickHandlingType;
        public BuildingType BuildingType;
        public event Action OnClickHappend;
        public event Action OffClickHappend;
        public event Action<ClickHandling> MoveHappend;


        public void OnClick()
        {
            _drawOutline.MakeVisible();
            OnClickHappend?.Invoke();
        }

        public void OffClick()
        {
            _drawOutline.MakeUnvisible();
            OffClickHappend?.Invoke();
        }

        public void MoveUnit(ClickHandling building)
        {
            MoveHappend?.Invoke(building);
        }
    }
}