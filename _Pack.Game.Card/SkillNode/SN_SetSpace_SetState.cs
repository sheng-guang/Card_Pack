using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pack;
/// <summary>
/// state
/// </summary>
public class SN_SetState : SKillNode
{
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == nn.state) o.TryToIGet_ref(ref state);
    }
    IGet<int> state;
    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.SetState(unit, state.Value);
        return ToNext;
    }
}
public class SN_SetState_Card : SKillNode
{

    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.SetState(unit, UnitState.Card);
        return ToNext;
    }
}
public class SN_SetState_Space : SKillNode
{
    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.SetState(unit, UnitState.Space);
        return ToNext;
    }
}
//-------------------------------------------------------------------------------------------------------------------
/// <summary>
/// space
/// </summary>
public class SN_SetSpace : SKillNode
{
    public SN_SetSpace()
    {
        new Func<Unit>(GetUnit).TryToIGet_ref(ref getUnit);

    }
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == nn.space) o.TryToIGet_ref(ref space);
        else if (key == nn.Unit) o.TryToIGet_ref(ref getUnit);
    }

    IGet<int> space;
    IGet<Unit> getUnit;



    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.SetSpace(unit, space.Value);
        return ToNext;
    }
}
public class SN_SetSpace_Spce : SKillNode
{
    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.SetSpace(unit, UnitSpace.Space);
        return ToNext;
    }
}
public class SN_SetSpace_Hand : SKillNode
{

    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.SetSpace(unit, UnitSpace.Hand);
        return ToNext;
    }
}

