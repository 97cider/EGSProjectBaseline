using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items;
    public void AddItem(Item item)
    {
        items.Add(item);
    }
    public Item FindItem(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            //honestly if we need more space in the inventory, we can
            //make the ID a string and just do an int parse
            if(items[i].localID == id)
            {
                //create the item to the DB
                return CreateItem(items[i]);
            }
        }
        //if it fucks up
        return new Item();
    }
    public Item FindItemByName(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == name)
            {
                return CreateItem(items[i]);
            }
        }
        return new Item();
    }
	public Item CreateItem(Item item)
    {
        item = Inventory.CopyItem(item);
        //this is actually kinda neat, since there are public stats
        //we can actually do a lot of random item gen here
        //below is a pretty simple example
        //buuutt they cant be nullable, which is fine i guess
        item.damage = Random.Range(item.minDamage, item.maxDamage);
        return item;
    }


}
