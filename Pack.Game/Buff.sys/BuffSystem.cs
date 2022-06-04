using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    
    public static partial class BuffSys
    {
        static HashSet_List_Loop<IBuffSysData> ChangedDatas = new HashSet_List_Loop<IBuffSysData>();
        public static void AddChangedData(IBuffSysData changedData) { ChangedDatas.Add(changedData); }
        public static void _1_FreshAllmonitors() { BuffSysData.ForEachOneMonitor(A_FreshEachM); }
        static Action<OneMonitor> A_FreshEachM = (to) =>
        {
            if (BuffSysData.OnListening(to.Value, out _) == false) return;
            if (to.Value.IsChanged(to.SnapshotKey) == false) return;
            AddChangedData(to.Value);
        };


        public static void _1_ChangedData_To_MayChangeBuff()
        {
            for (int i = 0; i < ChangedDatas.OneLoopCount; i++)
            {
                var to= ChangedDatas.Get(i);
               if(BuffSysData.OnListening(to,out var group) == false)continue;
                group.LintenerBuffs.ForEach(AddNeedFreshBuff);
            }
            ChangedDatas.ClearOneLoop();
        }


        static HashSet_List_Loop<IBuff> NeedFreshBuff = new HashSet_List_Loop<IBuff>();
        public static void AddNeedFreshBuff(IBuff b)
        {
            NeedFreshBuff.Add(b);
        }
        public static void _2_MayChangeBuff_To_NeedFreshDatas()
        {
            for (int i = 0; i < NeedFreshBuff.OneLoopCount; i++)
            {
                var to = NeedFreshBuff.Get(i);
                to.FreshActive();
            }

            for (int i = 0; i < NeedFreshBuff.OneLoopCount; i++)
            {
                var to = NeedFreshBuff.Get(i);
                to.FreshRemove();
            }
            NeedFreshBuff.ClearOneLoop();
        }


        static HashSet_List_Loop<BuffsToOneData> NeedFreshDatas = new HashSet_List_Loop<BuffsToOneData>();
        public static void AddNeedFreshData(IBuffSysBuffableData data)
        {
            var to = BuffSysData.EnsureBuffsToOneData(data);
            NeedFreshDatas.Add(to);
        }
        public static void _3_FreshNeedFreshData()
        {
            for (int i = 0; i < NeedFreshDatas.OneLoopCount; i++)
            {
               var to= NeedFreshDatas.Get(i);
                to.Value.BackToBase();
                to.buffs.ForEach(Apply_Act);
            }
            NeedFreshDatas.ClearOneLoop();

        }
        static Action<IBuff> Apply_Act = (b) => b.Apply();


    }

    public static partial class BuffSys
    {
        public static int FreshVersion { get; private set; } = 0;
        public static BuffSysMonitor<int> FreshVersionMonitor { get; private set; }
            = new BuffSysMonitor<int>(nameof(BuffSys.FreshVersionMonitor), () => FreshVersion);
        public static int NeedFreshCount => 
            ChangedDatas.OneLoopCount
            + NeedFreshBuff.OneLoopCount 
            + NeedFreshDatas.OneLoopCount;

        public static void Fresh()
        {
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
            ChangedDatas.ClearAll();
            NeedFreshBuff.ClearAll();
            NeedFreshDatas.ClearAll();
        }
    }



    }
