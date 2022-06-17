using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pack
{

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class Param<T>:IBuffSysData //buff
    {

    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class Param<T> //Iid
    {
        public string Name { get; set; } = null;
        public static Param<T> GetNew(string name = null)
        {
            return new Param<T>() { Name = name }; 
        }
        public int ID { get; set; }
        public string FullName { get; set; } = null;
        public virtual Param<T> SetID(int NetID, bool LinkToNet = true)
        {
            if (Name == null) { Debug.Log("param   has no name"); return this; }
            ID = NetID;
            FullName = NetID + "|" + Name;

            if (LinkToNet)
            {
                LinkedToNet = true;
                NetDataCollection<T>.LinkToNet(this, NetID, Name.GetHashCode());
            }
            //{
            //}
            return this;
        }

    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class Param<T> : IParam<T> where T : IEquatable<T> //value
    {
        public static bool log = false;
        public override string ToString()
        {
            return "param  "+ NowValue.ToString();
        }
        T NowValue;
        public virtual T Value
        {
            get 
            {
               if(log) Debug.Log(NowValue);
                return NowValue; 
            }
            set
            {
                if (log) { Debug.Log(value); }

                isDefault = false;
                if (log) { Debug.Log("equal "+value.Equals(NowValue)); }

                if (value.Equals(NowValue)) return;

                if (isServer && LinkedToNet)
                {
                    NetDataCollection<T>.BeforeValueChange(this, value);
                }
                {
                    BuffSys.AddChangedData(this);
                }
                if (log) { Debug.Log(value); }
                NowValue = value;
                if (log) { Debug.Log(NowValue); }
                onChange?.Invoke();
            }
        }
        bool isDefault = true;
        public IParam<T> TrySetDefault(T value)
        {
            if (isDefault == false) return this;
            Value = value;
            return this;    
        }
        public IParam<T> SetValue(T value)
        {
            if(typeof( T) ==typeof(N<bool>))
            {
                //NullAble<bool> d = value as NullAble<bool>;

            }
            Value = value;
            return this;
        }


    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class Param<T>//change
    {
        public void Listen(Action act)
        {
            onChange += act;
            act.Invoke();
        }
        Action onChange = null;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class Param<T>: INetData<T>//net
    {
        public int NetHashCode { get; set; }
        bool LinkedToNet = false;
        static IGetSeat<bool> IsServer = Setting<bool>.GetSetting(nn.IsServer);
        bool isServer => IsServer.Value;
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial class Param<T>: ISetObj //bolt lua set data
    {
        public void setValue(object value)
        {
            if(value.TryTo<T>(out var re))
            {
                Value = re;
                return;
            }
            Debug.Log("param.set: [" + value + " , " + value.GetType().Name + "]  type can not cast to <" + typeof(T).Name+">"); return;
        }


        //public void SetValue_String(string s)
        //{
        //    //Debug.Log(this.GetHashCode() +"  set value" + s);
        //    if (cover == null) { Debug.Log("param<" + typeof(T) + "> have no cover function"); return; }
        //    Value = cover(s);
        //}
        //static Func<string, T> cover = null;
        ////cover
        //static Param()
        //{
        //    if (typeof(T) == typeof(int))
        //    {
        //        cover = (s) => { if (int.TryParse(s, out var re)) return (T)(object)re; return default; };
        //    }
        //    else if (typeof(T) == typeof(float))
        //    {
        //        cover = (s) => { if (float.TryParse(s, out var re)) return (T)(object)re; return default; };
        //    }
        //    else if (typeof(T) == typeof(bool))
        //    {
        //        cover = (s) => { if (bool.TryParse(s, out var re)) return (T)(object)re; return default; };
        //    }

        //}

    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------


}
