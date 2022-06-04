using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;


public class SN_UseMana : SKillNode
{
    public override int Fix_1Exit_2ToNext_4Break()
    {
        eve.UseMana(unit, player);
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



