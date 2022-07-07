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
partial class eve//设置图标
{
    public static void SetSkillImageName(this Skill s, string name)
    {
        s.FullName = name;
    }
    public static void SetBuffImageName(this Buff b, string name)
    {
        b.FullName = name;
    }
}
partial class eve//技能 相关
{
    public static void ApplyOneTarInputToSkill(Skill s,InputForm f)
    {
        Skill.ApplyOneTargetInput(s, f);
    }
    public static void PauseSkillForSeconds(this Skill s,float t)
    {
        s.ServerSetPauseTime(t);
    }
    public static void PauseSkillForPlayer(this Skill s,int PausePlayerID)
    {
        s.ServerSetPausing(PausePlayerID);
    }
    public static void EnsureStackComp(this Skill s)
    {
        s.EnsureComp($"-'{nameof(StackSkillComp)}");
    }  
    public static void EnsureLongSkillComp(this Skill s)
    {
        s.EnsureComp($"-'{nameof(LongSkillComp)}");
    }
    public static void ReducePauseLeft(Skill s)
    {
        if (s.PauseTimeLeft > 0)
        {
            s.PauseTimeLeft -= TimeSetting.FixedDeltaTime;
        }
    }
    public static int SN_TestPause(this Skill s)
    {
        if (s.IsPausing)
        {
            ReducePauseLeft(s);
            return SNResult.Break_TryThisAgain;
        }
        return SNResult.ToNext;
    }


    public static void AddSkillToLayerID(this Skill s, LayerID l,int SkillKindMask,byte kind=0)
    {
        l.AddSkill(s, SkillKindMask, kind);
    }

    public static void SetSkillUp(this Skill s,LayerID up)
    {
        up.LinkToSkill(s);
    }

}

partial class eve//沉默 buff
{
    public static void RemoveBuff(Buff b)
    {
        BuffSys.RemoveBuff(b);
        b.Up.buffs.RemoveBuff(b.key);
    }
    public static void Silence(LayerID u)
    {
        u.buffs.ForEach(x => BuffSys.RemoveBuff(x));
        u.buffs.Clear();
    }
    //public static void AddSilenceBUff(LayerID u,Buff b)
    //{
    //    u.buffs
    //}
    public static bool HasBuff(LayerID l, int From, string key)
    {
        return HasBuff(l, From + "|" + key);
    }
    public static bool HasBuff(LayerID l, string key)
    {
        return l.buffs.HasBuff(key);
    }
    public static bool TryGetBuff(LayerID l,string key,out Buff re)
    {
        return l.buffs.TryGetBuff(key, out re);
    }
    public static Buff GetBuff(LayerID l,string key)
    {
        return l.buffs.GetBuff(key);
    }
    public static void AddBuff(LayerID l, Buff b, int From, string key)
    {
        AddBuff(l, b, From + "|" + key);
    }
    public static void AddBuff(LayerID l, Buff b, string key)
    {
        b.SetUp(l);
        l.buffs.AddBuff(key, b);
    }
    public static void WillApplyToData( IBuff buff, IBuffSysBuffableData data)
    {
        BuffSys.WillApplyToData(buff, data);
    }
    public static void ForEachBuff(LayerID l,Action<Buff> a)
    {
        l.buffs.ForEach(a);
    }
}
partial class eve// buff 和数据刷新
{
    public static void AddNeedFreshData(IBuffSysBuffableData data)
    {
        BuffSys.AddNeedFreshData(data);
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

partial class eve//新建
{
    public static Player CreatPlayer(string FullName)
    {
        var re = Layer.CreatPlayer(FullName);
        return re;
    }
    public static Unit CreatUnit(string FullName)
    {
        var to= Layer.CreatUnit(FullName);
        return to;
    }
    public static SkillBuilder CreatSkill<T>()
    {
        var re = Layer.CreatSkill<T>();
        return re;
    }
    public static SkillBuilder CreatSkill(string FullName)
    {
        var re = Layer.CreatSkill(FullName);
        return re;
    }
   public static void AddSkill(LayerID l,object skill,int ToAddMask, byte inputSkillKind=0)
    {
        l.AddSkill(skill, ToAddMask, inputSkillKind);
    }
    public static void AddSkillOnlyLink(LayerID l,object o)
    {
        l.LinkToSkill(o);
    }
    public static void AddToStackSkillList(IStackSkill s)
    {
        s.AddToStackSkillList();
    }
    public static void AddToLongSkillList(ILongSkill s)
    {
        s.AddToLongSkillList();
    }
}