using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface ISetKV
    {
        void setKV(string key, object o);
    }
    public interface ISet<T>
    {
        T Value { set; }
    }
