using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFixSkill_Delegate:ISkill_Delegate
{
    SkillNodeGroupLoop fix { get; set; }
}
public class FixSkill_Delegate : Skill, IFixSkill,IFixSkill_Delegate
{
    public FixSkill_Delegate()
    {
        visible.SetSelf(this);
        fix.SetSelf(this);
    }
    public SkillNodeGroupLoop fix { get; set; } = new SkillNodeGroupLoop();
    public Func0<Skill, bool> visible { get; set; }=new Func0<Skill, bool>();

    public virtual void Fix() { fix.Invoke(); }


}

