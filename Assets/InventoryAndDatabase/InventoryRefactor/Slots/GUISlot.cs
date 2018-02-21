using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUISlot : MonoBehaviour,
                       IPointerExitHandler,
                       IPointerEnterHandler,
                       IPointerClickHandler
{
    public bool entered;
    public Inventory inventory;
    public int index;

    public void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        entered = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        entered = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        entered = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(entered)
        {
            //Debug.Log(inventory.items[index].name);
            Debug.Log(eventData.button == PointerEventData.InputButton.Right);
        }
    }
}
