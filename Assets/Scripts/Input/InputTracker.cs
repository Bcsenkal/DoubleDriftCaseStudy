using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UnityEngine.EventSystems;
public class InputTracker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.Instance.ONOnMouseDown(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventManager.Instance.ONOnMouseUp(eventData.position);
    }
}
