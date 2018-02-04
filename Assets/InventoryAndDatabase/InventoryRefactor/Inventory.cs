using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Reflection;

public class Inventory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
