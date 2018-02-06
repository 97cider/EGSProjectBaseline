using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InventoryWindow : EditorWindow {
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
    [MenuItem ("Inventory/Items")]
    static void Init()
    {

        //this is kinda the only reason why i think this is a good idea
        itemDB = (ItemDatabase)Resources.Load("ItemDatabase", typeof(ItemDatabase)) as ItemDatabase;
        EditorWindow.GetWindow(typeof(InventoryWindow));
        EditorWindow.GetWindow(typeof(InventoryWindow)).minSize = new Vector2(200, 400);
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
        GUILayout.BeginVertical();
        //this should prolly create an item
        GUI.color = Color.cyan;
        if(GUILayout.Button("Create Ibbems"))
        {
            windowAction = WindowAction.createItem;
            item = new Item();
        }
        GUI.color = Color.magenta;
        if (GUILayout.Button("Update Ibbems"))
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
            //call an update item function
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
        GUILayout.BeginVertical();
        item.itemRarity = (ItemRarity)EditorGUILayout.EnumPopup("Item Rarity: ", item.itemRarity);
        item.itemLocation = (BiomeLocator)EditorGUILayout.EnumPopup("Item Location: ", item.itemLocation);
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical();
        item.value = EditorGUILayout.IntField("Item Value: ", item.value);
        item.descriptionHeader = EditorGUILayout.TextField("Item Tooltip: ", item.descriptionHeader);
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical();
        item.description = EditorGUILayout.TextField("Item Description: ", item.description);
        item.icon = (Sprite)EditorGUILayout.ObjectField("Item Icon:", item.icon, typeof(Sprite), false);
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical();
        item.worldObject = (GameObject)EditorGUILayout.ObjectField("Item World Object: ", item.worldObject, typeof(GameObject), false);
        //if webbin 
        GUILayout.EndHorizontal();
        if (itemType == ItemToGenerate.Weapon)
        {
            GUILayout.BeginVertical();
            item.isEquipable = EditorGUILayout.Toggle("Is Equippable: ", item.isEquipable);
            item.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Webbin Type: ", item.weaponType);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            item.maxDamage = EditorGUILayout.IntField("Weapon Max Damage: ", item.maxDamage);
            item.minDamage = EditorGUILayout.IntField("Weapon Min Damage; ", item.minDamage);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            item.AttackRate = EditorGUILayout.IntField("Weapon Attack Rate; ", item.AttackRate);
            item.Range = EditorGUILayout.IntField("Weapon Range: ", item.Range);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            //we need to do a custom editor gui for skills, ill work on that once the code is done
            //but for now fuck that lol
            item.primarySkill = (WeaponSkill)EditorGUILayout.ObjectField("Primary Skill:", item.primarySkill, typeof(WeaponSkill), false);
            item.secondarySkill = (WeaponSkill)EditorGUILayout.ObjectField("Secondary Skill:", item.secondarySkill, typeof(WeaponSkill), false);
            GUILayout.EndHorizontal();
        }
        else if(itemType == ItemToGenerate.armor)
        {
            GUILayout.BeginVertical();
            item.isEquipable = EditorGUILayout.Toggle("Is Equippable: ", item.isEquipable);
            item.Armor = EditorGUILayout.IntField("Armor Value:", item.Armor);
            item.Weight = EditorGUILayout.IntField("Armor Weight: ", item.Weight);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            item.FireResistance = EditorGUILayout.IntField("Armor Fire Resistance: ", item.FireResistance);
            item.ShockResistance = EditorGUILayout.IntField("Armor Shock Resistance: ", item.ShockResistance);
            GUILayout.EndHorizontal();
            //GUILayout.BeginVertical();
            //this should work but if it doesnt im removing it asap
            //im just gonna comment it out for now (SCARED)
            /*
            SerializedProperty itemSkills = obj.FindProperty("itemSkills");
            EditorGUILayout.PropertyField(itemSkills, true);
            */
            //that should be it 
            //GUILayout.EndHorizontal();
        }
        else if(itemType == ItemToGenerate.other)
        {
            GUILayout.BeginVertical();
            item.isStackable = EditorGUILayout.Toggle("Is Stackable:", item.isStackable);
            if (item.isStackable)
            {
                item.maxStack = EditorGUILayout.IntField("Stack Size: ", item.stackSize);
            }
            GUILayout.EndHorizontal();
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

    }
}
