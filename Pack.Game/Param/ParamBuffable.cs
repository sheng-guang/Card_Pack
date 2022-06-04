using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Pack
{
   
    public partial class ParamBuffable<T> : IBuffSysBuffableData//back
    {
        public void BackToBase()
        {
            BackToBaseData();
        }
    }

    public partial class ParamBuffable<T> : IBackToBaseData, ISetParamBuffable where T : IEquatable<T>
    {
        string name;
        public ParamBuffable(string name = null)
        {
            this.name = name;
            BuffedData = Param<T>.GetNew(name);
            BaseData = Param<T>.GetNew(name + "|Base");
        }
        public virtual ParamBuffable<T> SetID(int NetID, bool LinkToNet = true)
        {
            BuffedData.SetID(NetID, LinkToNet);
            BaseData.SetID(NetID, LinkToNet);
            return this;
        }

        public void BackToBaseData()
        {
            Value_Buffed = Value_Base;
        }

        public void SetValueBuffed(object o)
        {
            if (o.TryTo(out T re))
            {
                Value_Buffed = re;
                return;
            }
            Debug.Log(o + " <" + o.GetType().Name + "> type is not <" + typeof(T).Name + ">");

        }

        public void SetValueBase(object o)
        {
            if(o.TryTo(out T re))
            {
                Value_Base = re;
                return;
            }
            Debug.Log(o + " <" + o.GetType().Name + "> type is not <" + typeof(T).Name + ">");

        }



        Param<T> BuffedData = null;
        Param<T> BaseData = null;
        public T Value_Base
        {
            get { return BaseData.Value; }
            set
            {
                //Debug.Log("set BaseData");
                BaseData.Value = value;
                BuffSys.AddNeedFreshData(this);
            }
        }
        public T Value_Buffed
        {
            get { return BuffedData.Value; }
            set
            {
                BuffedData.Value = value;

                BuffSys.AddChangedData(this);
            }
        }
        public IGet<T> Value_Buffed_IGet => BuffedData;
        public override string ToString()
        {
            return GetType().Name+"<"+typeof(T)+">";
        }
    }
}
