using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public interface IBuff
    {
        public void FreshActive();
        public void FreshRemove();
        public void Apply();
    //public bool Removed { get; set; }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public interface IBuffSysData
    {

    }
    public interface IBuffSysMonitor : IBuffSysData
    {
        public string FullName { get; }
        public bool IsChanged(string SnapshotKey);
    }
    public class BuffSysMonitor<T> : IBuffSysMonitor
    {
        public BuffSysMonitor(string n,Func<T> f)
        {
            FullName = n;
            GetValue = f;
            BuffSys.AddMonitor(this);
        }
        public string FullName { get;private set;}
        public Func<T> GetValue { get;private set; }

        public bool IsChanged(string SnapshotKey)
        {
           return DataSnapshot<T>.NewData_Changed(SnapshotKey, GetValue());
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public interface IBuffSysBuffableData:IBuffSysData
    {
        public void BackToBase();
    }



    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class DataListenGroup
    {
        //public IBuffSysData d;
        public HashSet_NodeList<IBuff> LintenerBuffs = new HashSet_NodeList<IBuff>();
    }
    public class BuffsToOneData:ISet<IBuffSysBuffableData>
    {
        public IBuffSysBuffableData Value { get; set; }
        public HashSet_NodeList<IBuff> buffs = new HashSet_NodeList<IBuff>();
    }
    public class OneMonitor:ISet<IBuffSysMonitor>
    {
        public IBuffSysMonitor Value { get; set; }
        public string SnapshotKey;
    }

