using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public class StackSkill_Delegate : Skill_Delegate,IStackSkill
    {
        public StackSkill_Delegate()
        {
            A_StackStart.SetSelf(this);
            A_Run_ToBreak.SetSelf(this);
        }
        //stack
        public SkillNodeGroupOnce A_StackStart = new SkillNodeGroupOnce();
        public virtual void Stack_Start() { A_StackStart.Invoke(); }

        public SkillNodeGroupStack A_Run_ToBreak = new SkillNodeGroupStack();
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
}