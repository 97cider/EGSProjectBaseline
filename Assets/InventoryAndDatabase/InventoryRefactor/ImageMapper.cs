using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ImageMapper : MonoBehaviour {

    public static Sprite getItemSprite(string spriteName)
    {
        return Resources.Load<Sprite>("ItemSprites/" + spriteName);
    }

    public static string getPathToSprite(Sprite s)
    {
        return AssetDatabase.GetAssetPath(s);
    }
}
