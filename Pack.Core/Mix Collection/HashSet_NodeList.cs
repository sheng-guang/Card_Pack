using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Pack
{
    //partial class HashSet_NodeList<T> : IEnumerable<T>
    //{
    //    public class HashSet_NodeListEnum : IEnumerator<T>
    //    {
    //        public T Current => throw new NotImplementedException();

    //        object IEnumerator.Current => throw new NotImplementedException();

    //        public void Dispose()
    //        {
    //        }

    //        public bool MoveNext()
    //        {
    //        }

    //        public void Reset()
    //        {
    //        }
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        yield break;
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
    public partial class HashSet_NodeList <T>
    {
        public int Coumt=>dic.Count;
        class OneValue 
        {
            public T Value;
            public LinkedListNode<T> Node;
        }
        Dictionary<T, OneValue> dic=new Dictionary<T, OneValue> ();
        LinkedList<T> list=new LinkedList<T>();
        public void EnsureAdd(T ToAdd)
        {
            if (dic.ContainsKey(ToAdd)) return;
            var ne=new OneValue();
            ne.Value=ToAdd;
            ne.Node= list.AddLast(ToAdd);
            dic.Add(ToAdd, ne);
        }
        public void Remove(T to)
        {
            if (dic.ContainsKey(to) == false) return;
            var vv=dic[to];
            dic.Remove(to);
            list.Remove(vv.Node);
        }
        public void ForEach(Action<T> act)
        {
            var to = list.First;
            while (to!=null)
            {
                act(to.Value);
                to= to.Next;
            }
        }
        public void Clear()
        {
            dic.Clear();
            list.Clear();
        }
    }


}