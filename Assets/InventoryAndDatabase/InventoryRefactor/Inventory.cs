using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Reflection;

public class Inventory : MonoBehaviour {
    public int inv_size;
    public int equip_slots;
    
    public List<EquipmentSlot> eqSlots;
    public List<InventorySlot> items;

    //public HandSlot left;
    //public HandSlot right;


    //this might be obsolete, as it would prolly work better to make it equal 
    //to the AddStackedItem code for consistency and similarity
    public InventorySlot GetUsableSlot()
    {
        //alright this is sorta up for debate
        for(int i = 0; i < items.Count; i++)
        {
            if (!items[i].containsItem)
            {
                return items[i];
            }
        }
        //no item slot found
        return null;
    }
    //unfortunately i think this is the safest route to go
    //im pretty hyped to see if this works
    public bool AddStackedItem(Item item)
    {
        //alright this is sorta up for debate
        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].containsItem && items[i].item.name == item.name)
            {
                //max stack check and calcs
                int c = items[i].item.stackSize + item.stackSize;
                if(c <= items[i].item.maxStack)
                {
                    items[i].item.stackSize = c;
                    items[i].stackSize = c;
                    return true;
                }
                else if(c > items[i].item.maxStack)
                {
                    //well we gotta split some stacks now
                    //honestly adrenaline is kickin in and pretty much dictating how this works
                    //so if it doesnt then we can just remove it
                    Item temp = CopyItem(item);
                    temp.stackSize = c - items[i].item.maxStack;
                    //we should do some error checking and stuff i guess
                    AddItem(temp);
                    items[i].item.stackSize = items[i].item.maxStack;
                }
                return items[i];
            }
            if (i == items.Count - 1)
            {
                //no stack found, add it to the inventory
                //god this does seem a little unoptimized but whatevs
                AddItem(item);
            }
        }
        //no item slot found
        return false;
    }
    //ignores if an item is stackable (i guess????)
    //it kinda sucks, but i guess with stacks and stuff its probably the better option
    public bool AddItem(Item item)
    {
        //alright this is sorta up for debate
        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].containsItem)
            {
                //we have found a slot lets add an item and update some variables
                items[i].item = CopyItem(item);
                items[i].stackSize = item.stackSize;
                items[i].containsItem = true;
                return true;
            }
        }
        //no open slot was found, we can probably drop an item or something
        return false;
    }
    public bool EquipItem(Item item)
    {
        //if an item is armor or a webbon then we should find an easy was to equip it automatically
        for(int i  = 0; i < eqSlots.Count; i++)
        {
            //make it so that you can only equip a helmet in a helmet slot 
            if(item.isEquipable && item.itemType == eqSlots[i].equipmentType)
            {
                if (eqSlots[i].containsItem)
                {
                    //duplicate the old item and send it back to the inventory
                    Item temp = CopyItem(eqSlots[i].item);
                    AddItem(temp);
                }
                //this will prolly just overwrite the item
                //i dont see a problem with this, unless networking comes into play
                //as i think you can dupe items this way but whatevs
                eqSlots[i].item = CopyItem(item);
                eqSlots[i].containsItem = true;
                return true;
            }
        }
        return false;
    }
    public static Item CopyItem(Item obj)
    {
        if (obj == null)
        {
            //oof
            return null;
        }
        GameObject oj = obj.worldObject;
        Sprite tempIcon = obj.icon;
        obj.icon = null;
        //HERE COMES THE REFLECTION GANG LETS GOOOOOOOOOOOOOOO
        Item i = (Item)ReflectProcess(obj);
        i.worldObject = oj;
        i.icon = tempIcon;
        obj.worldObject = oj;
        obj.icon = tempIcon;
        return i;
    }

    static object ReflectProcess(object obj)
    {
        if(obj == null)
        {
            //oof
            return null;
        }
        Type type = obj.GetType();
        if(type.IsValueType || type == typeof(string))
        {
            return obj;
        }
        //used for item skills and other array fields
        else if (type.IsArray)
        {
            Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
            var array = obj as Array;
            Array copiedArray = Array.CreateInstance(elementType, array.Length);
            for(int i = 0; i < array.Length; i++)
            {
                copiedArray.SetValue(ReflectProcess(array.GetValue(i)), i);
            }
            return Convert.ChangeType(copiedArray, obj.GetType());
        }
        //used for item/weapon skill classess and shit
        else if (type.IsClass)
        {
            object instance = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(FieldInfo field in fields)
            {
                object fieldVal = field.GetValue(obj);
                if (fieldVal == null)
                {
                    //continue i guess????
                    //with the item templates we have alot of nulls
                    //but idk if we want to do anything with it
                    continue;
                }
                field.SetValue(instance, ReflectProcess(fieldVal));
            }
            return instance;
        }
        else
        {
            //fuck
            //might occur if using tuples or structs but idk
            throw new ArgumentException("what tf just happened (unknown type)");
        }
    }
}
