using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System.Linq;
using System;

using Pack;
partial class eve//call
{
    public static void DoCall(Call c)
    {
        CallSys.DoCall(c);
    }
}
partial class eve//skill
{
    public static void SetSkillUp(this Skill s,LayerID up)
    {
        up.LinkToSkill(s);
    }
    public static void EnsureSkillComp(this Skill s)
    {
        s.EnsureComp();
    }
}

public static partial class eve//damage
{
    public static void DestoryUnit(Unit u)
    {
        if (u == null) return;
        u.ToDestory.Value_Buffed = true;
    }
    public static void DoDamage(Unit u, int damage)
    {
        if (u == null) return;
        if (damage <= 0) return;
        if (u.HP.Value.HasValue == false) return;
        u.HP.Value -= damage;
    }
}
