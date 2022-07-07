using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGetSeatObj : IGetSeat<object>
{

}
[Api]
public static class Extension
{
    public static IGetSeatObj Ex(this object obj,string DataName)
    {
        if (obj == null) { Debug.Log("Ex_Ptr  null  obj"); return null; }
        var re = Extension<IGetSeatObj>.EnsureExtension(obj, DataName);
        return re;
    }
    public static IGetSeat<T> Ex<T>(this object obj, string DataName)
    {
        if (obj == null) { Debug.Log("Ex_Ptr  null  obj"); return null; }
        var re = Extension<IGetSeat<T>>.EnsureExtension(obj, DataName);
        return re;
    }
    public static IGetSeat<T> Ex<T>(this int hash, string DataName)
    {
        if(hash==0)Debug.Log("int hash   " + hash + "| " + DataName);
        var re = Extension<IGetSeat<T>>.EnsureExtension(hash, DataName);
        return re;
    }
}

