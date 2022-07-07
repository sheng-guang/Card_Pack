using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Api]
partial class Buff//remove
{
    public string key;
    public LayerID Up { get; private set; }
    public void SetUp(LayerID u) { Up = u;  OnSetUp(); }
    public virtual void OnSetUp() { }
}
partial class Buff : ISetKV
{
    public virtual void SetKV(string key, object o) { }
}
partial class Buff//layer 
{
    public virtual string FullName { get; set; }= "";
}
public abstract partial class Buff : IBuff//ibuff
{
    public int NowVersion => BuffSys.FreshVersion;

    public bool Removed { get; set; } = false;

    public virtual void Apply() { }
    public virtual void FreshActive() { }
    public virtual void FreshRemove() { }
}
partial class Buff
{
    public override string ToString()
    {
        return Up.ToString()+":   "+FullName+"["+"]";
    }
}



