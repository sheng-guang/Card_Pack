using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System.Linq;
using System;

partial class eve//触发 call
{
    public static void DoCall(Call c)
    {
        CallSys.DoCall(c);
    }
}
partial class eve//技能 相关
{

    public static void PauseSkillForPlayer_EnsureComp(this Skill s,int PausePlayerID)
    {
        s.EnsureComp();
        s.ServerSetPausing(PausePlayerID);
    }
    public static int SN_TestPause(this Skill s)
    {
        if (s.IsPausing) return SkillNodeResult.Break_TryThisAgain;
        return SkillNodeResult.ToNext;
    }


    public static void AddSkillToLayerID(this Skill s, LayerID l,int SkillKindMask,byte kind=0)
    {
        l.AddSkill(s, SkillKindMask, kind);
    }
    public static void SetSkillImageName(this Skill s,string name)
    {
        s.FullName = name;
    }
    public static void SetSkillUp(this Skill s,LayerID up)
    {
        up.LinkToSkill(s);
    }
    public static void EnsureSkillComp(this Skill s)
    {
        s.EnsureComp();
    }
}

partial class eve//沉默 buff
{
    public static void RemoveBuff(Buff b)
    {
        b.RemoveSelf();
    }
    public static void Silence(Unit u)
    {
        u.RemoveAllBuff();
    }
}
partial class eve//伤害 摧毁
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
