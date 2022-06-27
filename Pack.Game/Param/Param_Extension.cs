using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public static class Extention
    {

        public static IParam<T> ExParam<T>(this object obj, string DataName) where T : IEquatable<T>
        {
            var re = Extension<Param<T>>.EnsureExtension(obj, DataName);
            re.Name = DataName;
            return re;
        }
        public static IParam<T> ExParam<T>(this int hash, string DataName) where T : IEquatable<T>
        {
            var re = Extension<Param<T>>.EnsureExtension(hash, DataName);
            re.Name = DataName;
            return re;
        }
    }



