using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSysD;
public static partial class BuffSys
{
    public static void AddChangedData(IBuffSysData changedData) { BuffSysFresh.ChangedDatas.Add(changedData); }
    static void _1_FreshAllmonitors() { BuffSysData.ForEachOneMonitor(A_FreshEachM); }
    static Action<OneMonitor> A_FreshEachM = (to) =>
    {
        if (BuffSysData.OnListening(to.Value, out _) == false) return;
        if (to.Value.IsChanged(to.SnapshotKey) == false) return;
        AddChangedData(to.Value);
    };


    static void _1_ChangedData_To_MayChangeBuff()
    {
        for (int i = 0; i < BuffSysFresh.ChangedDatas.OneLoopCount; i++)
        {
            var to = BuffSysFresh.ChangedDatas.Get(i);
            if (BuffSysData.OnListening(to, out var group) == false) continue;
            group.LintenerBuffs.ForEach(AddNeedFreshBuff);
        }
        BuffSysFresh.ChangedDatas.ClearOneLoop();
    }


    public static void AddNeedFreshBuff(IBuff b)
    {
        //Debug.Log("AddNeedFreshBuff   " + b);
        BuffSysFresh.NeedFreshBuff.Add(b);
    }
    static void _2_MayChangeBuff_To_NeedFreshDatas()
    {
        //Debug.Log("fresh buff==================");
        for (int i = 0; i < BuffSysFresh.NeedFreshBuff.OneLoopCount; i++)
        {
            var to = BuffSysFresh.NeedFreshBuff.Get(i);
            //如果已经删除掉 则不再刷新 防止??//todo
            //不再需要
            //if (to.Removed) continue;
            to.FreshActive();
            //Debug.Log("fresh active: " + to);
        }

        for (int i = 0; i < BuffSysFresh.NeedFreshBuff.OneLoopCount; i++)
        {
            var to = BuffSysFresh.NeedFreshBuff.Get(i);
            //如果已经删除掉 则不再刷新 防止移除掉其他 相同key的buff
            //不再需要
            //if (to.Removed) continue;

            to.FreshRemove();
            //Debug.Log("fresh remove: " + to);
        }
        BuffSysFresh.NeedFreshBuff.ClearOneLoop();
        //Debug.Log("==================fresh buff");

    }


    public static void AddNeedFreshData(IBuffSysBuffableData data)
    {
        var to = BuffSysData.EnsureBuffsToOneData(data);
        BuffSysFresh.NeedFreshDatas.Add(to);
    }
    static void _3_FreshNeedFreshData()
    {
        for (int i = 0; i < BuffSysFresh.NeedFreshDatas.OneLoopCount; i++)
        {
            var to = BuffSysFresh.NeedFreshDatas.Get(i);
            to.Value.BackToBase();
            to.buffs.ForEach(Apply_Act);
        }
        BuffSysFresh.NeedFreshDatas.ClearOneLoop();

    }
    static Action<IBuff> Apply_Act = (b) => b.Apply();
}
partial class BuffSys
{
    public static void ListenDataChange(IBuff buff, IBuffSysData data) 
    {
        BuffSysData.ListenDataChange(buff, data); 
    }
    public static void CancelListene(IBuff buff, IBuffSysData data)
    {
        BuffSysData.CancelListene(buff, data);
    }
    public static void WillApplyToData(IBuff buff, IBuffSysBuffableData data)
    {
        BuffSysData.WillApplyToData(buff, data);
    }
    public static void RemoveBuff(IBuff b)
    {
        BuffSysData.RemoveBuff(b);
    }
    public static void AddMonitor(IBuffSysMonitor monitor)
    {
        BuffSysData.AddMonitor(monitor);
    }
}




public static partial class BuffSys
{
    public static int FreshVersion { get; private set; } = 0;
    public static BuffSysMonitor<int> FreshVersionMonitor { get; private set; }
        = new BuffSysMonitor<int>(nameof(BuffSys.FreshVersionMonitor), () => FreshVersion);
    public static int NeedFreshCount => BuffSysFresh.NeedFreshCount;

    public static void Fresh()
    {
        //Debug.Log("one fresh==========================");
        FreshVersion++;
        while (true)
        {
            _1_FreshAllmonitors();
            _1_ChangedData_To_MayChangeBuff();
            if (NeedFreshCount == 0) break;
            _2_MayChangeBuff_To_NeedFreshDatas();
            if (NeedFreshCount == 0) break;
            _3_FreshNeedFreshData();
            if (NeedFreshCount == 0) break;
        }
        BuffSysFresh.ChangedDatas.ClearAll();
        BuffSysFresh.NeedFreshBuff.ClearAll();
        BuffSysFresh.NeedFreshDatas.ClearAll();
        //Debug.Log("one fresh--------------------------------------------------");

    }
}



