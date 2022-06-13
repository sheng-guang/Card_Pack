using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
public class Buff_Delegate : Buff
{
    public string _FullName;
    public override string FullName => _FullName;
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
