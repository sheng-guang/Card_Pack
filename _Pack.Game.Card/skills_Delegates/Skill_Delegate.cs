using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill_Delegate
{
    Func0<Skill, bool> F_Visible { get; set; }
}
public class Skill_Delegate : Skill
{
    public Skill_Delegate()
    {
        F_Visible.SetSelf(this);
    }
    public Func0<Skill, bool> F_Visible { get; set; } = new Func0<Skill, bool>().SetDef(true);
    public override bool Visible => F_Visible.Invoke();

}


