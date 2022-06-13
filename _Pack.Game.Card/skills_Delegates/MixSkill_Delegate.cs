using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;

public interface IMixSkill_Delegate:ILongSkil_Delegate,IStackSkill_Delegate
{

}
public interface IMixSkill : ILongSkill, IStackSkill
{

}
//»ìºÏ°æ
public class MixSkill_Delegate : InputSkill_Delegate, IMixSkill
    , IMixSkill_Delegate
{
    public MixSkill_Delegate()
    {
        //long
        A_FixStart.SetSelf(this);
        A_Fix.SetSelf(this);
        A_Fix50.SetSelf(this);
        //stack
        A_StackStart.SetSelf(this);
        A_Run_ToBreak.SetSelf(this);
    }

    //long--------------------------------------------------------------------------------------
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
        existList = a;
        A_Fix.SetExistAction(a);
        A_FixStart.SetExistAction(a);
        A_Fix50.SetExistAction(a);
    }

    Action<object> existList { get; set; }
    public void ExistList() { existList?.Invoke(this); }

    //stack--------------------------------------------------------------------------------------
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


