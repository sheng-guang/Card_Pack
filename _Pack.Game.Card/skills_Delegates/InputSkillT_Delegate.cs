using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputSkill_Delegate:ISkill_Delegate
{
    Act2<InputSkill,Action<string>,int> forEachNodeForm { get;set;}
    Func2<InputSkill, string, int, N<float>> getFloat { get; set; }
    Func2<InputSkill, string, int, N<bool>> getBool { get; set; }
    Func2<InputSkill, string, int, N<int>> getInt { get; set; }
    Func2<InputSkill, string, int, N<Vector3>> getV3 { get; set; }
    Func0<InputSkill, bool> highLightTest { get; set; }
    Func1<InputSkill, InputNode, bool> testNodeUseful { get; set; }

    Func2<InputSkill, InputNode, int, InputNode> afterWrite_GetNext { get; set; }
    Act1<InputSkill, InputForm> tested_Then_RunSkill { get; set; }
}
[Api]
public class InputSkill_Delegate : InputSkill,ISkill_Delegate,IInputSkill_Delegate
{
    public override void OnSetID_LoadStructure() => onSetID_LoadStructure.Invoke();
    public Act0<InputSkill> onSetID_LoadStructure { get; private set; } = new Act0<InputSkill>();
    public override void SetKV(string key, object o)
    {
        base.SetKV(key, o);
        setKV.Invoke(key, o);
    }
    public Act2<InputSkill, string, object> setKV { get; private set; } = new Act2<InputSkill, string, object>();
    public InputSkill_Delegate()
    {
        setKV.SetSelf(this);
        onSetID_LoadStructure.SetSelf(this);

        visible.SetSelf(this);
        forEachNodeForm.SetSelf(this);
        getFloat.SetSelf(this);
        getBool.SetSelf(this);
        getInt.SetSelf(this);
        getV3.SetSelf(this);

        highLightTest.SetSelf(this);
        testNodeUseful.SetSelf(this);

        afterWrite_GetNext.SetSelf(this);
        tested_Then_RunSkill.SetSelf(this);

    }
    public Func0<Skill, bool> visible { get; set; } = new Func0<Skill, bool>().Act(x => x.SetDef(true));
    public override bool Visible => visible.Invoke();

    //forPreView
    public Act2<InputSkill, Action<string>, int> forEachNodeForm { get; set; } = new Act2<InputSkill, Action<string>, int>();
    //public Func1<InputSkill, int, IEnumerable<string>> nodeForm { get; set; } = new Func1<InputSkill, int, IEnumerable<string>>().Act(x => x.SetDef(null));
    public override void ForEachNodeForm(Action<string> act, int kind)    {        forEachNodeForm.Invoke(act, kind);    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public Func2<InputSkill, string, int, N<float>> getFloat { get; set; } = new Func2<InputSkill, string, int, N<float>>().Act(x => x.SetDef(null));
    public override N<float> GetFloat(string DataName, int kind) { return getFloat.Invoke(DataName, kind); }

    public Func2<InputSkill, string, int, N<bool>> getBool { get; set; } = new Func2<InputSkill, string, int, N<bool>>().Act(x => x.SetDef(null));
    public override N<bool> GetBool(string DataName, int kind) { return getBool.Invoke(DataName, kind); }

    public Func2<InputSkill, string, int, N<int>> getInt { get; set; } = new Func2<InputSkill, string, int, N<int>>().SetDef(null);
    public override N<int> GetInt(string DataName, int kind) { return getInt.Invoke(DataName, kind); }

    public Func2<InputSkill, string, int, N<Vector3>> getV3 { get; set; } = new Func2<InputSkill, string, int, N<Vector3>>().SetDef(null);
    public override N<Vector3> GetV3(string DataName, int kind) { return getV3.Invoke(DataName, kind); }
    //test----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public Func0<InputSkill, bool> highLightTest { get; set; } = new Func0<InputSkill, bool>().SetDef(true);
    public override bool HighLightTest() { return highLightTest.Invoke(); }

    public Func1<InputSkill, InputNode, bool> testNodeUseful { get; set; } = new Func1<InputSkill, InputNode, bool>().Act(x => x.SetDef(true));
    public override bool TestNodeUseful(InputNode n) { return testNodeUseful.Invoke(n); }

    // node tree
    public Func2<InputSkill, InputNode, int, InputNode> afterWrite_GetNext { get; set; } = new Func2<InputSkill, InputNode, int, InputNode>();
    public override InputNode AfterWrite_GetNext(InputNode n, int Extrainfo) { return afterWrite_GetNext.Invoke(n, Extrainfo); }

    //test_run--------------------------------------------------------------------------------------
    public Act1<InputSkill, InputForm> tested_Then_RunSkill { get; set; } = new Act1<InputSkill, InputForm>();
    public override void Tested_Then_RunSkill(InputForm f) { tested_Then_RunSkill.Invoke(f); }


}

