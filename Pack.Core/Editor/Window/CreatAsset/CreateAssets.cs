using Pack;
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
        Asset_Dir = EditorPrefs.GetString(GetType().Name + "|" + nameof(Asset_Dir));
        PackName = EditorPrefs.GetString(GetType().Name + "|" + nameof(PackName));
        KindName = EditorPrefs.GetString(GetType().Name + "|" + nameof(KindName));
        FileKind = EditorPrefs.GetString(GetType().Name + "|" + nameof(FileKind));
    }
    void OnDisable()
    {
        EditorPrefs.SetString(GetType().Name+"|"+nameof(Asset_Dir), Asset_Dir);
        EditorPrefs.SetString(GetType().Name+"|"+nameof(PackName), PackName);
        EditorPrefs.SetString(GetType().Name+"|"+nameof(KindName), KindName);
        EditorPrefs.SetString(GetType().Name+"|"+nameof(FileKind), FileKind);
    }
    string Asset_Dir;

    string PackName="S";
    string KindName;
    string FileKind="ts"; 

    private void OnGUI()
    {
        if (GUILayout.Button("create")) Create();
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Asset_Dir:");
            Asset_Dir = EditorGUILayout.TextField(Asset_Dir);
            GUILayout.EndHorizontal();
        }
        {
            GUILayout.BeginHorizontal();
            PackName = EditorGUILayout.TextArea(PackName);
            GUILayout.Label("'");
            KindName = EditorGUILayout.TextField(KindName);
            GUILayout.Label(".");
            FileKind = EditorGUILayout.TextArea(FileKind);
            GUILayout.EndHorizontal();
        }

    }
    public void Create()
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
