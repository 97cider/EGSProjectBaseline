using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{

    public Item() { }

    public Item(ItemData itemData)
    {
        localID = itemData.localID;
        name = itemData.name;
        itemRarity = itemData.itemRarity;
        value = itemData.value;
        isStackable = itemData.isStackable;
        stackSize = itemData.stackSize;
        maxStack = itemData.maxStack;
        isConsumable = itemData.isConsumable;
        isEquipable = itemData.isEquipable;
        description = itemData.description;
        descriptionHeader = itemData.descriptionHeader;
        //worldObject = itemData.worldObject;
        itemLocation = itemData.itemLocation;
        itemType = itemData.itemType;
        weaponType = itemData.weaponType;
        consumableType = itemData.consumableType;
        primarySkill = itemData.primarySkill;
        secondarySkill = itemData.secondarySkill;
        itemSkills = itemData.itemSkills;
        damage = itemData.damage;
        minDamage = itemData.minDamage;
        maxDamage = itemData.maxDamage;
        AttackRate = itemData.AttackRate;
        Range = itemData.Range;
        Armor = itemData.Armor;
        Weight = itemData.Weight;
        FireResistance = itemData.FireResistance;
        ShockResistance = itemData.ShockResistance;
        
    }

    public static explicit operator Item(ItemData itemData)
    {
        return new Item(itemData);
    }

    public int localID;
    public string name;
    public ItemRarity itemRarity;
    public int value;
    public bool isStackable;
    public int stackSize;
    public int maxStack;
    public bool isConsumable;
    public bool isEquipable;
    public string description;
    public string descriptionHeader;
    public GameObject worldObject;
    //where the item can be found (should only used for crafting materials)
    public BiomeLocator itemLocation;
    public EquipmentType itemType;
    //if it is a weapon, it will have a weapon type
    public WeaponType weaponType;
    //if it is a consumable, it will have a consumable type
    public ConsumableType consumableType;
    public Sprite icon;
    //alright, so, since this is a template, and we dont want to do different
    //files for this kinda stuff, EVERYTHING is gonna be crammed here. IIRC since this is a 
    //template all the empty stuff will be unused.
    //also ill make it nullable just incase we want to display a 0 value to the screen 
    //well nullable is out the window becuase fuck gui scripts

    public WeaponSkill primarySkill, secondarySkill;
    public ItemSkill[] itemSkills;

    //Weapon Stats
    public int damage;
    public int minDamage;
    public int maxDamage;
    public int AttackRate;
    public int Range;

    //Armor Stats
    public int Armor;
    public int Weight;
    //totally just spitballin here, requires discussion
    public int FireResistance;
    public int ShockResistance;
    
}
