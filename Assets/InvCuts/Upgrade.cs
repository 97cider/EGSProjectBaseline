using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade: MonoBehaviour
{
    // General fields
    private int _id;
    private string _name, _description, _type;
    private Sprite _icon;
    private bool _stackable, _getsConverted;
    private UpgradeEvent[] _upgradeEvents;
    private UpgradeEvent[] _onStackEvents;

    //keeping track of the item
    private bool initiated = false;

    // General properties
    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public string Type { get { return _type; } }
    public bool Stackable { get { return _stackable; } }
    public Sprite Icon { get { return _icon; } }
    public bool GetsConverted { get { return _getsConverted; } }
    public UpgradeEvent[] UpgradeEvents { get { return _upgradeEvents; } }
    public UpgradeEvent[] OnStackEvents {  get { return _onStackEvents; } }

    public void Init(UpgradeTemplate template)
    {
        _id = template.id;
        _name = template.name;
        _description = template.description;
        _type = template.type;
        _stackable = template.stackable;
        _getsConverted = template.getsConverted;
        _icon = template.icon;
        _upgradeEvents = template.upgradeEvents;
        _onStackEvents = template.onStackEvents;
        initiated = true;
    }
}
