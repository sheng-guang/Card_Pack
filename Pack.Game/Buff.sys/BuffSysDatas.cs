using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BuffSysD
{
    public static class BuffSysData
    {
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static Dictionary<IBuffSysData, DataListenGroup> ListenGroups = new Dictionary<IBuffSysData, DataListenGroup>();
        public static void ListenDataChange(IBuff buff, IBuffSysData data)
        {
            var to = ListenGroups.Ensure(data);
            to.LintenerBuffs.EnsureAdd(buff);

            var toto = buffs.Ensure_SetKey(buff);
            toto.ListenTo.EnsureAdd(data);

            BuffSys.AddNeedFreshBuff(buff);
        }

        public static void CancelListene(IBuff buff, IBuffSysData data)
        {
            var to = ListenGroups.Ensure(data);
            to.LintenerBuffs.Remove(buff);

            //todo 是否要加这个
            //结果 可能造成沉默复杂度增加先删除
            //BuffSys.AddNeedFreshBuff(buff);

        }
        public static bool OnListening(IBuffSysData d, out DataListenGroup to)
        {
            if (ListenGroups.TryGetValue(d, out to) == false) return false;
            if (to.LintenerBuffs.Coumt == 0) return false;
            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static Dictionary<IBuffSysBuffableData, BuffsToOneData> IBuffableDatas = new Dictionary<IBuffSysBuffableData, BuffsToOneData>();
        public static BuffsToOneData EnsureBuffsToOneData(IBuffSysBuffableData data)
        {
            return IBuffableDatas.Ensure_SetKey(data);
        }
        public static void WillApplyToData(this IBuff buff, IBuffSysBuffableData data)
        {
            var to = EnsureBuffsToOneData(data);
            to.buffs.EnsureAdd(buff);

            var toto = buffs.Ensure_SetKey(buff);
            toto.ApplyTo = data;

            BuffSys.AddNeedFreshData(data);
        }
        public static void RemoveWillApply(this IBuff buff, IBuffSysBuffableData data)
        {
            var to = EnsureBuffsToOneData(data);
            to.buffs.Remove(buff);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static Dictionary_List<IBuffSysMonitor, OneMonitor> monitors = new Dictionary_List<IBuffSysMonitor, OneMonitor>();
        public static void AddMonitor(IBuffSysMonitor monitor)
        {
            var to = monitors.Ensure_SetKey(monitor);
            to.SnapshotKey = "BuffSys|" + monitor.FullName;
        }
        public static void ForEachOneMonitor(Action<OneMonitor> act) { monitors.ForEach(act); }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        static Dictionary<IBuff, BuffGroup> buffs = new Dictionary<IBuff, BuffGroup>();
        public static void RemoveBuff(IBuff b)
        {
            //b.Removed = true;
            if (buffs.TryGetValue(b, out var group) == false) return;
            for (var i = 0; i < group.ListenTo.Count; i++)
            {
                var to = group.ListenTo.Get(i);
                CancelListene(b, to);
            }
            RemoveWillApply(b, group.ApplyTo);
            //Debug.Log("need fresh  " +links.AttachTo);
            BuffSys.AddNeedFreshData(group.ApplyTo);
        }
    }
    public class BuffGroup : ISet<IBuff>
    {
        public IBuff Value { get; set; }
        public HashSet_List<IBuffSysData> ListenTo = new HashSet_List<IBuffSysData>();
        public IBuffSysBuffableData ApplyTo;

    }



}