using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public struct N<T>:IEquatable<N<T>> where T : struct,IEquatable<T>
    {

        public override string ToString() { return "" + HasValue + " , " + v; }

        public static N<T> Null { get; private set; } = null;
         bool _h ;
        public bool NoValue => !HasValue;
        public bool HasValue
        {
            get=>_h;
            private set 
            {
                _h = value; 
            } 
        }
         T v;
        public T Value
        {
            get
            {
                if (!HasValue) { Debug.LogError("N<"+typeof(T).Name+">"+"  is null"); return default; }
                return v;
            }
            set
            {
                v = value;
                HasValue=v.NotNull_and_NotEqualNull();
            }
        }
        public bool TrySet(object o)
        {
            if( o.TryTo(out T v))
            {
                Value = v;
                return true;
            }
            return false;
        }
        public bool Equals(T? other)
        {
            if (other.HasValue == false && HasValue == false) return true;
            if ((other.HasValue && HasValue) && (Value.Equals(other.Value))) return true;
            return false;
        }
        public bool Equals(N<T> other)
        {
            //if (HasValue != other.HasValue) return false;
            if (HasValue==false&&other.HasValue==false) return true;
            return (HasValue == other.HasValue&& Value.Equals(other.Value));
        }

        public static implicit operator N<T>(T value)
        {
            var re=new N<T>();
            re.HasValue = true;
            re.v = value;
            return  re;
        }
        public static implicit operator N<T>(T? value)
        {
            var re = new N<T>();
            re.HasValue = value.HasValue;
            if (re.HasValue) re.v = value.Value;
            return re;
        }
        public static implicit operator T(N<T> value)
        {
            return value.Value;
        }
    }



