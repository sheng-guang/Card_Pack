using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using Pack;
public class Cfg : EditorWindow
{
    private void OnEnable()
    {
        //Debug.Log("UnitDataWindow enable");
    }
    [MenuItem("Tool window/Cfg")]
    public static void Init()
    {
        Debug.Log("open unitdata window");
        Cfg window = (Cfg)EditorWindow.GetWindow(typeof(Cfg));
        window.Show();
    }
    private void OnGUI()
    {
        if (GUILayout.Button("CreatImgDir"))
        {
            EnsureDir();
        }
        if (GUILayout.Button("Creat ALL Unit Data"))
        {
            CreatAllData();
        }
        if (GUILayout.Button("Creat ALL ResData"))
        {
            CleaResData();
        }

        if (GUILayout.Button("Clear ResData"))
        {
            CleaResData();
        }

        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();
    }

    public void EnsureDir()
    {
        var d = UnitData.GetAllData();
        foreach (var item in d)
        {
            string path = ("Img/" + item.FullName).ToStreamingAssetFullPath() + "/";
            if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
        }
    }
    public static void CreatAllData()
    {
        UnitData.ClearLoaded();
        var datas = UnitData.GetAllData().ToList();
        //Debug.Log("Load Count: "+datas.Count);
        var list = PrefabList.GetListCopy();
        foreach (var item in list)
        {
            if (item.pre == null) continue;
            Unit u = item.pre.GetComponent<Unit>();
            if (u == null) continue;
            UnitData.EnsureData(u.FullName());
        }
        UnitData.SaveAll();
        datas = UnitData.GetAllData().ToList();
        Debug.Log("Load Count: " + datas.Count);

    }

    public void CreatAllResData()
    {

    }
    public void CleaResData()
    {
        DB.Datas(MeshRes.kind).ClearAllData_Save();
    }
}

