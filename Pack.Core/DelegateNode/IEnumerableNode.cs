using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IEnumerableNode<T1,Tr>
{
   
    public void AddFunc(Func<T1,IEnumerable<Tr>> Node)
    {
        list.Add(Node);
    }
    List<Func<T1, IEnumerable<Tr>>> list=new List<Func<T1,IEnumerable<Tr>>>();
    public IEnumerable<Tr> Invoke(T1 kind)
    {
        for (int i = 0; i < list.Count; i++)
        {
            foreach (var item in list[i].Invoke(kind))
            {
                yield return item;
            }
        }
    }
}
