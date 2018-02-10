using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[System.Serializable]
public class UpgradeEvent{
    public GameObject target;
    public string eventName;
    public MethodInfo methodInfo;
    public bool Bool;
    public float Float;
    public string String;
    public int Int;
    public Vector2 Vector2Val;
    public Vector3 Vector3Val;
    public Rect rectVal;
    public Color color;
    public UnityEngine.Object Uobject;
    public string valueType;
    public string targetComponent;
    public AudioClip useSound;

    public int selectedIndex;

    public void TriggerEvent()
    {
        if (eventName != "none")
        {
            object temp = new object();
            switch (valueType)
            {
                case "Boolean":
                    {
                        temp = Bool;
                        break;
                    }
                case "String":
                    {
                        temp = String;
                        break;
                    }
                case "Single":
                    {
                        temp = Float;
                        break;
                    }
                case "Int32":
                    {
                        temp = Int;
                        break;
                    }
                case "Vector2":
                    {
                        temp = Vector2Val;
                        break;
                    }
                case "Vector3":
                    {
                        temp = Vector3Val;
                        break;
                    }
                case "Rect":
                    {
                        temp = rectVal;
                        break;
                    }
                case "Color":
                    {
                        temp = color;
                        break;
                    }
                default:
                    {
                        temp = Uobject;
                        break;
                    }
            }
            if (targetComponent != "GameObject")
            {
                var component = target.GetComponent(targetComponent);
                var mthds = component.GetType().GetMethods();
                foreach (var mthd in mthds)
                {
                    if ((mthd.Name == eventName && mthd.GetParameters().Length < 2 && !mthd.IsGenericMethod))
                    {
                        methodInfo = mthd;
                        break;
                    }
                }
                methodInfo.Invoke(component, new object[] { temp });
            }
            else {
                var mthds = typeof(GameObject).GetMethods();
                foreach (var mthd in mthds)
                {
                    if ((mthd.Name == eventName && mthd.GetParameters().Length < 2 && !mthd.IsGenericMethod))
                    {
                        methodInfo = mthd;
                        break;
                    }
                }
                methodInfo.Invoke(target, new object[] { temp });
            }
            if (useSound != null)
                AudioSource.PlayClipAtPoint(useSound, Vector3.zero);
        }
    }
}

[System.Serializable]
public enum Rarity
{
    common, rare, epic, legendary
}
