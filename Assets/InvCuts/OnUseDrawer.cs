using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(UpgradeEvent))]
public class OnUseDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect temp = position;
        temp.height = 16;

        SerializedProperty target = property.FindPropertyRelative("target");
        SerializedProperty eventName = property.FindPropertyRelative("eventName");
        SerializedProperty selectedIndex = property.FindPropertyRelative("selectedIndex");
        // Possible values
        SerializedProperty Bool = property.FindPropertyRelative("Bool");
        SerializedProperty Float = property.FindPropertyRelative("Float");
        SerializedProperty String = property.FindPropertyRelative("String");
        SerializedProperty Int = property.FindPropertyRelative("Int");
        SerializedProperty Vector3Val = property.FindPropertyRelative("Vector3Val");
        SerializedProperty Vector2Val = property.FindPropertyRelative("Vector2Val");
        SerializedProperty RectVal = property.FindPropertyRelative("rectVal");
        SerializedProperty color = property.FindPropertyRelative("color");
        SerializedProperty Uobject = property.FindPropertyRelative("Uobject");

        SerializedProperty valueType = property.FindPropertyRelative("valueType");
        SerializedProperty targetComponent = property.FindPropertyRelative("targetComponent");

        // Target
        EditorGUILayout.PropertyField(target, GUIContent.none);
        if (target.objectReferenceValue == null)
            return;

        // MethodSelection
        GameObject targetGO = target.objectReferenceValue as GameObject;
        temp.y += 16;
        Type[] types;
        string[] functions;
        var methods = GetMethods(targetGO, out functions, out types);
        if (selectedIndex.intValue + 1 > methods.Length)
            selectedIndex.intValue = 0;
        selectedIndex.intValue = EditorGUILayout.Popup(selectedIndex.intValue, functions);
        if (methods[selectedIndex.intValue] != null)
            eventName.stringValue = methods[selectedIndex.intValue].Name;
        else
            eventName.stringValue = "none";

        if (eventName.stringValue == "")
            return;
        temp.y += 16;
        // Getting the type of the current method's parent component
        Type currentType;
        if (types.Length > selectedIndex.intValue)
        {
            currentType = types[selectedIndex.intValue];
            targetComponent.stringValue = currentType.Name;
        }
        else {
            currentType = null;
            targetComponent.stringValue = "none";
        }

        // Displaying the parameter field insertion
        if (methods[selectedIndex.intValue] != null && methods[selectedIndex.intValue].GetParameters().Length > 0)
        {
            for (int i = 0; i < methods[selectedIndex.intValue].GetParameters().Length; i++)
            {
                var argumentType = methods[selectedIndex.intValue].GetParameters()[i].ParameterType;
                EditorGUILayout.LabelField(methods[selectedIndex.intValue].GetParameters()[i].Name);
                switch (argumentType.Name)
                {
                    case "Boolean":
                        {
                            Bool.boolValue = EditorGUILayout.Toggle(Bool.boolValue);
                            valueType.stringValue = "Boolean";
                            break;
                        }
                    case "String":
                        {
                            String.stringValue = EditorGUILayout.TextArea(String.stringValue);
                            valueType.stringValue = "String";
                            break;
                        }
                    case "Single":
                        {
                            Float.floatValue = EditorGUILayout.FloatField(Float.floatValue);
                            valueType.stringValue = "Single";
                            break;
                        }
                    case "Int32":
                        {
                            Int.intValue = EditorGUILayout.IntField(Int.intValue);
                            valueType.stringValue = "Int32";
                            break;
                        }
                    case "Vector3":
                        {
                            Vector3Val.vector3Value = EditorGUILayout.Vector3Field(GUIContent.none, Vector3Val.vector3Value);
                            valueType.stringValue = "Vector3";
                            break;
                        }
                    case "Vector2":
                        {
                            Vector2Val.vector2Value = EditorGUILayout.Vector2Field(GUIContent.none, Vector2Val.vector2Value);
                            valueType.stringValue = "Vector2";
                            break;
                        }
                    case "Rect":
                        {
                            RectVal.rectValue = EditorGUILayout.RectField(GUIContent.none, RectVal.rectValue);
                            valueType.stringValue = "Rect";
                            break;
                        }
                    case "Color":
                        {
                            color.colorValue = EditorGUILayout.ColorField(GUIContent.none, color.colorValue);
                            valueType.stringValue = "Color";
                            break;
                        }
                    default:
                        {
                            Uobject.objectReferenceValue = EditorGUILayout.ObjectField(Uobject.objectReferenceValue, argumentType, true);
                            valueType.stringValue = "Uobject";
                            break;
                        }
                }
            }
        }
    }

    MethodInfo[] GetMethods(GameObject target, out string[] methods, out System.Type[] types)
    {
        // Getting the methods for components
        var components = new List<Component>();
        target.GetComponents<Component>(components);
        List<string> temp = new List<string>();
        List<MethodInfo> temp2 = new List<MethodInfo>();
        List<System.Type> temp3 = new List<System.Type>();
        foreach (var c in components)
        {
            var type = c.GetType();
            var functions = type.GetMethods();
            foreach (var func in functions)
            {
                if (func.ReturnType.Name == "Void" && func.GetParameters().Length < 5 && !func.IsGenericMethod)
                {
                    string funcName = func.Name;
                    if (funcName.Contains("set_"))
                    {
                        string s = funcName.Remove(0, 4);
                        funcName = s;
                    }
                    funcName = c.GetType().Name + "/" + funcName;
                    temp.Add(funcName);
                    temp2.Add(func);
                    temp3.Add(c.GetType());
                }
            }
        }
        // Getting the methods for GameObject
        var type2 = typeof(GameObject);
        var functions2 = type2.GetMethods();
        foreach (var func in functions2)
        {
            if (func.ReturnType.Name == "Void" && func.GetParameters().Length < 5 && !func.IsGenericMethod)
            {
                string funcName = func.Name;
                if (funcName.Contains("set_"))
                {
                    string s = funcName.Remove(0, 4);
                    funcName = s;
                }
                funcName = (typeof(GameObject)).Name + "/" + funcName;
                temp.Add(funcName);
                temp2.Add(func);
                temp3.Add(typeof(GameObject));
            }
        }

        methods = new string[temp.Count + 1];
        MethodInfo[] rawMethods = new MethodInfo[temp.Count + 1];
        types = new System.Type[temp.Count];
        for (int i = 0; i < temp.Count; i++)
        {
            methods[i] = temp[i];
        }
        for (int i = 0; i < temp.Count; i++)
        {
            rawMethods[i] = temp2[i];
        }
        for (int i = 0; i < temp.Count; i++)
        {
            types[i] = temp3[i];
        }
        rawMethods[temp.Count] = null;
        methods[temp.Count] = "none";
        return rawMethods;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 32;
    }
}