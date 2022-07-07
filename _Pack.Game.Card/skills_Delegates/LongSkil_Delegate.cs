using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILongSkil_Delegate:ISkill_Delegate
{
    SkillNodeGroupOnce fixStart { get; set; }
    SkillNodeGroupStep fix { get; set; }
    SkillNodeGroupStep fix50 { get; set; }
}

public class LongSkil_Delegate : Skill_Delegate, ILongSkill,ILongSkil_Delegate
{

    public LongSkil_Delegate()
    {
        fixStart.SetSelf(this);
        fix.SetSelf(this);
        fix50.SetSelf(this);
    }
    //long
    //1
    public SkillNodeGroupOnce fixStart { get; set; } = new SkillNodeGroupOnce();
    public virtual void Fix_Start() { fixStart.Invoke(); }
    //2
    public SkillNodeGroupStep fix { get; set; } = new SkillNodeGroupStep();
    public virtual void Fix() { fix.Invoke(); }
    //3
    public SkillNodeGroupStep fix50 { get; set; } = new SkillNodeGroupStep();
    public virtual void Fix50() { fix50.Invoke(); }
    public void SetExitListAction(Action<object> a)
    {
        exitList = a;
        fix.SetExistAction(a);
        fixStart.SetExistAction(a);
        fix50.SetExistAction(a);
    }

    Action<object> exitList { get; set; }
    public void ExitList() { exitList?.Invoke(this); }

}
