using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Vector2 startPoint;

    [SerializeField]
    Vector2 endPoint;

    [SerializeField]
    bool drag;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.pressPosition;
        SendMessage("OnBeginDragMsg", startPoint, SendMessageOptions.DontRequireReceiver);
    }

    public void OnDrag(PointerEventData eventData)
    {
        drag = true;
        endPoint = eventData.position;
        SendMessage("OnDragMsg", endPoint, SendMessageOptions.DontRequireReceiver);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        drag = false;

        //Debug.Log("DraggScript-startPoint=" + startPoint + " endpoint=" + endPoint);
    }
}
