using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Pack;

public class QuickAccess : EditorWindow
{

    [MenuItem("Tool window/Quick Access")]
    public static void Init()
    {
        Debug.Log("open QucikAccessWindow");
        QuickAccess window = (QuickAccess)GetWindow(typeof(QuickAccess));
        window.Show();
    }
    private void OnEnable()
    {
        //Debug.Log("quick obj access enable :");

        count = EditorPrefs.GetInt(GetType().Name + "|"+nameof (count),0);
        for (int i = 0; i < count; i++)
        {
            list.Add( GetSavedObject(i));
        }


    }
    List<Object> list { get; set; } = new List<Object>();

    void OnDisable()
    {
        EditorPrefs.SetInt(GetType().Name + "|" + nameof(count), count);
        for (int i = 0; i < list.Count; i++)
        {
            SaveObj(list[i],i);
        }
    }
    int count = 15;

    private void OnGUI()
    {
        Time.timeScale = EditorGUILayout.Slider(Time.timeScale, 0, 1);
        count = EditorGUILayout.IntField(count);
        while(list.Count>count==false)list.Add(null);
        list[list.Count-1]=GetSavedObject(list.Count-1);

        for (int i = 0; i < count; i++)
        {
            var old = list[i];
            list[i] = EditorGUILayout.ObjectField(list[i], typeof(Object));
        }

    }
    public void SaveObj(Object to,int i)
    {
        if (to == null) 
        { 
            EditorPrefs.DeleteKey(GetType().Name + "|" + nameof(Object) + "|" + i);
            return;
        }
        EditorPrefs.SetString(GetType().Name + "|" + nameof(Object) + "|" + i
              , AssetDatabase .GetAssetPath(to));
    }
    public Object GetSavedObject(int i)
    {
        string path = EditorPrefs.GetString(GetType().Name + "|" + nameof(Object) + "|" + i, null);
        return AssetDatabase.LoadAssetAtPath<Object>(path);
    }

}






