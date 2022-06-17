using System;
using System.Collections.Generic;
using UnityEngine;



    //各种扩展方法
    //List 扩展
    public static class ListExtra
    {
        public static void EnsureIndex<T>(ref List<T> list, int index)
        {
            while (list.Count > index == false) { list.Add(default); }
        }

        public static void EnsureIndex<T>(this List<T> list, int index)
        {
            while (list.Count > index == false) { list.Add(default); }
        }

        public static void EnsureIndex_ThenSet<T>(this List<T> list, int index, T ToSet)
        {
            while (list.Count > index == false) { list.Add(default); }
            list[index] = ToSet;
        }

        public static T EnsureElement<T>(this List<T> list, int index, Func<T> getElementFunc)
        {
            list.EnsureIndex(index);
            if (list[index] == null) { list[index] = getElementFunc(); }
            return list[index];
        }
        public static bool HaveIndex<T>(this IList<T> list, int index)
        {
            if (index < 0 || list.Count > index == false) return false;
            return true;
        }

        public static void SetAsMaxIfOverMax<T>(this IList<T>list,ref int index)
        {
            if(list.Count>index==false)index=list.Count-1;
        }

        public static bool IndexOverMax<T>(this IList<T> list, int index)
        {
            if (list.Count > index ) return false;
            return true;
        }
        public static int MaxIndex<T>(this IList<T> list)
        {
            return list.Count -1;
        }

        public static T GetElement<T>(this List<T> list, int index)
        {
            if (index < 0 || index < list.Count == false) { return default; }
            return list[index];
        }

        public static T EnsureElement<T>(this Dictionary<string, T> dic, string index) where T : new()
        {
            if (dic.ContainsKey(index) == false) { dic.Add(index, default); }
            if (dic[index] == null) { dic[index] = new T(); }
            return dic[index];
        }

    }


    public static class DictionaryExtra
    {
        public static v Ensure<k,v>(this Dictionary<k,v> dic,k key)where v : new()
        {
            if(dic.TryGetValue(key,out var re)==false)
            {
                re= new v();
                dic.Add(key,re);
            }
            return re;
        }
       
        public static v Ensure_SetKey<k, v>(this Dictionary<k, v> dic, k key) where v :ISet<k>, new()
        {
            if (dic.TryGetValue(key, out var re) == false)
            {
                re = new v();
                re.Value = key;
                dic.Add(key, re);
            }
            return re;
        }
    }




    //扩展
    public static class Extra
    {
        public static void ChangeTrype(object o,Type t)
        {
            Convert.ChangeType(o,t);
        }
        public static void ExChanged<T>(ref T a,ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        public static void Print(this object o)
        {
            Debug.Log(o);
        }

        public static T MarkAs<T>(this T t, out T name) { name = t; return t; }
        public static T Act<T>(this T t, Action<T> a) { a(t); return t; }

        public static bool NotNull_and_NotEqualNull<T>(this T t)
        {
            return t != null && (t.Equals(null) == false);
        }

        public static bool IsNull_or_EqualNull<T>(this T t)
        {
            return t == null || t.Equals(null);
        }

        //位运算
        public static bool MaskContain(this int LayerMask, int toTest)
        {
            return ((LayerMask & toTest) != 0);
        }

    }


