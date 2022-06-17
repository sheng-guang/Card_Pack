using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeCallSkill_Delegate:ISkill_Delegate
{
    SkillNodeGroupLoop A_Fix { get; set; }
}
public class BeCallSkill_Delegate : Skill, IBeCallSkill,IBeCallSkill_Delegate
{
    public BeCallSkill_Delegate()
    {
        F_Visible.SetSelf(this);
        A_Fix.SetSelf(this);
    }
    public SkillNodeGroupLoop A_Fix { get; set; } = new SkillNodeGroupLoop();
    public Func0<Skill, bool> F_Visible { get; set; }=new Func0<Skill, bool>();

    public virtual void Fix() { A_Fix.Invoke(); }


}

