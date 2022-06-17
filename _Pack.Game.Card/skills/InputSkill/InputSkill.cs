using System;
using System.Collections.Generic;
using UnityEngine;

    public abstract partial class InputSkill//IHighLightInput
    {
        public HighLightStruct IsHighLightInput { get; private set; } = new HighLightStruct();
    }
    public abstract partial class InputSkill//test
    {
        //test
        public abstract bool HighLightTest();
        public abstract bool TestNodeUseful(InputNode n);
        public virtual bool FullTest(InputForm r)
        {

            if (HighLightTest() == false) return false;
            InputNode to = r;
            if (TestNodeUseful(to) == false) return false;
            //遍历每个树节点
            byte LastOption = 1;
            while (LastOption != 5)
            {
                bool moved = InputFormFunctions.MoveToNext(ref to, ref LastOption);
                if (moved == false) continue;
                if (LastOption == 1 || LastOption == 3)
                {
                    if (TestNodeUseful(to) == false) return false;
                }
            }
            return true;
        }
    }

    public abstract partial class InputSkill//get float int bool V3
    {
        //forPreView
        public virtual N<float> GetFloat(string DataName, int kind) { return null; }
        public virtual N<int> GetInt(string DataName, int kind) { return null; }
        public virtual N<bool> GetBool(string DataName, int kind) { return null; }
        public virtual N<Vector3> GetV3(string DataName, int kind) { return null; }
        public virtual IEnumerable<string> NodeForm(int kind) { return null; }
    }

    public abstract partial class InputSkill //move next
    {
        public virtual InputNode Write_GetNext(IInputData data,InputNode n,int Extrainfo)
        {
            if (Extrainfo.MaskContain(InputInfo.CanceAlllInput)) return InputNode.Canceled;
            if (n == null) return null;

            n.LayerID = data.LayerId;
            n.Point = data.Point;
            var re= AfterWrite_GetNext(n, Extrainfo);
            return re;
        }
        public abstract InputNode AfterWrite_GetNext(InputNode n, int Extrainfo);
    }

    public abstract partial class InputSkill//test and run
    {
        //run
        public virtual bool ServerTestToRun(InputForm f)
        {
            if (FullTest(f) == false) return false;
            Tested_Then_RunSkill(f);
            return true;
        }
        //test_run--------------------------------------------------------------------------------------
        public abstract void Tested_Then_RunSkill(InputForm f);
    }


    public abstract partial class InputSkill : Skill { }


    public abstract partial class InputSkill//kind
    {
        public virtual byte SkillKind { get; set; }

    }
    public abstract partial class InputSkill//get form
    {
        public InputForm GetEmptyForm() { return new InputForm().SetMasterSkill(this); }
    }



