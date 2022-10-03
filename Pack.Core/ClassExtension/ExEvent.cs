using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ActPlus<T>
{
    //public ActPlus()
    //{

    //}
    Action<T> act;
    public void AddAct(Action<T> a)
    {
        act += a;
    }
    public void TryInvoke(T p1)
    {
        act?.Invoke(p1);
    }

}
public static class ExEvent 
{
    public static void Act<T>(this int hash,string DN,T v)
    {
        var re = Extension<ActPlus<T>>.EnsureExtension(hash, DN);
        re.TryInvoke(v);
    }
    public static void Act<T>(this int hash,string DN,Action<T> a)
    {
        var re = Extension<ActPlus<T>>.EnsureExtension(hash, DN);
        re.AddAct(a);
    }



}
