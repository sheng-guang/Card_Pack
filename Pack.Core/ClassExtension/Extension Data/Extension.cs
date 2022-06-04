using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public static class Extension
    {
        public static IGetSeat<T> Ex_Ptr<T>(this object obj, string DataName)
        {
            if (obj == null) { Debug.Log("Ex_Ptr  null  obj"); return null; }
            var re = Extension<IGetSeat<T>>.EnsureExtension(obj, DataName);
            return re;
        }
        public static IGetSeat<T> Ex_Ptr<T>(this int hash,string DataName)
        {
            var re = Extension<IGetSeat<T>>.EnsureExtension(hash, DataName);
            return re;
        }
    }

}
