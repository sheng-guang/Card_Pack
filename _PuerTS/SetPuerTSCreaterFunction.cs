using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Puerts;
using System;
using System.IO;

public static class Creater_TXT_PuerTS
{
    public static void EnsureExports(this JsEnv env)
    {
        env.Eval("if(!exports) var exports={}");
    }
    [RuntimeInitializeOnLoadMethod]
    static void Load()
    {
        Debug.Log("[Load]" + nameof(Creater_TXT_PuerTS));
        Creater_TXT.LoadFromString = Create;
    }
    public static object Create(string s, ResArgs a)
    {
        //JsEnv env = new JsEnv(new DefaultLoader());
        JsEnv env = new JsEnv();
        
        env.UsingAction<string, object>();
        env.UsingFunc<Vector3>();
        env.UsingFunc<int>();

        env.EnsureExports();
        env.Eval(s);
        var to = env.Eval<Func<ResArgs, object>>("create");
        if (to == null) return null;
        return to.Invoke(a);
    }
}


public class DefaultLoader : ILoader
{
    private string root = "";

    public DefaultLoader()
    {
    }

    public DefaultLoader(string root)
    {
        this.root = root;
    }

    private string PathToUse(string filepath)
    {
        if (filepath.EndsWith(".cjs")) return filepath.Substring(0, filepath.Length - 4);
        if(filepath.EndsWith(".mjs")) return filepath.Substring(0, filepath.Length - 4);
        if(filepath.EndsWith(".js")) return filepath.Substring(0, filepath.Length - 3);
        return filepath;
    }
    public enum LoadKind
    {
        _,
        Resources,
        dataPath_Assets_DTS,
        StreamPath,
        un,
    }
    public class LoadGroup
    {
        public LoadKind kind=  LoadKind._;
    }
    static Dictionary<string, LoadGroup> record = new Dictionary<string, LoadGroup>();
    public string ToDataPath(string pathToUse)
    {
        return Application.dataPath + "/Assets/DTS/" + pathToUse + ".sys.js";
    }
    public string ToStreamPath(string pathToUse)
    {
        return Application.streamingAssetsPath + "/Assets/DTS/" + pathToUse + ".sys.js,txt";
    }
    public bool FileExists(string filepath)
    {
        string pathToUse = this.PathToUse(filepath);
        if(record.TryGetValue(pathToUse,out var  g)==false) record[pathToUse] = new LoadGroup().MarkAs(out g);
        if (g.kind == LoadKind.un) return false;

        if (Resources.Load(pathToUse)) { g.kind = LoadKind.Resources; return true; }
        {
            string DataPath = ToDataPath(pathToUse);
            if (File.Exists(DataPath)) { g.kind = LoadKind.dataPath_Assets_DTS; return true; }
        }

        {
            string StreamPath = ToStreamPath(pathToUse);
            if (File.Exists(StreamPath)) { g.kind = LoadKind.StreamPath;return true; }
        }

        g.kind = LoadKind.un;
        return false;

    }

    public string ReadFile(string filepath, out string debugpath)
    {
        debugpath = Path.Combine(root, filepath).Replace("/", "\\");

        string pathToUse = PathToUse(filepath);
        if (record.TryGetValue(pathToUse, out var re) == false) return null;
        if(re.kind== LoadKind.Resources)
        {
            TextAsset file = (TextAsset)Resources.Load(pathToUse);
            return file == null ? null : file.text;
        }
        else if (re.kind == LoadKind.dataPath_Assets_DTS)
        {
            return File.ReadAllText(ToDataPath(pathToUse));
        }
        else if (re.kind == LoadKind.StreamPath)
        {
            return File.ReadAllText(ToStreamPath(pathToUse));
        }
        return null;



    }
}