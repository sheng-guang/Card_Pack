using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputSkill_Delegate:ISkill_Delegate
{
    Func1<InputSkill, int, IEnumerable<string>> F_NodeForm { get; set; }
    Func2<InputSkill, string, int, N<float>> F_GetFloat { get; set; }
    Func2<InputSkill, string, int, N<bool>> F_GetBool { get; set; }
    Func2<InputSkill, string, int, N<int>> F_GetInt { get; set; }
    Func2<InputSkill, string, int, N<Vector3>> F_GetV3 { get; set; }
    Func0<InputSkill, bool> F_HighLightTest { get; set; }
    Func1<InputSkill, InputNode, bool> F_TestNodeUseful { get; set; }

    Func2<InputSkill, InputNode, int, InputNode> F_AfterWrite_GetNext { get; set; }
    Act1<InputSkill, InputForm> A_Tested_Then_RunSkill { get; set; }
}
[Api]
public class InputSkill_Delegate : InputSkill,ISkill_Delegate,IInputSkill_Delegate
{
    public override void OnSetID_LoadStructure() => A_OnSetID_LoadStructure.Invoke();
    public Act0<InputSkill> A_OnSetID_LoadStructure { get; private set; } = new Act0<InputSkill>();
    public override void setKV(string key, object o)
    {
        base.setKV(key, o);
        A_setKV.Invoke(key, o);
    }
    public Act2<InputSkill, string, object> A_setKV { get; private set; } = new Act2<InputSkill, string, object>();
    public InputSkill_Delegate()
    {
        A_OnSetID_LoadStructure.SetSelf(this);

        F_Visible.SetSelf(this);

        F_NodeForm.SetSelf(this);
        F_GetFloat.SetSelf(this);
        F_GetBool.SetSelf(this);
        F_GetInt.SetSelf(this);
        F_GetV3.SetSelf(this);

        F_HighLightTest.SetSelf(this);
        F_TestNodeUseful.SetSelf(this);

        F_AfterWrite_GetNext.SetSelf(this);
        A_Tested_Then_RunSkill.SetSelf(this);

    }
    public Func0<Skill, bool> F_Visible { get; set; } = new Func0<Skill, bool>().Act(x => x.SetDef(true));
    public override bool Visible => F_Visible.Invoke();

    //forPreView
    public Func1<InputSkill, int, IEnumerable<string>> F_NodeForm { get; set; } = new Func1<InputSkill, int, IEnumerable<string>>().Act(x => x.SetDef(null));
    public override IEnumerable<string> NodeForm(int kind) { return F_NodeForm.Invoke(kind); }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public Func2<InputSkill, string, int, N<float>> F_GetFloat { get; set; } = new Func2<InputSkill, string, int, N<float>>().Act(x => x.SetDef(null));
    public override N<float> GetFloat(string DataName, int kind) { return F_GetFloat.Invoke(DataName, kind); }

    public Func2<InputSkill, string, int, N<bool>> F_GetBool { get; set; } = new Func2<InputSkill, string, int, N<bool>>().Act(x => x.SetDef(null));
    public override N<bool> GetBool(string DataName, int kind) { return F_GetBool.Invoke(DataName, kind); }

    public Func2<InputSkill, string, int, N<int>> F_GetInt { get; set; } = new Func2<InputSkill, string, int, N<int>>().SetDef(null);
    public override N<int> GetInt(string DataName, int kind) { return F_GetInt.Invoke(DataName, kind); }

    public Func2<InputSkill, string, int, N<Vector3>> F_GetV3 { get; set; } = new Func2<InputSkill, string, int, N<Vector3>>().SetDef(null);
    public override N<Vector3> GetV3(string DataName, int kind) { return F_GetV3.Invoke(DataName, kind); }
    //test----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public Func0<InputSkill, bool> F_HighLightTest { get; set; } = new Func0<InputSkill, bool>().SetDef(true);
    public override bool HighLightTest() { return F_HighLightTest.Invoke(); }

    public Func1<InputSkill, InputNode, bool> F_TestNodeUseful { get; set; } = new Func1<InputSkill, InputNode, bool>().Act(x => x.SetDef(true));
    public override bool TestNodeUseful(InputNode n) { return F_TestNodeUseful.Invoke(n); }

    // node tree
    public Func2<InputSkill, InputNode, int, InputNode> F_AfterWrite_GetNext { get; set; } = new Func2<InputSkill, InputNode, int, InputNode>();
    public override InputNode AfterWrite_GetNext(InputNode n, int Extrainfo) { return F_AfterWrite_GetNext.Invoke(n, Extrainfo); }

    //test_run--------------------------------------------------------------------------------------
    public Act1<InputSkill, InputForm> A_Tested_Then_RunSkill { get; set; } = new Act1<InputSkill, InputForm>();
    public override void Tested_Then_RunSkill(InputForm f) { A_Tested_Then_RunSkill.Invoke(f); }


}

