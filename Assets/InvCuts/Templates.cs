using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponTemplate
{
    //weapon properties
    public int id;
    public string name, description, flavor;
    public string damage, fireRate, accuracy;
    public Rarity rarity;
    public Sprite icon;

    public WeaponTemplate(string name)
    {
        this.name = name;
    }
    public WeaponTemplate() { }
}
[System.Serializable]
public class UpgradeTemplate
{
    //upgrade properties
    public int id;
    public string name, description, type;
    public Sprite icon;
    public bool stackable, getsConverted;
    public UpgradeEvent[] upgradeEvents;
    public UpgradeEvent[] onStackEvents;
    
    public UpgradeTemplate(string name)
    {
        this.name = name;
    }
    public UpgradeTemplate() { }
}
