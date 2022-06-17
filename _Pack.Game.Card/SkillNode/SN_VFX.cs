using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;

/// <summary>
/// resName
/// </summary>
public class SN_NewVFX : SKillNode
{
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == nn.ResName) o.TryToIGet_ref(ref EffName);
    }
    IGet<string> EffName;
    public override int Fix_1Exit_2ToNext_4Break()
    {
        var eff = unit.AddMesh(EffName.Value);
        return ToNext;

    }
}
