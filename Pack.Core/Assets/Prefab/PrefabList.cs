using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
//┌┬┐
//├┼┤
//└┴┘
//┌─┐
//│┼│
//└─┘
//预制体版本的资源
[CreateAssetMenu(fileName = "PrefabList", menuName = "Creat PrefabList Asset")]
public class PrefabList : ScriptableObject
{

    [MenuItem("PrefabList/Ensure List")]

    public static void EnsurePrefabList()
    {

#if UNITY_EDITOR
        
        if (ins != null) return;
        Debug.Log("Load PrefabList  ");
        string p = "Assets/Resources_/PrefabList.asset";
        var list = AssetDatabase.LoadAssetAtPath(p, typeof(ScriptableObject));
        if (list == null)
        {
            list = CreateInstance<PrefabList>();
           if( AssetDatabase.IsValidFolder("Assets/Resources_")==false) AssetDatabase.CreateFolder("Assets", "Resources_");
            AssetDatabase.CreateAsset(list, p);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            list = AssetDatabase.LoadAssetAtPath(p, typeof(ScriptableObject));
        }
        ins = list as PrefabList;
        ins.Load();
        Selection.activeObject = list;

#endif
    }

    private void Load()
    {
        Debug.Log("PrefabList load[" + list.Count + "]prefabs──────┐");
        foreach (var item in list)
        {
            //Debug.Log("load" + item.preName);
            dic.Add(item.preName, item.pre);
        }
        //Debug.Log(list.Count);
        Debug.Log("PrefabList load[" + list.Count + "]prefabs──────┘");
    }
    public bool UseAB;
    Dictionary<string, GameObject> dic = new Dictionary<string, GameObject>();
    [SerializeField]
    List<OnePrefab> list = new List<OnePrefab>();
    static PrefabList ins = null;
    public static PrefabList instance { get { EnsurePrefabList(); return ins; } }
    public static void SetUseAB(bool b)
    {
        if (instance == null) return;
        instance.UseAB = b;
        freshEditorUI();
    }
    public static List<OnePrefab> GetListCopy()
    {
        if (instance == null) return null;
        return instance.list.ToList();
    }
    public static void TryGetValue(string name, out GameObject re)
    {
        if (instance == null||instance.UseAB) { re = null; return; }
        instance.dic.TryGetValue(name, out re);

    }
    public static void Add(GameObject obj)
    {
        Add(obj, obj.name);
    }
    public static void Remove(string name)
    {
        foreach (var item in instance.list)
        {
            if (item.preName != name) continue;
            instance.list.Remove(item);
            break;
        }

        freshEditorUI();
    }
    public static void Add(GameObject obj, string n)
    {
        Debug.Log("add prefab:" + n);

        int toindex = 0;
        //删除再修改
        while (instance.list.Count > toindex)
        {
            if (instance.list[toindex].preName == n) { instance.list.RemoveAt(toindex); continue; }
            if (instance.list[toindex].pre == null) { instance.list.RemoveAt(toindex); continue; }
            toindex++;
        }

        instance.list.Add(new OnePrefab() { preName = n, pre = obj });

        freshEditorUI();

    }

    public static void freshEditorUI()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(instance);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }


}









[Serializable]
public struct OnePrefab
{
    public string preName;
    public GameObject pre;
}



#if UNITY_EDITOR
public static class PreFabSetting
{
    public static bool UseAB => EditorPrefs.GetBool("useAB", false);

    [MenuItem("AssetBundle/useAB")]
    public static void ChangeUseAB()
    {
        var b = EditorPrefs.GetBool("useAB", false);
        b = !b;
        EditorPrefs.SetBool("useAB", b);
        Menu.SetChecked("AssetBundle/useAB", b);
        PrefabList.SetUseAB(b);
        Debug.Log("useAB  " + b);
    }

}
#endif