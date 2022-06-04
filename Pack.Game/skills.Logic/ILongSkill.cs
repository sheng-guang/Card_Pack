using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public interface ILongSkill
    {
        void SetExitListAction(Action<object> a);
        void Fix_Start();
        void Fix();
        void Fix50();
    }

    //public interface ILongSkil_Delegate
    //{
    //    SkillNodeGroupOnce A_FixStart { get; set; }
    //    SkillNodeGroupStep A_Fix { get; set; }
    //    SkillNodeGroupStep A_Fix50 { get; set; }
    //}

}