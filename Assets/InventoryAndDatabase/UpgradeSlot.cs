using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeSlot : Slot {
    public Upgrade upgrade;
    public Text stackText;
    public int currentStack = 0;
    public new void activateEvent()
    {
        //what happens when the item is picked up
        for(int i = 0; i < upgrade.UpgradeEvents.Length; i++)
        {
            upgrade.UpgradeEvents[i].TriggerEvent();
        }
    }
    public void ActivateStackEvents()
    {
        for (int i = 0; i < upgrade.OnStackEvents.Length; i++)
        {
            upgrade.OnStackEvents[i].TriggerEvent();
        }
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
        stackText = gameObject.transform.GetChild(1).GetComponent<Text>();
    }
    public void AddUpgrade(Upgrade XREFupgrade)
    {
        upgrade = XREFupgrade;
        icon.sprite = XREFupgrade.Icon;
        isOccupied = true;
        //if it is stackable
        if (XREFupgrade.Stackable)
        {
            if (currentStack == 0)
            {
                Debug.Log("BASIC EVENT CALLED");
                currentStack++;
                activateEvent();
            }
            else {
                Debug.Log("STACK EVENT CALLED");
                currentStack++;
                ActivateStackEvents();
            }
        }
        else
        {
            currentStack++;
            Debug.Log("BASIC EVENT CALLED");
            activateEvent();
        }
        stackText.text = currentStack.ToString();
    }
}
