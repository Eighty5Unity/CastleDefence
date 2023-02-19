using UnityEngine;

namespace Code.GameServices.InputService
{
    public class InputService : MonoBehaviour
    {
        private ClickHandling _newClickHandling;
        private ClickHandling _oldClickHandling;

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                

                if(Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.transform.TryGetComponent<ClickHandling>(out _newClickHandling))
                    {
                        _oldClickHandling?.OffClick();
                    
                        if (_newClickHandling.ClickHandlingType == ClickHandlingType.Unit)
                        {
                            _newClickHandling.OnClick();
                        }
                        else if (_newClickHandling.ClickHandlingType == ClickHandlingType.Building && _oldClickHandling == null)
                        {
                            _newClickHandling.OnClick();
                        }
                        else if (_newClickHandling.ClickHandlingType == ClickHandlingType.Building && _oldClickHandling.ClickHandlingType == ClickHandlingType.Building)
                        {
                            _newClickHandling.OnClick();
                        }
                        else if (_newClickHandling.ClickHandlingType == ClickHandlingType.Building && _oldClickHandling.ClickHandlingType == ClickHandlingType.Unit)
                        {
                            _oldClickHandling.MoveUnit(_newClickHandling);
                            _oldClickHandling = null;
                            return;
                        }
                        
                        _oldClickHandling = _newClickHandling;
                    }
                }
            }
        }
    }
}