using UnityEngine;

namespace Code.Architecture
{
    public class ClickHandling : MonoBehaviour
    {
        private Outline _outline;

        private void Start()
        {
            _outline = GetComponent<Outline>();
            _outline.OutlineWidth = 0f;
        }

        public void OnClick()
        {
            _outline.OutlineWidth = 5f;
        }

        public void OffClick()
        {
            _outline.OutlineWidth = 0f;
        }
    }
}