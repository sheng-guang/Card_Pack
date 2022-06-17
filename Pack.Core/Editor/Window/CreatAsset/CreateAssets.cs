
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssets : EditorWindow
{
    [MenuItem("Tool window/CreateAssets")]
    public static void Init()
    {
        Debug.Log("open QucikAccessWindow");
        CreateAssets window = (CreateAssets)GetWindow(typeof(CreateAssets));
        window.Show();
    }
    void OnEnable()
    {
        Groups = EditorSaveData<CreateAssets>.GetInt(-1);

        for (int i = 0; i < Groups; i++)
            LoadOneGroup(i);


    }
    public void LoadOneGroup(int index)
    {
        string s0 = EditorSaveData<CreateAssets>.GetString(0,index);
        string s1 = EditorSaveData<CreateAssets>.GetString(1,index);
        string s2 = EditorSaveData<CreateAssets>.GetString(2, index);
        string s3 = EditorSaveData<CreateAssets>.GetString(3, index);
        var ne = new string[] { s0, s1, s2 , s3 };
        groups.EnsureIndex_ThenSet(index, ne);
    }
    List<string[]> groups = new List<string[]>();
    void OnDisable()
    {
        EditorSaveData<CreateAssets>.SetInt(Groups, -1);
        for (int i = 0; i < groups.Count; i++) SaveGroup(i);
    }
    public void SaveGroup(int index)
    {
        EditorSaveData<CreateAssets>.SetString(groups[index][0],0,index);
        EditorSaveData<CreateAssets>.SetString(groups[index][1],1,index);
        EditorSaveData<CreateAssets>.SetString(groups[index][2],2,index);
        EditorSaveData<CreateAssets>.SetString(groups[index][3],3,index);

    }
    int Groups;


    private void OnGUI()
    {
        Groups=EditorGUILayout.IntField(Groups);

        for (int i = 0; i < Groups; i++)
        {
            groups.EnsureIndex(i);
            if (groups[i] == default) groups[i] = new string[] { "", "", "", "" };
            string Asset_Dir = groups[i][0];
            string PackName = groups[i][1];
            string KindName = groups[i][2];
            string FileKind = groups[i][3];
            if (GUILayout.Button("create")) Create(PackName,KindName,Asset_Dir,FileKind);
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Asset_Dir:");
                groups[i][0] = EditorGUILayout.TextField(Asset_Dir);
                GUILayout.EndHorizontal();
            }
            {
                GUILayout.BeginHorizontal();
                groups[i][1] = EditorGUILayout.TextArea(PackName);
                GUILayout.Label("'");
                groups[i][2] = EditorGUILayout.TextField(KindName);
                GUILayout.Label(".");
                groups[i][3] = EditorGUILayout.TextArea(FileKind);
                GUILayout.EndHorizontal();
            }
        }
     

    }
    public void Create(string PackName, string KindName, string Asset_Dir, string FileKind)
    {
        string FullName = PackName + "'" + KindName;
        string dirPath = Application.dataPath + "/Assets" + "/" + Asset_Dir + "/" + FullName + "/";
        string FileName = FullName + "." + FileKind;
        string full = dirPath + FileName;
        string AssetPath = "Assets" + "/" + "Assets" + "/" + Asset_Dir + "/" + FullName + "/" + FileName;
        if (Directory.Exists(dirPath) == false) { Directory.CreateDirectory(dirPath); }
        full.WriteEachLine("", fileType: "ts");
        AssetDatabase.Refresh();
        var to = AssetDatabase.LoadAssetAtPath<Object>(AssetPath);
        Debug.Log(to + "  path" + AssetPath);
        Selection.objects = new Object[] { to };
    }
    
}
