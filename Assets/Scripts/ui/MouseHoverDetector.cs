using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _isHoveringOver;

    public bool IsHoverOver()
    {
        return _isHoveringOver;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHoveringOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHoveringOver = false;
    }
}