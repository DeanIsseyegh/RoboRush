using UnityEngine;
using UnityEngine.EventSystems;

namespace ui
{
    public class PointTouchDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
    {
        private bool _isPointDown;

        public void OnPointerDown(PointerEventData eventData)
        {
            // _isPointDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // _isPointDown = false;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointDown = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointDown = false;
        }

        public bool IsPressed()
        {
            return _isPointDown;
        }

    }
}