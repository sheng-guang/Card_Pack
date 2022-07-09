using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SN_UseMana : SKillNode
{
    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.UseMana(unit, player);
        Debug.Log("use mana");
        return ToNext;
    }
}

public class SN_SetUp_Player : SKillNode
{
    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.SetUP(unit, self.player);
        return ToNext;
    }
}



