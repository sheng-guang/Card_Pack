using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSysFresh
{
    public static HashSet_List_Loop<IBuffSysData> ChangedDatas = new HashSet_List_Loop<IBuffSysData>();
    public static HashSet_List_Loop<IBuff> NeedFreshBuff = new HashSet_List_Loop<IBuff>();
    public static HashSet_List_Loop<BuffsToOneData> NeedFreshDatas = new HashSet_List_Loop<BuffsToOneData>();

    public static int NeedFreshCount =>
    ChangedDatas.OneLoopCount
    + NeedFreshBuff.OneLoopCount
    + NeedFreshDatas.OneLoopCount;



}
