using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeGroup<T>where T :  class,new ()
{
    public T OnPoint { get; private set; }  = new T();
    public T Saved { get; private set; } = new T();
    public void ExChange()
    {
        T temp = OnPoint;
        OnPoint = Saved;
        Saved = temp;
    }
        
}
