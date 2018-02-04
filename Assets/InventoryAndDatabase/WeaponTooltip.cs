using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponTooltip : MonoBehaviour {
    public InventoryManager im;
    public WeaponSlot weaponXREF;
    public Image icon;
    public Text itemName, flavor, description;

    public Button equip;
    public Sprite missing;

    public void DrawTooltip(WeaponSlot newXref)
    {
        if (newXref.isOccupied)
        {
            //holy shit if this actually works
            //insert image of man holding CS degree
            gameObject.transform.parent.gameObject.SetActive(true);
            weaponXREF = newXref;
            icon.sprite = newXref.weapon.Icon;
            itemName.text = newXref.weapon.Name;
            flavor.text = newXref.weapon.Flavor;
            description.text = newXref.weapon.Description;
            equip.interactable = true;
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(true);
            icon.sprite = missing;
            itemName.text = "???";
            flavor.text = "??? ??? ??";
            description.text = "...";
            equip.interactable = false;
        }
    }
    public void triggerEquip()
    {
        weaponXREF.EquipWeapon();
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
