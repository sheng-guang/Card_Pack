using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace Pack
{

    public interface IRes
    {
        string DirectoryName { get; }
        string PackName { get; }
        string KindName { get; }
    }

    public interface IResGetter
    {
        object GetNewObject(ResArgs a);
    }
    public interface IResGetter<T>:IResGetter
    {
        T GetNew(ResArgs a);
    }



    //public class Res<T>
    //{
    //    static Dictionary<string, IResGetter<T>> getters = new Dictionary<string, IResGetter<T>>();

    //    public static void Ensure(string kind,string dir)
    //    {
    //        //Debug.Log(typeof(T).ToString());
    //        if (getters.ContainsKey(kind)==false|| getters[kind] == null || getters[kind].Equals(null))
    //        {
    //            GameObject g = null;
    //            PrefabList.TryGetValue(kind, out g);
    //            if (g == null)
    //            {
    //                Debug.Log("load from AssetBundle");
    //                string part = dir + "/" + kind;
    //                Debug.Log(part);
    //                if (File.Exists(Application.streamingAssetsPath + "/" + part))
    //                {
    //                    g = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + part).LoadAsset<GameObject>(kind);
    //                }
    //            }
    //            if (g == null) Debug.Log("res   nofound: " + dir + " - " + kind);
    //            getters[kind] = g.GetComponent<IResGetter<T>>();
    //        }
    //    }
    //    public static T GetNew(string kind)
    //    {
    //        return GetNew(kind, typeof(T).ToString());
    //    }
    //    public static T GetNew(string kind,string dirName)
    //    {
    //        Ensure(kind, dirName);
    //        return getters[kind].GetNew();
    //    }

    //    public static IResGetter<T> GetGetter(string kind)
    //    {
    //        return GetGetter(kind, typeof(T).ToString());
    //    }
    //    public static IResGetter<T> GetGetter( string kind,string dirName)
    //    {
    //        Ensure(kind,dirName);
    //        return getters[kind];
    //    }

    //}




}
