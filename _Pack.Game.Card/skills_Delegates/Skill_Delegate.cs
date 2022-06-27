using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ISkill_Delegate
{
    Func0<Skill, bool> F_Visible { get; set; }
}
public class Skill_Delegate : Skill
{
    public Skill_Delegate()
    {
        //this.Ex_Ptr<Action<string,object>>(nn.setKV).set
        F_Visible.SetSelf(this);
    }
    public Func0<Skill, bool> F_Visible { get; set; } = new Func0<Skill, bool>().SetDef(true);
    public override bool Visible => F_Visible.Invoke();

    public Act2<Skill, string, object> A_setKV { get; private set; } = new Act2<Skill, string, object>();
    public override void setKV(string key, object o) { A_setKV.Invoke(key, o); }

}


