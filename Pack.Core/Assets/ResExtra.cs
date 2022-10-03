using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static partial class ResExtra
{
    public static string FullName(this IRes r)
    {
        return r.PackName + "'" + r.KindName;
    }
    public static string Name(this IRes r)
    {
        return (r as Component).name;
    }
    public static T LastItem<T>(this T[] list)
    {
        return list[list.Length - 1];
    }

    //public static string[] ToPossArgs(this Vector3 poss)
    //{
    //    return new string[] { "PossX:"+poss.x,"PossY:"+poss.y,"PossZ:"+poss.z };
    //}
}
public static partial class ResExtra
{
    //---------------------------------------------------------------------
    public static void EnsureAssetFolder(this IRes res)
    {
        var f = Application.dataPath + "/" + res.DirectoryName + "/" + res.Name();
        if (Directory.Exists(f) == false) Directory.CreateDirectory(f);
    }

    //---------------------------------------------------------------------
    public static string PrefabeDir(IRes res)
    { return "Assets/" + res.DirectoryName + "/" + res.Name(); }

    public static string PrefabPath(this IRes res)
    { return PrefabeDir(res) + "/" + res.Name() + ".prefab"; }

    public static string PrefabeAsyncPath(this AsyncResTool n, IRes res)
    { return PrefabeDir(res) + "/" + n.AsyncAsetName + ".prefab"; }



    //---------------------------------------------------------------------
    public static string ABDir(this IRes res)
    {
        return res.DirectoryName + "/" + res.Name() + "/";
    }
    public static string ABPath(this IRes res)
    {
        return res.DirectoryName + "/" + res.Name() + "/" + res.Name();
    }
    public static string ABAsyncPath(this IRes res)
    {
        return res.DirectoryName + "/" + res.Name() + "/" + res.Name() + "_";
    }

}
