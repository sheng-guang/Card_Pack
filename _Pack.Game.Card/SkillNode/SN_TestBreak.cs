using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SN_OnlySpaceInSpace : SKillNode
    {
        public override int Fix_1Exit_2ToNext_4Break()
        {
            //Debug.Log("test SN_OnlySpaceInSpace");
            if (eve.IsSpaceInSpace(unit) == false) return Break_TryThisAgain;
            return ToNext;
        }
    }


