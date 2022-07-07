using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackSkill_Delegate:ISkill_Delegate
{
    SkillNodeGroupOnce stackStart { get; set; }
    SkillNodeGroupStack runToBreak { get; set; }

}
public class StackSkill_Delegate : Skill_Delegate, IStackSkill, IStackSkill_Delegate
{
    public StackSkill_Delegate()
    {
        stackStart.SetSelf(this);
        runToBreak.SetSelf(this);
    }
    //stack
    public SkillNodeGroupOnce stackStart { get; set; } = new SkillNodeGroupOnce();
    public virtual void Stack_Start() { stackStart.Invoke(); }

    public SkillNodeGroupStack runToBreak { get; set; } = new SkillNodeGroupStack();
    public virtual bool Run_ToBreak() { return runToBreak.Invoke(); }

    public void SetExistStackAction(Action<object> a)
    {
        existStack = a;
        stackStart.SetExistAction(a);
        runToBreak.SetExistAction(a);
    }

    Action<object> existStack { get; set; }
    public void ExistStack() { existStack?.Invoke(this); }
}
