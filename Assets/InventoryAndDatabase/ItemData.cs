using UnityEngine;

[System.Serializable]
public class ItemData
{
    public ItemData(Item item)
    {
        localID = item.localID;
        name = item.name;
        itemRarity = item.itemRarity;
        value = item.value;
        isStackable = item.isStackable;
        stackSize = item.stackSize;
        maxStack = item.maxStack;
        isConsumable = item.isConsumable;
        isEquipable = item.isEquipable;
        description = item.description;
        descriptionHeader = item.descriptionHeader;
       //worldObject = item.worldObject;
        itemLocation = item.itemLocation;
        itemType = item.itemType;
        weaponType = item.weaponType;
        consumableType = item.consumableType;
        primarySkill = item.primarySkill;
        secondarySkill = item.secondarySkill;
        itemSkills = item.itemSkills;
        damage = item.damage;
        minDamage = item.minDamage;
        maxDamage = item.maxDamage;
        AttackRate = item.AttackRate;
        Range = item.Range;
        Armor = item.Armor;
        Weight = item.Weight;
        FireResistance = item.FireResistance;
        ShockResistance = item.ShockResistance;
    }

    public static explicit operator ItemData(Item item)
    {
        return new ItemData(item);
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

    //public GameObject worldObject;

    public BiomeLocator itemLocation;
    public EquipmentType itemType;

    public WeaponType weaponType;

    public ConsumableType consumableType;

    //sprite to path
    public string icon_path;

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