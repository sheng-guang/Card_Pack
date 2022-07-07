using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMixSkill_Delegate:ILongSkil_Delegate,IStackSkill_Delegate
{

}
public interface IMixSkill : ILongSkill, IStackSkill
{

}
//»ìºÏ°æ
public class MixSkill_Delegate : InputSkill_Delegate, IMixSkill, IMixSkill_Delegate
{

    public MixSkill_Delegate()
    {
        //long
        fixStart.SetSelf(this);
        fix.SetSelf(this);
        fix50.SetSelf(this);
        //stack
        stackStart.SetSelf(this);
        runToBreak.SetSelf(this);
    }

    //long--------------------------------------------------------------------------------------
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
        existList = a;
        fix.SetExistAction(a);
        fixStart.SetExistAction(a);
        fix50.SetExistAction(a);
    }

    Action<object> existList { get; set; }
    public void ExistList() { existList?.Invoke(this); }

    //stack--------------------------------------------------------------------------------------
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


