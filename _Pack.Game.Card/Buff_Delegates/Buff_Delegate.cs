using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Delegate : Buff
{
    public Buff_Delegate()
    {
        A_setKV.SetSelf(this);
        A_FreshActive.SetSelf(this);
        A_FreshRemove.SetSelf(this);
        A_Apply.SetSelf(this);
    }
    public Act2<Buff, string, object> A_setKV { get; set; } = new Act2<Buff, string, object>();
    public override void setKV(string key, object o)
    {
        base.setKV(key, o);
        A_setKV.Invoke(key, o);
    }


    public Act0<Buff> A_FreshActive { get; set; }=new Act0<Buff>();
    public override void FreshActive()
    {
        A_FreshActive.Invoke();
    }
    public Act0<Buff> A_FreshRemove { get; set; } = new Act0<Buff>();

    public override void FreshRemove()
    {
        A_FreshRemove.Invoke();
    }
    public Act0<Buff> A_Apply { get; set; } = new Act0<Buff>();

    public override void Apply()
    {
        A_Apply.Invoke();
    }
}
