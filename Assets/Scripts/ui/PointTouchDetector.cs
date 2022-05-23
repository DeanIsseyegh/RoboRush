using UnityEngine;
using UnityEngine.EventSystems;

namespace ui
{
    public class PointTouchDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _isPointDown;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPointDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPointDown = false;
        }

        public bool IsPressed()
        {
            return _isPointDown;
        }
    }
}