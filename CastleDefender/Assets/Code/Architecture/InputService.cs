using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Architecture
{
    public class InputService : MonoBehaviour, IPointerClickHandler
    {
        private ClickHandling _eventData;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _eventData?.OffClick();
            _eventData = eventData.pointerEnter.GetComponent<ClickHandling>();
            _eventData.OnClick();
        }
    }
}