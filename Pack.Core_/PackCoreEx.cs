using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
public static partial class eve//IDs
{
    //int ת Layer
    public static T To<T>(this int id)
    {
        return IDs<T>.Get(id);
    }
    public static T To<T>(this N<int> id)
    {
        if (id.HasValue == false) return default;
        return IDs<T>.Get(id);
    }
    public static bool To<T>(this N<int> id, out T re) where T : class
    {
        re = default;
        if (id.HasValue == false) { return false; }
        re = IDs<T>.Get(id.Value);
        return re != null;
    }
    public static bool To<T>(this int id, out T re) where T : class
    {
        re = IDs<T>.Get(id);
        return re != null;
    }
}
