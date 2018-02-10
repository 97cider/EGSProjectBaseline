using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponSlot : OldSlot{
    public bool isEquipped;
    public Weapon weapon;
    public Text damage, firerate, accuracy;
    public void EquipWeapon()
    {
        //unequip the old weapon
        im.currentlyEquippedWeapon.UnequipWeapon();
        //and equip the new one
        im.currentlyEquippedWeapon = this;
        isEquipped = true;
    }
    public void UnequipWeapon()
    {
        isEquipped = false;
    }
    void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        //there should only be one, so it can take the first manager
        im = FindObjectOfType<InventoryManager>();
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        damage = gameObject.transform.GetChild(1).GetComponent<Text>();
        firerate = gameObject.transform.GetChild(2).GetComponent<Text>();
        accuracy = gameObject.transform.GetChild(3).GetComponent<Text>();
    }
    public void AddItem(Weapon item)
    {
        var temp = new Weapon(item);
        Debug.Log("TEMP" + temp.Name);
        weapon = temp;
        Debug.Log("CURR" + weapon.Name);
        icon.sprite = weapon.Icon;
        damage.text = weapon.Damage;
        firerate.text = weapon.FireRate;
        accuracy.text = weapon.Accuracy;
        isOccupied = true;
    }
    public void showTooltip()
    {
        im.sendMessageToWeaponTooltip(this);
    }
    
}
