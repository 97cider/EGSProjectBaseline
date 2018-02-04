using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeTooltip : MonoBehaviour
{
    public InventoryManager im;
    public UpgradeSlot upgradeXREF;
    public Image icon;
    public Text itemName, flavor, description;

    public Sprite missing;

    public void DrawTooltip(UpgradeSlot newXref)
    {
        if (newXref.isOccupied)
        {
            //holy shit if this actually works
            //insert image of man holding CS degree
            gameObject.transform.parent.gameObject.SetActive(true);
            upgradeXREF = newXref;
            icon.sprite = newXref.upgrade.Icon;
            itemName.text = newXref.upgrade.Name;
            flavor.text = newXref.upgrade.Type;
            description.text = newXref.upgrade.Description;
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(true);
            icon.sprite = missing;
            itemName.text = "???";
            flavor.text = "??? ??? ??";
            description.text = "...";
        }
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
