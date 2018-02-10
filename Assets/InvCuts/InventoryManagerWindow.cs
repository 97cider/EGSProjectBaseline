using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(InventoryManager))]
public class InventoryManagerWindow : Editor
{
    public bool showWeapons = true;
    public bool showUpgrades = true;
    public bool showUpgradeEvents = true;
    SerializedObject manager;
    static SerializedProperty weaponList;
    static SerializedProperty upgradeList;
    InventoryManager foundManager { get { var manag = FindObjectOfType<InventoryManager>(); return manag == null ? null : manag; } }
    delegate void DrawEditor();
    DrawEditor drawFunction;
    private void OnEnable()
    {
        Initialize();
    }
    void Initialize()
    {
        if (foundManager != null)
        {
            manager = new SerializedObject(foundManager);
            weaponList = manager.FindProperty("weaponDatabase");
            upgradeList = manager.FindProperty("upgradeDatabase");
        }
    }
    public override void OnInspectorGUI()
    {
        if(manager != null)
        {
            manager.Update();
            serializedObject.Update();
        }
        GUILayout.Label("-----------------------------WEAPONS-----------------------------");
        #region weapons
        showWeapons = EditorGUILayout.Foldout(showWeapons, "Weapons");
        if (showWeapons)
        {
            Color defaultColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add Weapon"))
            {
                weaponList.arraySize++;
            }
            GUI.backgroundColor = defaultColor;
            for (int i = 0; i < weaponList.arraySize; i++)
            {
                Color baseColor = GUI.backgroundColor;
                SerializedProperty MyListRef = weaponList.GetArrayElementAtIndex(i);
                SerializedProperty name = MyListRef.FindPropertyRelative("name");
                SerializedProperty id = MyListRef.FindPropertyRelative("id");
                SerializedProperty desc = MyListRef.FindPropertyRelative("description");
                SerializedProperty flavor = MyListRef.FindPropertyRelative("flavor");
                SerializedProperty icon = MyListRef.FindPropertyRelative("icon");
                GUILayout.Label(weaponList.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue);
                EditorGUILayout.LabelField(i.ToString());
                name.stringValue = EditorGUILayout.TextField("Weapon Name", name.stringValue);
                id.intValue = EditorGUILayout.IntField("Weapon ID", i);
                flavor.stringValue = EditorGUILayout.TextField("Flavor", flavor.stringValue);
                desc.stringValue = EditorGUILayout.TextField("Description", desc.stringValue);
                icon.objectReferenceValue = EditorGUILayout.ObjectField("Icon", icon.objectReferenceValue, typeof(Sprite), true);
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Delete Item"))
                {
                    weaponList.DeleteArrayElementAtIndex(i);
                }
                GUI.backgroundColor = baseColor;
            }
        }
        #endregion
        GUILayout.Label("-----------------------------UPGRADES----------------------------");
        #region upgrades
        showUpgrades = EditorGUILayout.Foldout(showUpgrades, "Upgrades");
        if (showUpgrades)
        {
            Color defaultColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("Add Upgrade"))
            {
                upgradeList.arraySize++;
            }
            GUI.backgroundColor = defaultColor;
            for (int i = 0; i < upgradeList.arraySize; i++)
            {
                Color baseColor = GUI.backgroundColor;
                SerializedProperty MyListRef = upgradeList.GetArrayElementAtIndex(i);
                SerializedProperty name = MyListRef.FindPropertyRelative("name");
                SerializedProperty id = MyListRef.FindPropertyRelative("id");
                SerializedProperty desc = MyListRef.FindPropertyRelative("description");
                SerializedProperty flavor = MyListRef.FindPropertyRelative("type");
                SerializedProperty icon = MyListRef.FindPropertyRelative("icon");
                SerializedProperty upgradeEventArray = MyListRef.FindPropertyRelative("upgradeEvents");
                GUILayout.Label(upgradeList.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue);
                EditorGUILayout.LabelField(i.ToString());
                name.stringValue = EditorGUILayout.TextField("Upgrade Name", name.stringValue);
                id.intValue = EditorGUILayout.IntField("Upgrade ID", i);
                flavor.stringValue = EditorGUILayout.TextField("Flavor", flavor.stringValue);
                desc.stringValue = EditorGUILayout.TextField("Description", desc.stringValue);
                icon.objectReferenceValue = EditorGUILayout.ObjectField("Icon", icon.objectReferenceValue, typeof(Sprite), true);
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Delete Item"))
                {
                    upgradeList.DeleteArrayElementAtIndex(i);
                }
                GUI.backgroundColor = baseColor;
            }
        }
        #endregion
        manager.ApplyModifiedProperties();
        serializedObject.ApplyModifiedProperties();
        DrawDefaultInspector();

    }
    public void DrawUpgrade()
    {
        if (GUILayout.Button("Add Upgrade"))
        {
            upgradeList.arraySize++;
        }
    }
    private void ItemSlotGUI(int index)
    {
    }
}

[CustomEditor(typeof(WeaponSlot))]
public class WeaponSlotWindow : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}

[CustomEditor(typeof(UpgradeSlot))]
public class UpgradeSlotWindow : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}