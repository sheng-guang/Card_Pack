using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;

/// <summary>
/// StartPoint   Direction  TimeLeft
/// </summary>
public class SN_ShowBeforeMagicEffect : SKillNode
{
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == NN.StartPoint) o.TryToIGet_ref(ref StartPoint);
        else if (key == NN.Direction) o.TryToIGet_ref(ref Dir);
        else if (key == NN.TimeLeft) 
        {
            o.TryToIGet_ref(ref timeLeftGetter); 
            TimeLeft = timeLeftGetter.Value;
            total = TimeLeft;
        }
    }

    IGet<Vector3> Dir;
    IGet<Vector3> StartPoint;
    IGet<float> timeLeftGetter;
    float TimeLeft = 1;

    float total;
    float baseWide = 0.5f;
    public override int Fix_1Exit_2ToNext_4Break()
    {
        float percent = 1 - TimeLeft / total;
        percent =Mathf.Pow( percent,0.4f);
        var pos = StartPoint.Value + Dir.Value * percent*baseWide;
        pos += Dir.Value * 0.2f;

        eve.SetPoss(unit, pos + Vector3.up);
        unit.rig.velocity = Vector3.zero;

        TimeLeft -= TimeSetting.FixedDeltaTime;
        if (TimeLeft < 0) return Break_TryThisAgain + ToNext;
        return Break_TryThisAgain;
    }
}

