using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Var : Var<object>
{

}
public class IGetSeatAct<T> : Var<Action< T>>
{
    public void TryInvoke(T v1)
    {
        if (Value == null) return;
        Value.Invoke(v1);
    }
}
public static class ExTest
{
    public static bool TestNull(int hash,string DN)
    {
        if (hash != 0) { return true; }
        Debug.Log("hash is 0|" + DN);
        return false;
    }
    public static bool TestNull(object obj,string DN)
    {
        if (obj != null) return true;
        Debug.Log("obj is null|" + DN);
        return false;
    }
}
[Api]
public static class Extension
{
    public static Var Ex(this object obj, string DataName)
    {
        if (ExTest.TestNull(obj,DataName) == false) return null;
        var re = Extension<Var>.EnsureExtension(obj, DataName);
        return re;
    }
    public static Var<T> Ex<T>(this object obj, string DataName)
    {
        if (ExTest.TestNull(obj, DataName) == false) return null;

        var re = Extension<Var<T>>.EnsureExtension(obj, DataName);
        return re;
    }
    public static Var<T> Ex<T>(this int hash, string DataName)
    {
        if (ExTest.TestNull(hash, DataName) == false) return null;

        var re = Extension<Var<T>>.EnsureExtension(hash, DataName);
        return re;
    }
    //int  ---------------------------------------------------------------------------------------------------------------------------------------------------
    public static Var<int>Int(this int h, string DN) => h.Ex<int>(DN);
    public static Var<bool> Bool(this int h, string DN) => h.Ex<bool>(DN);
    public static Var<float>Float(this int h, string DN) => h.Ex<float>(DN);
    public static Var<Vector3>V3(this int h, string DN) => h.Ex<Vector3>(DN);
    public static Var<string>Str(this int h, string DN) => h.Ex<string>(DN);
    //---------------------------
    public static Var<int> Int(this int h, string DN,object o) => h.Ex<int>(DN).SetIGet(o);
    public static Var<bool> Bool(this int h, string DN, object o) => h.Ex<bool>(DN).SetIGet(o);
    public static Var<float> Float(this int h, string DN, object o) => h.Ex<float>(DN).SetIGet(o);
    public static Var<Vector3> V3(this int h, string DN,object o) => h.Ex<Vector3>(DN).SetIGet(o);
    public static Var<string> Str(this int h, string DN,object o) => h.Ex<string>(DN).SetIGet(o);

    //N<T>---------------------------------------------------------------------------------------------------------------------------------------------------
    public static Var<N<int>>NInt(this int h, string DN) => h.Ex<N<int>>(DN);
    public static Var<N<bool>>NBool(this int h, string DN) => h.Ex<N<bool>>(DN);
    public static Var<N<float>>NFloat(this int h, string DN) => h.Ex<N<float>>(DN);
    public static Var<N<Vector3>>NV3(this int h, string DN) => h.Ex<N<Vector3>>(DN);
    //---------------------------
    public static Var<N<int>> NInt(this int h, string DN, object o) => h.Ex<N<int>>(DN).SetIGet(o);
    public static Var<N<bool>> NBool(this int h, string DN, object o) => h.Ex<N<bool>>(DN).SetIGet(o);
    public static Var<N<float>> NFloat(this int h, string DN, object o) => h.Ex<N<float>>(DN).SetIGet(o);
    public static Var<N<Vector3>> NV3(this int h, string DN, object o) => h.Ex<N<Vector3>>(DN).SetIGet(o);





    //object---------------------------------------------------------------------------------------------------------------------------------------------------
    public static Var<int> Int(this object h, string DN) => h.Ex<int>(DN);
    public static Var<bool> Bool(this object h, string DN) => h.Ex<bool>(DN);
    public static Var<float> Float(this object h, string DN) => h.Ex<float>(DN);
    public static Var<Vector3> V3(this object h, string DN) => h.Ex<Vector3>(DN);
    public static Var<string> Str(this object h, string DN) => h.Ex<string>(DN);
    //---------------------------
    public static Var<int> Int(this object h, string DN, object o) => h.Ex<int>(DN).SetIGet(o);
    public static Var<bool> Bool(this object h, string DN, object o) => h.Ex<bool>(DN).SetIGet(o);
    public static Var<float> Float(this object h, string DN, object o) => h.Ex<float>(DN).SetIGet(o);
    public static Var<Vector3> V3(this object h, string DN, object o) => h.Ex<Vector3>(DN).SetIGet(o);
    public static Var<string> Str(this object h, string DN, object o) => h.Ex<string>(DN).SetIGet(o);
    //N<T>---------------------------------------------------------------------------------------------------------------------------------------------------
    public static Var<N<int>> NInt(this object h, string DN) => h.Ex<N<int>>(DN);
    public static Var<N<bool>> NBool(this object h, string DN) => h.Ex<N<bool>>(DN);
    public static Var<N<float>> NFloat(this object h, string DN) => h.Ex<N<float>>(DN);
    public static Var<N<Vector3>> NV3(this object h, string DN) => h.Ex<N<Vector3>>(DN);
    //---------------------------
    public static Var<N<int>> NInt(this object h, string DN, object o) => h.Ex<N<int>>(DN).SetIGet(o);
    public static Var<N<bool>> NBool(this object h, string DN, object o) => h.Ex<N<bool>>(DN).SetIGet(o);
    public static Var<N<float>> NFloat(this object h, string DN, object o) => h.Ex<N<float>>(DN).SetIGet(o);
    public static Var<N<Vector3>> NV3(this object h, string DN, object o) => h.Ex<N<Vector3>>(DN).SetIGet(o);




}

