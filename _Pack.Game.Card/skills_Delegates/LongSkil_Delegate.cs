using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILongSkil_Delegate:ISkill_Delegate
{
    SkillNodeGroupOnce A_FixStart { get; set; }
    SkillNodeGroupStep A_Fix { get; set; }
    SkillNodeGroupStep A_Fix50 { get; set; }
}

public class LongSkil_Delegate : Skill_Delegate, ILongSkill,ILongSkil_Delegate
{

    public LongSkil_Delegate()
    {
        A_FixStart.SetSelf(this);
        A_Fix.SetSelf(this);
        A_Fix50.SetSelf(this);
    }
    //long
    //1
    public SkillNodeGroupOnce A_FixStart { get; set; } = new SkillNodeGroupOnce();
    public virtual void Fix_Start() { A_FixStart.Invoke(); }
    //2
    public SkillNodeGroupStep A_Fix { get; set; } = new SkillNodeGroupStep();
    public virtual void Fix() { A_Fix.Invoke(); }
    //3
    public SkillNodeGroupStep A_Fix50 { get; set; } = new SkillNodeGroupStep();
    public virtual void Fix50() { A_Fix50.Invoke(); }
    public void SetExitListAction(Action<object> a)
    {
        exitList = a;
        A_Fix.SetExistAction(a);
        A_FixStart.SetExistAction(a);
        A_Fix50.SetExistAction(a);
    }

    Action<object> exitList { get; set; }
    public void ExitList() { exitList?.Invoke(this); }

}
