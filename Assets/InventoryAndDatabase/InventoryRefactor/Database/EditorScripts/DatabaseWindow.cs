using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DatabaseWindow : EditorWindow {
    //this whole script is gonna be real lazy
    //im just gonna pretty much look up how to do all of this and then 
    //just cram it into here, because tbh i have no idea how this gon work
    //with custom classes and shit
    Item item = new Item();
    EquipmentType equipType;
    WeaponType wepType;
    ItemToGenerate itemType;

    static ItemDatabase itemDB;
    Item ItemToEdit;
    SerializedObject obj;
    private Vector2 scrollPos;

    private enum WindowAction
    {
        createItem,
        updateItem
    }
    private WindowAction windowAction;
    [MenuItem ("Database/Items")]
    static void Init()
    {

        //this is kinda the only reason why i think this is a good idea
        itemDB = (ItemDatabase)Resources.Load("ItemDatabase", typeof(ItemDatabase)) as ItemDatabase;
        EditorWindow.GetWindow(typeof(DatabaseWindow));
        EditorWindow.GetWindow(typeof(DatabaseWindow)).minSize = new Vector2(200, 400);
    }
    void OnEnable()
    {
        obj = new SerializedObject(this);
        itemDB = (ItemDatabase)Resources.Load("ItemDatabase", typeof(ItemDatabase)) as ItemDatabase;
    }
    void OnInspectorUpdate()
    {
        itemDB = (ItemDatabase)Resources.Load("ItemDatabase", typeof(ItemDatabase)) as ItemDatabase;
    }
    //god FUCK editor scripts
    void OnGUI()
    {
        obj.Update();
        GUILayout.Space(25);
        //GUILayout.BeginVertical();
        //this should prolly create an item
        GUI.color = Color.cyan;
        if(GUILayout.Button("Create Ibbems", GUILayout.Width(400)))
        {
            windowAction = WindowAction.createItem;
            item = new Item();
        }
        GUI.color = Color.magenta;
        if (GUILayout.Button("Update Ibbems", GUILayout.Width(400)))
        {
            windowAction = WindowAction.updateItem;
        }
        GUI.color = Color.white;

        GUILayout.Space(25);
        if(windowAction == WindowAction.createItem)
        {
            CreateItem();
        }
        else if(windowAction == WindowAction.updateItem)
        {
            UpdateItems();
        }

        obj.ApplyModifiedProperties();
        //alright if the documentation is correct this will actual save some changes
        //again im kinda just spitballin right now
        if (GUI.changed)
        {
            //DIRTY DAN
            EditorUtility.SetDirty(itemDB);
            //Save the item database prefab
            PrefabUtility.SetPropertyModifications(PrefabUtility.GetPrefabObject(itemDB), PrefabUtility.GetPropertyModifications(itemDB));
        }
    }
    void CreateItem()
    {
        itemType = (ItemToGenerate)EditorGUILayout.EnumPopup("Item Type: ", itemType);
        item.name = EditorGUILayout.TextField("Item name:", item.name);
       // //GUILayout.BeginVertical();
        item.itemRarity = (ItemRarity)EditorGUILayout.EnumPopup("Item Rarity: ", item.itemRarity);
        item.itemLocation = (BiomeLocator)EditorGUILayout.EnumPopup("Item Location: ", item.itemLocation);
        ////GUILayout.EndHorizontal();
        ////GUILayout.BeginVertical();
        item.value = EditorGUILayout.IntField("Item Value: ", item.value);
        item.descriptionHeader = EditorGUILayout.TextField("Item Tooltip: ", item.descriptionHeader);
        ////GUILayout.EndHorizontal();
        ////GUILayout.BeginVertical();
        item.description = EditorGUILayout.TextField("Item Description: ", item.description);
        item.icon = (Sprite)EditorGUILayout.ObjectField("Item Icon:", item.icon, typeof(Sprite), false);
        ////GUILayout.EndHorizontal();
        ////GUILayout.BeginVertical();
        item.worldObject = (GameObject)EditorGUILayout.ObjectField("Item World Object: ", item.worldObject, typeof(GameObject), false);
        //if webbin 
        ////GUILayout.EndHorizontal();
        if (itemType == ItemToGenerate.Weapon)
        {
          //  //GUILayout.BeginVertical();
            item.isEquipable = EditorGUILayout.Toggle("Is Equippable: ", item.isEquipable);
            item.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Webbin Type: ", item.weaponType);
           // //GUILayout.EndHorizontal();
            ////GUILayout.BeginVertical();
            item.maxDamage = EditorGUILayout.IntField("Weapon Max Damage: ", item.maxDamage);
            item.minDamage = EditorGUILayout.IntField("Weapon Min Damage; ", item.minDamage);
            ////GUILayout.EndHorizontal();
            ////GUILayout.BeginVertical();
            item.AttackRate = EditorGUILayout.IntField("Weapon Attack Rate; ", item.AttackRate);
            item.Range = EditorGUILayout.IntField("Weapon Range: ", item.Range);
            ////GUILayout.EndHorizontal();
            ////GUILayout.BeginVertical();
            //we need to do a custom editor gui for skills, ill work on that once the code is done
            //but for now fuck that lol
            item.primarySkill = (WeaponSkill)EditorGUILayout.ObjectField("Primary Skill:", item.primarySkill, typeof(WeaponSkill), false);
            item.secondarySkill = (WeaponSkill)EditorGUILayout.ObjectField("Secondary Skill:", item.secondarySkill, typeof(WeaponSkill), false);
            ////GUILayout.EndHorizontal();
        }
        else if(itemType == ItemToGenerate.armor)
        {
            ////GUILayout.BeginVertical();
            item.isEquipable = EditorGUILayout.Toggle("Is Equippable: ", item.isEquipable);
            item.Armor = EditorGUILayout.IntField("Armor Value:", item.Armor);
            item.Weight = EditorGUILayout.IntField("Armor Weight: ", item.Weight);
            ////GUILayout.EndHorizontal();
            ////GUILayout.BeginVertical();
            item.FireResistance = EditorGUILayout.IntField("Armor Fire Resistance: ", item.FireResistance);
            item.ShockResistance = EditorGUILayout.IntField("Armor Shock Resistance: ", item.ShockResistance);
            ////GUILayout.EndHorizontal();
            ////GUILayout.BeginVertical();
            //this should work but if it doesnt im removing it asap
            //im just gonna comment it out for now (SCARED)
            /*
            SerializedProperty itemSkills = obj.FindProperty("itemSkills");
            EditorGUILayout.PropertyField(itemSkills, true);
            */
            //that should be it 
            ////GUILayout.EndHorizontal();
        }
        else if(itemType == ItemToGenerate.other)
        {
            ////GUILayout.BeginVertical();
            item.isStackable = EditorGUILayout.Toggle("Is Stackable:", item.isStackable);
            if (item.isStackable)
            {
                item.maxStack = EditorGUILayout.IntField("Stack Size: ", item.stackSize);
            }
            ////GUILayout.EndHorizontal();
        }
        if(GUILayout.Button("Save Changes"))
        {
            item.localID = itemDB.items.Count;
            itemDB.AddItem(item);
            item = new Item();
        }
    }
    void UpdateItems()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        for(int i = 0; i < itemDB.items.Count; i++)
        {
            if(itemDB.items[i] != ItemToEdit)
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if(GUILayout.Button("Edit", "label", GUILayout.Width(80)))
                {
                    ItemToEdit = itemDB.items[i];
                }
                if (GUILayout.Button(itemDB.items[i].name, GUILayout.Width(280)))
                {
                    ItemToEdit = itemDB.items[i];
                }
                GUI.color = Color.red;
                //delete the item from the database
                if(GUILayout.Button("X", GUILayout.Width(40)))
                {
                    itemDB.items.Remove(itemDB.items[i]);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            //press the same buttons to close the item window
            else
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Close", "label", GUILayout.Width(80)))
                {
                    ItemToEdit = null;
                    return;
                }
                if (GUILayout.Button(itemDB.items[i].name, GUILayout.Width(280)))
                {
                    ItemToEdit = null;
                    return;
                }
                GUI.color = Color.red;
                if(GUILayout.Button("X", GUILayout.Width(40)))
                {
                    itemDB.items.Remove(itemDB.items[i]);
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUI.color = Color.white;

                //now we have to edit the item stats
                ItemToEdit.name = EditorGUILayout.TextField("Item name:", ItemToEdit.name);
                //GUILayout.BeginVertical();
                ItemToEdit.itemRarity = (ItemRarity)EditorGUILayout.EnumPopup("Item Rarity: ", ItemToEdit.itemRarity);
                ItemToEdit.itemLocation = (BiomeLocator)EditorGUILayout.EnumPopup("Item Location: ", ItemToEdit.itemLocation);
                //GUILayout.EndHorizontal();
                //GUILayout.BeginVertical();
                ItemToEdit.value = EditorGUILayout.IntField("Item Value: ", ItemToEdit.value);
                ItemToEdit.descriptionHeader = EditorGUILayout.TextField("Item Tooltip: ", ItemToEdit.descriptionHeader);
                //GUILayout.EndHorizontal();
                //GUILayout.BeginVertical();
                ItemToEdit.description = EditorGUILayout.TextField("Item Description: ", ItemToEdit.description);
                ItemToEdit.icon = (Sprite)EditorGUILayout.ObjectField("Item Icon:", ItemToEdit.icon, typeof(Sprite), false);
                //GUILayout.EndHorizontal();
                //GUILayout.BeginVertical();
                ItemToEdit.worldObject = (GameObject)EditorGUILayout.ObjectField("Item World Object: ", ItemToEdit.worldObject, typeof(GameObject), false);
                //if webbin 
                //GUILayout.EndHorizontal();
                if (ItemToEdit.itemType == EquipmentType.Weapon)
                {
                    //GUILayout.BeginVertical();
                    ItemToEdit.isEquipable = EditorGUILayout.Toggle("Is Equippable: ", ItemToEdit.isEquipable);
                    ItemToEdit.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Webbin Type: ", ItemToEdit.weaponType);
                    //GUILayout.EndHorizontal();
                    //GUILayout.BeginVertical();
                    ItemToEdit.maxDamage = EditorGUILayout.IntField("Weapon Max Damage: ", ItemToEdit.maxDamage);
                    ItemToEdit.minDamage = EditorGUILayout.IntField("Weapon Min Damage; ", ItemToEdit.minDamage);
                    //GUILayout.EndHorizontal();
                    //GUILayout.BeginVertical();
                    ItemToEdit.AttackRate = EditorGUILayout.IntField("Weapon Attack Rate; ", ItemToEdit.AttackRate);
                    ItemToEdit.Range = EditorGUILayout.IntField("Weapon Range: ", ItemToEdit.Range);
                    //GUILayout.EndHorizontal();
                    //GUILayout.BeginVertical();
                    //we need to do a custom editor gui for skills, ill work on that once the code is done
                    //but for now fuck that lol
                    ItemToEdit.primarySkill = (WeaponSkill)EditorGUILayout.ObjectField("Primary Skill:", ItemToEdit.primarySkill, typeof(WeaponSkill), false);
                    ItemToEdit.secondarySkill = (WeaponSkill)EditorGUILayout.ObjectField("Secondary Skill:", ItemToEdit.secondarySkill, typeof(WeaponSkill), false);
                    //GUILayout.EndHorizontal();
                }
                else if (ItemToEdit.itemType == EquipmentType.Armor)
                {
                    //GUILayout.BeginVertical();
                    ItemToEdit.isEquipable = EditorGUILayout.Toggle("Is Equippable: ", ItemToEdit.isEquipable);
                    ItemToEdit.Armor = EditorGUILayout.IntField("Armor Value:", ItemToEdit.Armor);
                    ItemToEdit.Weight = EditorGUILayout.IntField("Armor Weight: ", ItemToEdit.Weight);
                    //GUILayout.EndHorizontal();
                    //GUILayout.BeginVertical();
                    ItemToEdit.FireResistance = EditorGUILayout.IntField("Armor Fire Resistance: ", ItemToEdit.FireResistance);
                    ItemToEdit.ShockResistance = EditorGUILayout.IntField("Armor Shock Resistance: ", ItemToEdit.ShockResistance);
                    //GUILayout.EndHorizontal();
                    ////GUILayout.BeginVertical();
                    //this should work but if it doesnt im removing it asap
                    //im just gonna comment it out for now (SCARED)
                    /*
                    SerializedProperty itemSkills = obj.FindProperty("itemSkills");
                    EditorGUILayout.PropertyField(itemSkills, true);
                    */
                    //that should be it 
                    ////GUILayout.EndHorizontal();
                }
                else if (ItemToEdit.itemType == EquipmentType.Other)
                {
                    //GUILayout.BeginVertical();
                    ItemToEdit.isStackable = EditorGUILayout.Toggle("Is Stackable:", ItemToEdit.isStackable);
                    if (item.isStackable)
                    {
                        ItemToEdit.maxStack = EditorGUILayout.IntField("Stack Size: ", ItemToEdit.stackSize);
                    }
                    //GUILayout.EndHorizontal();
                }
            }
        }
        GUILayout.EndScrollView();
    }
}
