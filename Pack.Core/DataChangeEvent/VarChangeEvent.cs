using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class VarChangeEvent_Class<T>where T : class
    {
        Action onChange;
        public T lastValue { get; private set; } = null;
        public void NewData(T v)
        {
            if (lastValue==v) return;
            lastValue = v;
            onChange?.Invoke();
        }
        public void Listen(Action act)
        {

            onChange -= act;
            onChange += act;
            act.Invoke();
        }
    }
    public class VarChangeEvent<T> where T : struct, IEquatable<T>
    {

        Action onChange;
        public T? lastValue { get; private set; } = null;
        public void NewData(T v,ref bool changed)
        {
            var ischange=NewData(v);
            if(ischange) changed = true;
        }
        public bool NewData(T v)
        {
            if ((lastValue.HasValue) && (lastValue.Value.Equals(v))) return false;
            lastValue = v;
            onChange?.Invoke();
            return true;
        }
        //public void NewData(T v)
        //{
        //    NewData(v, ref _);
        //}
        public void NewData(N<T> v)
        {
            if(v.Equals(lastValue)) return;
            lastValue = v;
            onChange?.Invoke();
        }

        public void Listen(Action act)
        {

            onChange -= act;
            onChange += act;
            act.Invoke();
        }

    }




