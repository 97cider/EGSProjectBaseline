using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager manager;
    public List<WeaponTemplate> weaponDatabase = new List<WeaponTemplate>();
    public List<UpgradeTemplate> upgradeDatabase = new List<UpgradeTemplate>();

    public List<WeaponSlot> weaponSlots = new List<WeaponSlot>();
    public List<UpgradeSlot> upgradeSlots = new List<UpgradeSlot>();

    //inventory sections
    public GameObject weaponsSection, upgradesSection;

    //rarity colors
    public Color common, rare, epic, legenedary;

    //tooltip
    public WeaponTooltip weaponTooltip;
    public UpgradeTooltip upgradeTooltip;

    public WeaponSlot currentlyEquippedWeapon;
    public GameObject WeaponSlotPrefab, upgradeSlotPrefab, upgradeContainer, uiOverhead;
    //may be used to inherit data from a global persepctive, idk yet
    void Awake()
    {
        if (manager == null)
            manager = this;
        else if (manager != this)
            Destroy(gameObject);

        Initialize();
    }
    public void sendMessageToWeaponTooltip(WeaponSlot weaponSlot)
    {
        weaponTooltip.DrawTooltip(weaponSlot);
    }
    void populateLists()
    {
        //fill those slots famalam
        weaponSlots.AddRange(GameObject.FindObjectsOfType<WeaponSlot>());
        upgradeSlots.AddRange(GameObject.FindObjectsOfType<UpgradeSlot>());

        //because im wayyy to lazy for this shit
        weaponSlots.Reverse();
        upgradeSlots.Reverse();
    }
    private void Initialize()
    {
        weaponTooltip = GameObject.FindObjectOfType<WeaponTooltip>();
        upgradeTooltip = GameObject.FindObjectOfType<UpgradeTooltip>();
        if (weaponTooltip != null)
        {
            weaponTooltip.im = this;
            weaponTooltip.gameObject.transform.parent.gameObject.SetActive(false);
        }
        if (upgradeTooltip != null)
        {
            upgradeTooltip.im = this;
            upgradeTooltip.gameObject.transform.parent.gameObject.SetActive(false);
        }
        populateLists();
        //add our default starting weapon
        AddWeapon(0);
        //equip that shit boy
        getStartingWeapon();
        currentlyEquippedWeapon.EquipWeapon();
        upgradesSection.SetActive(false);
    }
    WeaponSlot GetUsableWeaponSlot()
    {
        foreach(var slot in weaponSlots)
        {
            if (!slot.isOccupied)
            {
                return slot;
            }
        }
        //THEY ATE ALL YO STORAGE SAUCE
        //well, its time to use object pooling and just create a new one
        var newSlot = Instantiate(WeaponSlotPrefab);
        newSlot.transform.parent = uiOverhead.transform;
        var temp = newSlot.GetComponent<WeaponSlot>();
        weaponSlots.Add(temp);
        return temp;
    }
    UpgradeSlot GetUsableUpgradeSlot()
    {
        foreach(var slot in upgradeSlots)
        {
            if (!slot.isOccupied)
            {
                return slot;
            }
        }
        //dynamic storage my dude
        var newSlot = Instantiate(upgradeSlotPrefab);
        newSlot.transform.parent = upgradeContainer.transform;
        var temp = newSlot.GetComponent<UpgradeSlot>();
        upgradeSlots.Add(temp);
        return temp;
    }
    UpgradeSlot GetStackedSlot(int stackableItem)
    {
        foreach (var slot in upgradeSlots)
        {
            if (slot.isOccupied)
            {
                if (slot.upgrade.ID == stackableItem && slot.upgrade.Stackable)
                {
                    return slot;
                }
            }
        }
        //if no stackable slot can be found, just get a new one
        return GetUsableUpgradeSlot();
    }
    private void getStartingWeapon()
    {
        foreach(var slots in weaponSlots)
        {
            if (slots.isOccupied)
            {
                currentlyEquippedWeapon = slots;
                return;
            }
        }
    }
    //this will add a weapon to an available slot, hopefully it will work
    public void AddWeapon(int id)
    {
        var slot = GetUsableWeaponSlot();
        Weapon toBeAdded = new Weapon();
        foreach(var weapon in weaponDatabase)
        {
            if(weapon.id == id)
            {
                toBeAdded.Init(weapon);
                Debug.Log("A WEAPON IS AVAILABLE" + toBeAdded.Name);
                break;
            }
        }
        if (toBeAdded.ID == id) {
            if (slot != null)
            {
                slot.AddItem(toBeAdded);
            }
        }
    }
    public void AddUpgrade(int id)
    {
        var slot = GetStackedSlot(id);
        Upgrade toBeAdded = new Upgrade();
        foreach(var upgrade in upgradeDatabase)
        {
            if(upgrade.id == id)
            {
                toBeAdded.Init(upgrade);
                break;
            }
        }
        if(toBeAdded.ID == id)
        {
            slot.AddUpgrade(toBeAdded);
        }
    }
}
