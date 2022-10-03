using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using System;

public static class CreaterKind
{
    public const int none = 0;
    public const int Prefbe = 1;
    public const int AssetBundle = 2;
    public const int Class = 4;
    public const int Txt = 8;
    public const int HasPiplineTag = 64;

}
public class Creater<T>where T:class
{
    //
    public static void ChangeDirName(string dirName)
    {
        DefaultPartPath = dirName;
    }
    public static string DefaultPartPath { get; private set; } = "Assets/" + typeof(T).Name;
    //
    //public static bool TryGetNew(string Name, out T re,ResArgs r=null, string dir = null)
    //{
    //    re = GetCreater(Name, dir)?.GetNew(r);
    //    return re.NotNull_And_NotEqualNull();
    //}
    public static T GetNew( string Name, ResArgs r = null)
    {
        return getCreater(DefaultPartPath,Name)?.GetNew(r);
    }
    public static T GetNew( string dir ,string Name, ResArgs r = null)
    {
        return getCreater(dir, Name)?.GetNew(r);
    }
    //
    //public static bool TryGetCreater(string dir ,string Name, out IResCreater<T> re )
    //{
    //    re = GetCreater(Name, dir);
    //    return re.NotNull_And_NotEqualNull();
    //}
    static Dictionary<string, IResCreater<T>> loaded = new Dictionary<string, IResCreater<T>>();
    public static IResCreater<T> getCreater(string dir ,string Name)
    {
        //
        if (dir == null) dir = DefaultPartPath;
        //
        if (loaded.ContainsKey(Name)) return loaded[Name];
        loaded.Add(Name, null);
        loaded[Name]= FillCreater(dir, Name);
        return loaded[Name];
    }
    public static IResCreater<T> FillCreater(string dir, string Name)
    {
        IResCreater<T> re = null;

        bool fill(Func<string, string, IResCreater<T>> f)
        {
            if (re == null) re = f(dir, Name + GraphSetting.PipLineTag);
            if (re == null) re = f(dir, Name);
            return re != null;
        }
#if UNITY_EDITOR
        if (fill(CreaterEditor<T>.GetCreater)) return re;
#endif

        if (re== null) Debug.Log("creater: No found error: " + Name + " ");
        return re;
    }




    //public static T GetNew(string FullName, ResArgs args = null) { return GetNew(DefaultPartPath, FullName, args); }

    //public static T GetNew(string PartDirPath, string FullName, ResArgs args = null)
    //{
    //    //Debug.Log("create skill: "+PartDirPath + "|" + kind);
    //    int k = Ensure(PartDirPath, FullName);

    //    if (k.MaskContain(CreaterKind.HasPiplineTag)) FullName += "'" + GraphSetting.PipLineName;
    //    if (k.MaskContain(CreaterKind.Prefbe))
    //    {
    //        //Debug.Log("1   create from prefabe" + kind);
    //        return Creater_Prefabe<T>.GetNew(FullName, args);
    //    }
    //    else if (k.MaskContain(CreaterKind.AssetBundle))
    //    {
    //        //Debug.Log("2   create from ab " + kind);
    //        return Creater_AssetBundle<T>.GetNew(PartDirPath, FullName, args);
    //    }
    //    else if (k.MaskContain(CreaterKind.Class))
    //    {
    //        //Debug.Log("3   create from class " + kind);
    //        return Creater_Class<T>.GetNew(FullName, args);
    //    }
    //    else if (k.MaskContain(CreaterKind.Txt))
    //    {
    //        //Debug.Log("4   create from txt " + kind);

    //        try
    //        {
    //            return (T)Creater_TXT.GetNew(PartDirPath, FullName, args);

    //        }
    //        catch { }
    //    }
    //    Debug.Log(PartDirPath + "|" + FullName + " can not be creat");
    //    return default;
    //}
    ////static Dictionary<string, int> loaded = new Dictionary<string, int>();

    //static int Ensure(string dir, string kind)
    //{
    //    {
    //        var  FN =kind+ "'" + GraphSetting.PipLineName;
    //        //Debug.Log(FN);
    //        var k = _Ensure(dir, FN, CreaterKind.HasPiplineTag);
    //        if (k != CreaterKind.none) return k;
    //    }
    //    {
    //        var k = _Ensure(dir, kind, 0);
    //        if (k != CreaterKind.none) return k;
    //    }
    //    return CreaterKind.none;

    //}
    //static int _Ensure(string dir, string kind,int ExInfo)
    //{
    //    if (loaded.TryGetValue(kind, out var re)) return re;

    //    if (Creater_Prefabe<T>.CanCreate(kind))
    //    { loaded[kind] = CreaterKind.Prefbe+ExInfo; return loaded[kind]; }

    //    if (Creater_AssetBundle<T>.CanCreate(dir, kind))
    //    { loaded[kind] = CreaterKind.AssetBundle+ExInfo; return loaded[kind]; }

    //    if (Creater_Class<T>.CanCreate(kind))
    //    { loaded[kind] = CreaterKind.Class+ExInfo; return loaded[kind]; }

    //    if (Creater_TXT.CanCreate(dir, kind))
    //    { loaded[kind] = CreaterKind.Txt+ExInfo; return CreaterKind.Txt; }
    //    return CreaterKind.none;
    //}
}

