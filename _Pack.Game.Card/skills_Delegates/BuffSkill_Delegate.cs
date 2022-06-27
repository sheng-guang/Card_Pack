using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Api]
public class BuffSkill_Delegate :BuffSkill
{
    public override void FreshActive()
    {
        base.FreshActive();
        A_FreshActive.Invoke();
    }
    public Act0<BuffSkill> A_FreshActive { get; private set; } =new Act0<BuffSkill>();
    public override void FreshRemove()
    {
        base.FreshRemove();
        A_FreshRemove.Invoke();
    }
    public Act0<BuffSkill> A_FreshRemove { get; private set; } =new Act0<BuffSkill>();

    public override void Apply()
    {
        base.Apply();
        A_Apply.Invoke();
    }
    public Act0<BuffSkill> A_Apply { get; private set; } =new Act0<BuffSkill>();
    public override void OnSetID_LoadStructure()
    {
        base.OnSetID_LoadStructure();
        A_OnSetID_LoadStructure.Invoke();
    }
    public Act0<BuffSkill> A_OnSetID_LoadStructure { get; private set; } =new Act0<BuffSkill>();

}
