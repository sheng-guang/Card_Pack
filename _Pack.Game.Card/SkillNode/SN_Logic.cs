using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;

public class SN_Exist : SKillNode
{
    public override int Fix_1Exit_2ToNext_4Break()
    {
        return Exit + ToNext;
    }
}