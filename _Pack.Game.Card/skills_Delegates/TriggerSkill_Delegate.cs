using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerSkill_Delegate:ISkill_Delegate
{
    Act1<Skill, Call> onCall { get; set; }
}
public class TriggerSkill_Delegate : Skill, ITriggerSkill_Delegate, ITriggerSkill
{
    public TriggerSkill_Delegate()
    {
        visible.SetSelf(this);
        onCall.SetSelf(this);
    }
    public Func0<Skill, bool> visible { get; set; } = new Func0<Skill, bool>();

    public Act1<Skill,Call> onCall { get; set; } = new Act1<Skill, Call> ();
    public void OnCall(Call call)
    {
        onCall.Invoke(call);
    }
}
public class CallReaction_Delegate : ICallReaction
{
    CallReaction_Delegate()
    {
        A_Do.SetSelf(this);
    }
    public void Do()
    {
        A_Do.Invoke();
    }
    public Act0<CallReaction_Delegate> A_Do { get; set; } = new Act0<CallReaction_Delegate>();
}
