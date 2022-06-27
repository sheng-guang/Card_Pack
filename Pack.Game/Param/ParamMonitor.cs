using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class SyncMonitor<T> : IBuffSysMonitor
    {
        public bool IsChanged(string DataSnapshotKey)
        {
            return DataSnapshot<T>.NewData_Changed(DataSnapshotKey, Value);
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class SyncMonitor<T>where T:IEquatable<T> //id
    {
        public int ID { get; private set; }
        public string Name { get; private set; } = null;
        public string FullName { get; private set; } = null;
        public static SyncMonitor<T> GetNew(string name) { return new SyncMonitor<T>() { Name = name }; }
        public SyncMonitor<T> SetID(int NetID, bool LinkToNet = true)
        {
            ID = NetID;
            FullName = NetID + "|" + Name;
            if (LinkToNet) 
            {
                NetDataCollection<T>.LinkToNet(this, NetID, FullName.GetHashCode());
            }

            {
                BuffSys.AddMonitor(this);
            }
            return this;
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class SyncMonitor<T>//value
    {
        T lastValue = default;
        Func<T> GetNowValue;
        Action<T> OnClientChangeAct;
        public virtual T Value
        {
            get { return GetNowValue(); }
            set { OnClientChangeAct(value); }
        }
        public SyncMonitor<T> SetGetSetFunction(Func<T> get, Action<T> set)
        {

            GetNowValue = get;
            lastValue = get();
            OnClientChangeAct = set;
            return this;
        }
    }


    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class SyncMonitor<T> : INetDataMonitor<T>
    {
        public int NetHashCode { get; set; }

        public void Send(Action<int, T> WriteAction)
        {
            var now = GetNowValue();
            if (now.Equals(lastValue)) return;
            //todo check if right
            WriteAction(NetHashCode, now);
            lastValue = now;
        }
    }

