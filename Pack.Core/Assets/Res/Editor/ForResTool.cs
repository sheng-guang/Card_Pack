using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;





[CustomEditor(typeof(ResTool))]
public class ForResTool : Editor<ResTool>
{
    public override void Awake()
    {
        base.Awake();
        SortComponet();
    }
    public void SortComponet()
    {
        if (Application.isPlaying) return;
        if (PrefabUtility.IsAnyPrefabInstanceRoot(tar.gameObject)) return;
        var comps = tar.GetComponents<Component>().Length;

        var ires = tar.GetComponent<IRes>() as Component;
        for (int i = 0; i < comps; i++)
            UnityEditorInternal.ComponentUtility.MoveComponentUp(ires);

        var v = tar.GetComponent<ResTool>();
        for (int i = 0; i < comps; i++)
            UnityEditorInternal.ComponentUtility.MoveComponentUp(v);
    }

    public override void OnInspectorGUI()
    {
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear")) { SetName(tar); Clear(tar); }
        if (GUILayout.Button("AsPrefabe")) { SetName(tar); AsPrefabe(tar); }
        if (GUILayout.Button("AsPackage")) { SetName(tar); AsPackage(tar); }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(GraphSetting.PipLineName);
        if (GUILayout.Button("Clear")) { SetName(tar, GraphSetting.PipLineTag); Clear(tar); }
        if (GUILayout.Button("AsPrefabe")) { SetName(tar, GraphSetting.PipLineTag); AsPrefabe(tar); }
        if (GUILayout.Button("AsPackage")) { SetName(tar, GraphSetting.PipLineTag); AsPackage(tar); }

        GUILayout.EndHorizontal();
        if (GUILayout.Button("SelectDir")) { SelectDir(tar); }
        base.OnInspectorGUI();

    }
    public static void SelectDir(ResTool tar)
    {
        //AssetDatabase.CreateFolder
    }
    public static void SetName(ResTool tar,string tag=null)
    {
        var res = tar.GetComponent<IRes>();
        tar.name = res.FullName() + tag;
    }
    public static void Clear(ResTool tar)
    {
        var res = tar.GetComponent<IRes>();
        //var AssetPath =  GetAssetPath(res);
        //res.GetAssetPath();// GetMainPreFullPath(res);
                                           //Debug.Log(fullpath);
        if (PrefabUtility.IsAnyPrefabInstanceRoot(tar.gameObject))
            PrefabUtility.UnpackPrefabInstance(tar.gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.UserAction);
        //if (AssetDatabase.LoadAssetAtPath<GameObject>(AssetPath)) { AssetDatabase.DeleteAsset(AssetPath); }

        PrefabList.Remove(res.Name());
        AssetDatabase.Refresh();
    }



    //-----------------------------------------------------------------------------------------------------------------------------
    
    public static void AsPrefabe(ResTool tar)
    {
        EnsureData(tar);
        var Res = tar.GetComponent<IRes>();
        if (Res == null) { Debug.Log("no  IRes"); return; }

        //0预先准备
        var BeforeAsPrefabe = tar.GetComponentsInChildren<IBeforeAsPrefabe>();
        var AfterAsPrefabe = tar.GetComponentsInChildren<IAfterAsPrefabe>();
        for (int i = 0; i < BeforeAsPrefabe.Length; i++) { BeforeAsPrefabe[i].BeforeAsPrefabe1(Res); }
        for (int i = 0; i < BeforeAsPrefabe.Length; i++) { BeforeAsPrefabe[i].BeforeAsPrefabe2(Res); }
        //2保存自己
        try
        {
            Res.EnsureAssetFolder();
            //2 save  main
            {
                var p = Res.PrefabPath();
                
                GameObject saved = PrefabUtility.SaveAsPrefabAssetAndConnect
                    (tar.gameObject, p, InteractionMode.UserAction);
                
                saved.transform.position = Vector3.zero;
                PrefabList.Add(saved,tar.name);
                //var pre = AssetImporter.GetAtPath(p);
                //pre.assetBundleName = getter.FullName();
            }
            foreach (var item in AfterAsPrefabe) { item.AfterAsPrefabe1(Res); }
            foreach (var item in AfterAsPrefabe) { item.AfterAsPrefabe2(Res); }

        }
        catch (System.Exception e) { Debug.Log("save error:  " + e.Message); }



    }

    public static void EnsureData(ResTool tar)
    {
        IResData dataOwner = tar.GetComponent<IResData>();
        if (dataOwner.Null_Or_EqualNull()) return;
        DB.Datas(dataOwner.DataKind).EnsureData_Save(dataOwner.FullName);

    }
    public static bool IsParent(ResTool par, Component c)
    {
        Transform t = c.transform;
        while (t != null)
        {
            var tool = t.GetComponent<ResTool>();

            if (tool == null)
            {
                t = t.parent;
                continue;
            }
            return tool == par;
        }
        Debug.Log(c.name + "no ResTool");
        return false;
    }
    public static void AsPackage(ResTool tar)
    {
        AsPackage(tar.gameObject);
    }

    public static void AsPackage(GameObject tar)
    {
        EnsureDir(tar);
        BuildMainPack(tar);
        BuildAsyncPack(tar);
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        AssetDatabase.Refresh();
    }
    public static void EnsureDir(GameObject tar)
    {
        var res = tar.GetComponent<IRes>();
        if (res == null) { Debug.Log("Res not exist"); return; }
        var dir = Application.streamingAssetsPath + "/" + res.ABDir();
        if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
    }
    public static void BuildMainPack(GameObject tar)
    {
        var res = tar.GetComponent<IRes>();
        if (res == null) { Debug.Log("Res main  not exist"); return; }

        //var PathMain = res.GetAssetPath();
        var PathMain = "";

        if (AssetDatabase.LoadAssetAtPath<GameObject>(PathMain) == null) { Debug.Log(PathMain + " main does not exist"); return; }
        Debug.Log(PathMain);
        string TarPath = "Assets/StreamingAssets/" + res.ABPath();
        Debug.Log(TarPath);
        BuildPipeline.BuildAssetBundle(PathMain.ToAssets()[0], null, TarPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows64);

    }

    public static void BuildAsyncPack(GameObject tar)
    {
        var res = tar.GetComponent<IRes>();
        if (res == null) return;
        List<string> assets = new List<string>();
        foreach (var item in tar.GetComponentsInChildren<AsyncResTool>())
        {
            var path = item.PrefabeAsyncPath(res);
            if (AssetDatabase.LoadAssetAtPath<GameObject>(path) == null) { Debug.Log(path + "Async does not exist"); continue; }
            Debug.Log(path);
            assets.Add(path);
        }
        if (assets.Count == 0) { Debug.Log("zero Async Asset"); return; }
        string TarPath = "Assets/StreamingAssets/" + res.ABAsyncPath();
        Debug.Log(TarPath);
        BuildPipeline.BuildAssetBundle(null, assets.ToAssets(), TarPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows64);

    }
}


public static partial class ResExtra
{
    public static Object[] ToAssets(this string path)
    {
        Object[] re = new Object[1];
        re[0] = AssetDatabase.LoadAssetAtPath<Object>(path);
        return re;
    }
    public static Object[] ToAssets(this List<string> path)
    {
        List<Object> objs = new List<Object>();
        foreach (var item in path)
        {
            objs.Add(AssetDatabase.LoadAssetAtPath<Object>(item));
        }
        return objs.ToArray();
    }
}



