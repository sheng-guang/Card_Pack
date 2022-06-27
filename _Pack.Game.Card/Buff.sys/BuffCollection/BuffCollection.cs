using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCollection
{
    public void LogSelf()
    {
        foreach (var item in dic)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value.Value.FullName);
        }
    }
    public int Coumt => dic.Count;
    class OneValue
    {
        public Buff Value;
        public LinkedListNode<Buff> Node;
    }
    Dictionary<string, OneValue> dic = new Dictionary<string, OneValue>();
    LinkedList<Buff> list = new LinkedList<Buff>();
    public bool TryGetBuff(string key,out Buff re)
    {
        bool b = dic.TryGetValue(key, out var onev);
        re = b ? onev.Value : null;
        return b;

    }
    public Buff GetBuff(string ket)
    {
        if (dic.TryGetValue(ket, out var onev)) return onev.Value;
        else return null;
    }
    public bool HasBuff(string key)
    {
        return dic.ContainsKey(key);
    }

    //public void AddBuffFromID(int ID, string key, Buff b) { AddBuff(ID + "|" + key, b); }
    public void AddBuff(string Key,Buff b)
    {
        if (dic.ContainsKey(Key)) return;
        var ne = new OneValue();
        ne.Value = b;
        ne.Node = list.AddLast(ne.Value);
        dic.Add(Key, ne);
    }

    //public void RemoveBuffFromID(int ID, string Key) { RemoveBuff(ID + "|" + Key); }
    public void RemoveBuff(string to)
    {
        if (dic.ContainsKey(to) == false) return;
        var vv = dic[to];
        dic.Remove(to);
        list.Remove(vv.Node);
    }
    public void ForEach(Action<Buff> act)
    {
        var to = list.First;
        while (to != null)
        {
            act(to.Value);
            to = to.Next;
        }
    }
    public void Clear()
    {
        dic.Clear();
        list.Clear();
    }
}
