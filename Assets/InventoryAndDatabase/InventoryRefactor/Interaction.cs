using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Interaction : MonoBehaviour
    , IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
    , IBeginDragHandler
    , IDragHandler
    , IEndDragHandler
{
    //SpriteRenderer sprite;
    //Color target = Color.red;

    public Slot slot;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit"); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Dragging");
    }
}
