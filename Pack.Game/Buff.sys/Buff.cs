using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{


    public interface IBuffOwner
    {
        void RemoveBuff(BuffBase b);
    }

    public abstract partial class Buff<T>:BuffBase where T:IBuffOwner//layer 
    {
        public T Up { get; private set; }
        public void SetUp(T u) { Up = u; owenr = u; OnSetUp();}
    }

    public abstract partial class BuffBase//remove
    {
        public virtual void OnSetUp() { }
        public IBuffOwner owenr { get; protected set; }
        public virtual void RemoveSelf()
        {

            owenr.RemoveBuff(this);
            BuffSysData.RemoveBuff(this);
        }
    }
    public abstract partial class BuffBase//layer 
    {
        public virtual string FullName => "";
    }
    public abstract partial class BuffBase : IBuff//ibuff
    {
        public int NowVersion => BuffSys.FreshVersion;
        public virtual void Apply() { }

        public virtual void FreshActive() { }

        public virtual void FreshRemove() { }
    }





}
