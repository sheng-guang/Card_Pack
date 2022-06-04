using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
using System;
public interface IIsUnit
{
    Unit u { get; }
}
public class IsUnit : MonoBehaviour,IIsUnit
{
    
    public Unit u => unit;
    Unit unit;
    public Collider Unitcollider;
    //todo
    private void Awake()
    {
        Unitcollider = GetComponent<Collider>();
        ColliderToUnit.Add(Unitcollider, this);
        unit = GetComponentInParent<Unit>();

    }
}

public static class ColliderToUnit
{
    static ColliderToUnit()
    {
        Action c = clear;
        NewGameClear.AddToNewGameClearList(c);
    }
    static void clear() {
        dic.Clear();
    }
    static Dictionary<Collider, IIsUnit> dic=new Dictionary<Collider, IIsUnit>();
    public static void Add(Collider c,IIsUnit u)
    {
        dic[c] = u;
    }
    public static IIsUnit ToUnit(this Collider c )
    {
        //u = null;
         dic.TryGetValue(c, out var re);
        return re;
    }
    public static bool ToUnit(this Collider c,out IIsUnit u)
    {
        //u = null;
       var re= dic.TryGetValue(c, out  u);
        return re;
    }

}
