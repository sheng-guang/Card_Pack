using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace Pack
{

    public static partial class SetDataExtra
    {
        //reflection------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static Dictionary<Type, MethodInfo> methods = new Dictionary<Type, MethodInfo>();
        public static object Var(this object obj, string key, object value)
        {
            {
                if (obj is ISetKV) { (obj as ISetKV).setKV(key, value); return obj; }
            }
            {
                var t = obj.GetType();
                if (methods.ContainsKey(t) == false)
                {
                    var info = t.GetMethod(nn.TrySetMethodName);
                    methods.Add(t, info);
                }
                var m = methods[t];
                //not exist
                if (m == null) return obj;
                try { m.Invoke(obj, new object[] { key, value }); }
                catch (Exception e) { Debug.LogWarning(e); }
            }
            return obj;
        }
        ////------------------------------------------------------------------------------------------------------------------------------
        public static bool TryTo<T>(this object value,out T re)
        {
            re = default;
            return TryTo_ref(value,ref re);
        }
        public static bool TryTo_ref<T>(this object o, ref T re)
        {
            if(o is T)
            {
                re=(T)o;
                return true;
            }
            if (typeof(T) == typeof(int))
            {
                if(int.TryParse(o.ToString(), out var rere))
                {
                    re = (T)(object)rere;
                    return true;
                }
            }
            if (typeof(T) == typeof(float))
            {
                if (float.TryParse(o.ToString(), out var rere))
                {
                    re = (T)(object)rere;
                    return true;
                }
            }

            var to = o.ToIGet<T>();
            if (to != null) { re = to.Value; return true; }

            return false;
        }

        ////------------------------------------------------------------------------------------------------------------------------------
        public static bool TryToIGet<T>(this object value, out IGet<T> re)
        {
            re = default;
            return TryToIGet_ref(value, ref re);

        }
        public static bool TryToIGet_ref<T>(this object value,ref  IGet<T>re)
        {
            if (value is IGet<T>) { re = value as IGet<T>; return true; }
            if (value is Func<T>) { re = new IGet_Func<T>() { f = value as Func<T> };return true; }
            if (value is T) { re = new IGet_Value<T>() { Value = (T)value }; return true; }
            if (value.IsNull_or_EqualNull()) { re = new IGet_Value<T>(); return true; }
            {
                T r = default;
                if (r != null)
                {
                    try
                    {
                        dynamic d = r;
                        if (d.TrySet(value))
                        {
                            /*Debug.Log(d);*/
                            re= new IGet_Value<T>() { Value = d };
                            return true;
                        }
                    }
                    catch { }
                }
            }
            Debug.Log("TryToIGet: [" + value + " , " + value.GetType().Name + "]  can not Cast To IGet<" + typeof(T).Name + ">");
            return false;

        }
        public static IGet<T> ToIGet<T>(this object value)
        {
            IGet<T> re = null;
            value.TryToIGet_ref(ref re);
            return re;
        }

        //------------------------------------------------------------------------------------------------------------------------------
        public static ISet<T> ToISet<T>(this object value)
        {

            if (value is ISet<T>) return value as ISet<T>;
            else if (value is Action<T>) return new ISet_Action<T>() { a = value as Action<T> };
            else
            {
                Debug.Log("[" + value + "]  can not CastToISet T  " + typeof(T));
                return new ISet_Action<T>();
            }
        }

        public static IGetSeat<T> ToIGetSeat<T>(this object value)
        {
            return new IGetSeat<T>().SetIGet(value.ToIGet<T>());
        }
    }

    public class ISet_Action<T> : ISet<T>
    {
        public Action<T> a = null;
        public T Value { set => a?.Invoke(value); }
    }

}