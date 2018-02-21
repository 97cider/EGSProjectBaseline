using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GUISlot : MonoBehaviour,
                       IPointerExitHandler,
                       IPointerEnterHandler,
                       IPointerClickHandler
{
    public bool entered;
    public Inventory inventory;
    public int index;
    public Image image;


    public void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        entered = false;
        image = gameObject.GetComponent<Image>();

        //Assign this to whatever to change each image
        //i.sprite = <your sprite here>;

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

    public void changeImage(Sprite change_to)
    {
        image.sprite = change_to;
    }
}
