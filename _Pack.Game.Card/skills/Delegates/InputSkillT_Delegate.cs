using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public class F0_Visible : Func0<Skill, bool> { }
    public class F1_NodeForm : Func1<InputSkill, int, IEnumerable<string>> { }
    public class F1_GetFloat : Func2<InputSkill, string, int, N<float>> { }
    public class F2_GetBool : Func2<InputSkill, string, int, N<bool>> { }


    public class F1_TestNodeUseful : Func1<InputSkill, InputNode, bool> { }

    public class InputSkill_Delegate:InputSkill
    {
        public InputSkill_Delegate()
        {
            F_Visible.SetSelf(this);
            
            F_NodeForm.SetSelf(this);
            F_GetFloat.SetSelf(this);
            F_GetBool.SetSelf(this);

            F_HighLightTest.SetSelf(this);
            F_TestNodeUseful.SetSelf(this);

            F_AfterWrite_GetNext.SetSelf(this);
            A_Tested_Then_RunSkill.SetSelf(this);

        }
        public F0_Visible F_Visible { get; set; } = new F0_Visible().Act(x=>x.SetDef(true));
        public override bool Visible => F_Visible.Invoke();

        //forPreView
        public F1_NodeForm F_NodeForm { get; set; } = new F1_NodeForm().Act(x=>x.SetDef(null));
        public override IEnumerable<string> NodeForm(int kind) { return F_NodeForm.Invoke(kind); }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public F1_GetFloat F_GetFloat { get; set; } = new F1_GetFloat().Act(x=>x.SetDef(null));
        public override N<float> GetFloat(string DataName, int kind) { return F_GetFloat.Invoke(DataName, kind); }

        public F2_GetBool F_GetBool { get; set; }= new F2_GetBool().Act(x=>x.SetDef(null));
        public override N<bool> GetBool(string DataName, int kind) { return F_GetBool.Invoke(DataName, kind); }

        public Func2<InputSkill, string, int, N<int>> F_GetInt { get; set; } = new Func2<InputSkill, string, int, N<int>>().SetDef(null);
        public override N<int> GetInt(string DataName, int kind) { return F_GetInt.Invoke(DataName, kind); }

        public Func2<InputSkill, string, int, N<Vector3>> F_GetV3 { get; set; } = new Func2<InputSkill, string, int, N<Vector3>>().SetDef(null);
        public override N<Vector3> GetV3(string DataName, int kind) { return F_GetV3.Invoke(DataName, kind); }
        //test----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public Func0<InputSkill, bool> F_HighLightTest { get; set; } = new Func0<InputSkill, bool>().SetDef(true);
        public override bool HighLightTest() { return F_HighLightTest.Invoke(); }

        public F1_TestNodeUseful F_TestNodeUseful { get; set; } = new F1_TestNodeUseful().Act(x => x.SetDef(true));
        public override bool TestNodeUseful(InputNode n) { return F_TestNodeUseful.Invoke(n); }

        // node tree
        public Func2<InputSkill, InputNode, int, InputNode> F_AfterWrite_GetNext { get; set; } = new Func2<InputSkill, InputNode, int, InputNode>();
        public override InputNode AfterWrite_GetNext(InputNode n, int Extrainfo) { return F_AfterWrite_GetNext.Invoke(n, Extrainfo); }

        //test_run--------------------------------------------------------------------------------------
        public Act1<InputSkill, InputForm> A_Tested_Then_RunSkill { get; set; } = new Act1<InputSkill, InputForm>();
        public override void Tested_Then_RunSkill(InputForm f) { A_Tested_Then_RunSkill.Invoke(f); }


    }

}