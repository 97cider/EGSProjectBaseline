using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OldSlot : Button
{
    public Image icon;
    public bool isOccupied;
    public InventoryManager im;

    public void activateEvent() { }

    public void Initialize()
    {
        Debug.Log("Generic Slot has been initialized");
        icon = gameObject.GetComponentInChildren<Image>();
    }


}