using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerSkill_Delegate:ISkill_Delegate
{
    Act1<Skill, Call> A_OnCall { get; set; }
}
public class TriggerSkill_Delegate : Skill, ITriggerSkill_Delegate, ITriggerSkill
{
    public TriggerSkill_Delegate()
    {
        F_Visible.SetSelf(this);
        A_OnCall.SetSelf(this);
    }
    public Func0<Skill, bool> F_Visible { get; set; } = new Func0<Skill, bool>();

    public Act1<Skill,Call> A_OnCall { get; set; } = new Act1<Skill, Call> ();
    public void OnCall(Call call)
    {
        A_OnCall.Invoke(call);
    }
}
public class CallReaction_Delegate : ICallReaction
{
    public void Do()
    {
        A_Do.Invoke();
    }
    public Act0<CallReaction_Delegate> A_Do { get; set; } = new Act0<CallReaction_Delegate>();
}
