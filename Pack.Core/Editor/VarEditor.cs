using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Pack
{

    public class Editor<T> : Editor where T : class
    {
        public T tar;
        public virtual void Awake()
        {
            tar = target as T;
            if (tar == null) tar = (target as Component).GetComponent<T>();
        }
    }
}

public static class EditorSaveData<T>
{
    public static string ToKey(object[] keys)
    {
        var re = typeof(T).Name;
        foreach (var item in keys)
        {
            re += "|" + item;
        }
        return re;
    }
    //int
    public static void SetInt(int i,params object[] keys)
    {
        EditorPrefs.SetInt(ToKey(keys), i);
    }
    public static int GetInt(params object[] keys)
    {
        return EditorPrefs.GetInt(ToKey(keys));
    }
    //string
    public static void SetString(string i, params object[] keys)
    {
        EditorPrefs.SetString(ToKey(keys), i);
    }
    public static string GetString(params object[] keys)
    {
        return EditorPrefs.GetString(ToKey(keys));
    }
}
