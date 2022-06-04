using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public class BeCallSkill_Delegate :Skill, IBeCallSkill
    {
        public BeCallSkill_Delegate()
        {
            A_Fix.SetSelf(this);
        }
        public SkillNodeGroupLoop A_Fix { get; set; } = new SkillNodeGroupLoop();
        public virtual void Fix() { A_Fix.Invoke(); }


    }

}
