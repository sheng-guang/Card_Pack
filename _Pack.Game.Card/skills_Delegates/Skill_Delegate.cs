using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ISkill_Delegate
{
    Func0<Skill, bool> visible { get; set; }
}
public class Skill_Delegate : Skill
{
    public Skill_Delegate()
    {
        //this.Ex_Ptr<Action<string,object>>(nn.setKV).set
        visible.SetSelf(this);
    }
    public Func0<Skill, bool> visible { get; set; } = new Func0<Skill, bool>().SetDef(true);
    public override bool Visible => visible.Invoke();

    public Act2<Skill, string, object> setKV { get; private set; } = new Act2<Skill, string, object>();
    public override void SetKV(string key, object o) { setKV.Invoke(key, o); }

}


