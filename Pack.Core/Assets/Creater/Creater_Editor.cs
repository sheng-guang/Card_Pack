#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

using UnityEditor;


public static class CreaterEditor<T>
{
    public static IResCreater<T> GetCreater(string dir, string Name)
    {
        var path = "Assets/" + dir + "/" + Name + "/" + Name + ".prefab";
        Log(path);
        var to = AssetDatabase.LoadAssetAtPath<Object>(path) as GameObject;
        var re = to?.GetComponent<IResCreater<T>>();
        return re;

    }

}
#endif