using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;

/// <summary>
/// unit         |point
/// </summary>
public class SN_SetPoss : SKillNode
{
    /// <summary>
    /// unit point
    /// </summary>
    public SN_SetPoss()
    {
        new Func<Unit>(GetUnit).TryToIGet_ref(ref TarGetUnit);
    }
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == nn.Point) o.TryToIGet_ref(ref poss);
        else if (key == nn.Unit) o.TryToIGet_ref(ref TarGetUnit);
    }
    IGet<Vector3> poss=Vector3.zero.ToIGet<Vector3>();
    IGet<Unit> TarGetUnit;
    public override int Fix_1Exit_2ToNext_4Break()
    {
        //Debug.Log("set poss"); 
        var to = TarGetUnit.Value;
        if (to == null) { Debug.Log("SN_SetPoss:    no unit "); return ToNext; }
        eve.SetPoss(to, poss.Value);
        return ToNext;
    }
}
/// <summary>
/// v
/// </summary>
public class SN_SetV : SKillNode
{
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == nn.V) o.TryToIGet_ref(ref v);
        //v = self.Param_V3(o.Cast<string>());
    }
    IGet<Vector3> v;
    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.SetV(unit, v.Value);
        return ToNext;
    }
}
/// <summary>
/// TarGetPoint   HighThrow
/// </summary>
public class SN_ThrowUnit : SKillNode
{
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == nn.TarGetPoint) o.TryToIGet_ref(ref TargetPoint);
        else if (key == nn.HighThrow) o.TryToIGet_ref(ref HighThrow);
    }
    IGet<Vector3> TargetPoint;
    IGet<N<bool>> HighThrow = false.ToIGet<N<bool>>();
    public override int Fix_1Exit_2ToNext_4Break()
    {
        //Debug.Log(TargetPoint.Value);
        //Debug.Log(unit.RealPoss);
        if (unit.speed.Value_Buffed.HasValue == false) 
        {
            Debug.Log(unit + " hav no speed value   [throwUnit]  Skip");
            return ToNext; 
        }
        //Debug.Log("is HighThrow: " + HighThrow.GetT);
        var v = PhyExtra.GetThrowVelocity(TargetPoint.Value, unit.RealPoss, HighThrow.Value, unit.speed.Value_Buffed.Value);
        unit.rig.velocity = v;
        return ToNext;
    }
}


/// <summary>
/// R
/// </summary>
public class SN_WaitTouchLand_OrBreak : SKillNode
{
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == nn.R) o.TryToIGet_ref(ref r);
    }
    public SN_WaitTouchLand_OrBreak()
    {
        Act_GetLand = GetLand;
    }
    IGet<float> r;
    //float r = 0.5f;
    Action<Collider> Act_GetLand;
    void GetLand(Collider c)
    {
        if (c.GetComponent<IIsUnit>().IsNull_or_EqualNull() == false) return;
        land = c;
    }
    Collider land;
    public override int Fix_1Exit_2ToNext_4Break()
    {

        land = null;
        eve.OverLapCollider(unit.RealPoss, r.Value, Act_GetLand);
        if (land == null) return Break_TryThisAgain;

        //Debug.Log(self+"  Touch Land: "+land.name);
        return ToNext;
    }
}
