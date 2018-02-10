using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int _id;
    private string _name, _description, _flavor;
    private string _damage, _fireRate, _accuracy;
    private Rarity _rarity;
    private Sprite _icon;

    //keeping track of the item
    private bool initiated = false;

    // General properties
    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public string Flavor { get { return _flavor; } }
    public string Damage { get { return _damage; } }
    public string FireRate { get { return _fireRate; } }
    public string Accuracy { get { return _accuracy; } }
    public Sprite Icon { get { return _icon; } }
    public Rarity Rarity {  get { return _rarity; } }

    public Weapon() { }
    public Weapon(Weapon wep)
    {
        _id = wep.ID;
        _name = wep.Name;
        _description = wep.Description;
        _flavor = wep.Flavor;
        _icon = wep.Icon;
        _damage = wep.Damage;
        _fireRate = wep.FireRate;
        _accuracy = wep.Accuracy;
        _rarity = wep.Rarity;

    }
    public void Init(WeaponTemplate template)
    {
        _id = template.id;
        _name = template.name;
        _description = template.description;
        _flavor = template.flavor;
        _icon = template.icon;
        _damage = template.damage;
        _fireRate = template.fireRate;
        _accuracy = template.accuracy;
        _rarity = template.rarity;
        initiated = true;
    }
}

