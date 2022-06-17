
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class NetDataCollection<T> where T : IEquatable<T>
{
    static NetDataCollection()
    {
        //todo add to usedlist
        NewGameClear.AddToNewGameClearList(clear);
    }
    static void clear()
    {
        values.Clear();
        Monitors.Clear();
        changedIndexSet.Clear();
        changedData.Clear();
        changedOldData.Clear();
    }
    public static void ClientSetData(int index, T value)
    {
        if (values.ContainsKey(index) == false) values[index] = new PreData<T>();
        values[index].Value = value;
    }
    static void LinkToNet_(INetData<T> data,int NetID,int Which)
    {
        int index = (NetID + "|" + Which).GetHashCode();
        while (values.ContainsKey(index))
        {
            if (values[index] is PreData<T>) { data.Value = values[index].Value; break; }
            index++;
            Debug.LogWarning(NetID + "|" + Which + "   hash code exists");
        }
        values[index] = data;
        data.NetHashCode = index;
    }
    public static void LinkToNet(INetData<T> data, int NetID, int Which)
    {
        LinkToNet_(data,NetID,Which);
    }
    public static void LinkToNet(INetDataMonitor<T> data, int NetID, int Which)
    {
        LinkToNet_(data,NetID,Which);
        Monitors.Add(data);
    }
    static Dictionary<int, INetData<T>> values = new Dictionary<int, INetData<T>>();
    public static List<INetDataMonitor<T>> Monitors = new List<INetDataMonitor<T>>();
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    static HashSet<int> changedIndexSet = new HashSet<int>();
    static List<INetData<T>> changedData = new List<INetData<T>>();
    static List<T> changedOldData = new List<T>();

    public static void BeforeValueChange(INetData<T> data,T newData)
    {
        //todo equal
        if (data.Value.Equals(newData)) return;
        //if (data.Value.Equals(newData)) return;
        if (changedIndexSet.Contains(data.NetHashCode)) return;

        changedIndexSet.Add(data.NetHashCode);
        changedData.Add(data);
        changedOldData.Add(data.Value);
    }

    public static void Send(Action<int, T> writeFunc)
    {
        for (int i = 0; i < Monitors.Count; i++)
        {
            Monitors[i].Send(writeFunc);
        }
        //send2
        for (int i = 0; i < changedData.Count; i++)
        {
            //todo 
            var to = changedData[i];
            if (to.Value.Equals(changedOldData[i])) continue;
            writeFunc(to.NetHashCode, to.Value);
        }
        changedOldData.Clear();
        changedData.Clear();
        changedIndexSet.Clear();


    }
    
}

public class PreData<T> : INetData<T>
{
    public int NetHashCode { get;set;}

    public T Value { get; set; }
}
public interface INetData<T>
{

    int NetHashCode { get; set; }
     T Value { set; get; }
}
public interface INetDataMonitor<T>:INetData<T>
{
    void Send(Action<int, T> WriteAction);
}
