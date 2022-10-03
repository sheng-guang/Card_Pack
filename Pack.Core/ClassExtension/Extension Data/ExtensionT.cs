using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Extension<T> where T : new()
{
    public static Dictionary<string, T> dic = new Dictionary<string, T>();
    public static T EnsureExtension(object obj, string DataName)
    {
        return EnsureExtension(obj.GetHashCode(), DataName);
    }
    public static T EnsureExtension(int hash, string DataName)
    {
        string key = hash + "|" + DataName;
        if (dic.TryGetValue(key, out var re)) return re;

        re = new T();
        dic[key] = re;
        ExtensionRecorder<ISetObj>.TryRecord(re, key);
        return re;
    }

}
public static class ExtensionRecorder<T>
{
    public static void TryRecord(object value, string key)
    {
        if (value is T == false) return;
        if (ForSeat.ContainsKey(key)) { Debug.LogWarning(typeof(T) + " exists  " + key); return; }
        ForSeat.Add(key, (T)value);
    }
    public static Dictionary<string, T> ForSeat = new Dictionary<string, T>();
    public static T Get(object obj, string DataName)
    {
        string key = obj.GetHashCode() + "|" + DataName;
        ForSeat.TryGetValue(key, out var re);
        return re;
    }
}


