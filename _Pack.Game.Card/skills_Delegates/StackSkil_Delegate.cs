using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
public interface IStackSkill_Delegate:ISkill_Delegate
{
    SkillNodeGroupOnce A_StackStart { get; set; }
    SkillNodeGroupStack A_Run_ToBreak { get; set; }

}
public class StackSkill_Delegate : Skill_Delegate, IStackSkill, IStackSkill_Delegate
{
    public StackSkill_Delegate()
    {
        A_StackStart.SetSelf(this);
        A_Run_ToBreak.SetSelf(this);
    }
    //stack
    public SkillNodeGroupOnce A_StackStart { get; set; } = new SkillNodeGroupOnce();
    public virtual void Stack_Start() { A_StackStart.Invoke(); }

    public SkillNodeGroupStack A_Run_ToBreak { get; set; } = new SkillNodeGroupStack();
    public virtual bool Run_ToBreak() { return A_Run_ToBreak.Invoke(); }

    public void SetExistStackAction(Action<object> a)
    {
        existStack = a;
        A_StackStart.SetExistAction(a);
        A_Run_ToBreak.SetExistAction(a);
    }

    Action<object> existStack { get; set; }
    public void ExistStack() { existStack?.Invoke(this); }
}
