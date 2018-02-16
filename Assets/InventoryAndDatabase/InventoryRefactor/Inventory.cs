using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Inventory : MonoBehaviour {
    
    public List<Item> eqSlots;
    public List<Item> items;

    bool load_me = true;

    public void Start()
    {
        if(load_me)
            Load();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Inventory.dat");

        List<ItemData> serializedInv = new List<ItemData>();
        serializedInv.Capacity = 20;

        for(int i = 0; i < items.Capacity; i++)
        {
            serializedInv.Add((ItemData)items[i]);
        }

        bf.Serialize(file, serializedInv);
        file.Close();
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Inventory.dat", FileMode.Open);
        List<ItemData> data = (List<ItemData>)bf.Deserialize(file);

        Debug.Log(data.Capacity);

        for(int i = 0; i < data.Capacity; i++)
        {
            items[i] = (Item)data[i];
        }

        file.Close();
    }

    public void OnApplicationQuit()
    {
        Save();
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
