using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
/// <summary>
///  fullname|newid|space|state|offsetY
/// </summary>
public class SN_CreatNewUnitForPlayer : SKillNode
{
    public override void Set_(string key, object o)
    {
        base.Set_(key, o);
        if (key == nn.FullName) o.TryToIGet_ref(ref unitFullName);
        else if (key == nn.space) o.TryToIGet_ref(ref space);
        else if (key == nn.state) o.TryToIGet_ref(ref state);
        else if (key == nn.OffSetY) o.TryToIGet_ref(ref offsetY);
        else if (key == nn.ID_offset) o.TryToIGet_ref(ref IDoffSet);
    }
    IGet<int>IDoffSet=0.ToIGet<int>();
    IGet<string> unitFullName;
    IGet<int> space = UnitSpace.Space.ToIGet<int>();
    IGet<int> state = UnitState.Space.ToIGet<int>();
    IGet<N<float>> offsetY = null;
    //bool firstTime = true;
    public override int Fix_1Exit_2ToNext_4Break()
    {

        var Poss = unit.RealPoss;
        if (offsetY.Value.HasValue) Poss.y += offsetY.Value.Value;
        var ne = driver.CreatUnit(unitFullName.Value, new ResArgs().SetPoss(Poss));
        var to = eve.getPlayer_offSet(player.ID,IDoffSet.Value);
        //Debug.Log(player + "creat new " + ne + " for " + to);
        eve.SetUP(ne, to);
        eve.SetSpace(ne, space.Value);
        eve.SetState(ne, state.Value);
        //cardmesh 在被添加时 会把卡牌位置归零
        eve.SetPoss(ne, Poss);
        //NetHostTimer.Going = 100;

        return ToNext;
    }

}