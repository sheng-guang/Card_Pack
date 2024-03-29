using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Api]
public struct SkillBuilder
{

    static int LastSettingID;
    static int GetNewSettingID()
    {
        return ++LastSettingID;
    }
    public SkillBuilder SetName(string name)
    {
        FullName = name;
        return this;
    }

    public Skill Copy(Skill origin)
    {
        FullName = origin.FullName;
        Dir = origin.dirPass;
        ClassSetting = origin.ClassSetting;
        return GetSkill();

    }
    public Skill Origin()
    {
        return GetSkill();
    }
    public Skill Branch(Skill group)
    {
        GroupSetting = group.groupData;
        return GetSkill();
    }
    public SkillBuilder SetUp(LayerID l)
    {
        up = l;
        return this;
    }
    public SkillBuilder AddFinalAction(Action<Skill> act)
    {
        FinalAct += act;
        return this;
    }
    Action<Skill> FinalAct;
    public string Dir;
    public string FullName;
    int GroupSetting;
    int ClassSetting;
    LayerID up;



    Skill GetSkill()
    {
        if (FullName == null) return null;
        
        var re = Dir != default ? Creater<Skill>.GetNew(Dir, FullName) : Creater<Skill>.GetNew(FullName);
        if (re == null) return null;

        if (GroupSetting == default) GroupSetting = GetNewSettingID();
        if (ClassSetting == default) ClassSetting = GetNewSettingID();
        //Debug.Log("  on build  1:" + ClassSetting + "     " + GroupSetting);
        re.groupData = GroupSetting;
        re.ClassSetting = ClassSetting;
        //Debug.Log("  on build  2:" + re.ClassSetting + "     " + re.groupData);

        //Debug.Log("id[" + re.ID + "]      upid[" + re.UPID.Value + "]");
        if (up != null)
        {
            re.SetUP(up);
            //Debug.Log("id[" + re.ID + "]      upid[" + re.UPID.Value + "]");
        }
        re.SetID(IDs.NextID);
        //Debug.Log("id[" + re.ID + "]      upid[" + re.UPID.Value + "]");


        FinalAct?.Invoke(re);
        //Debug.Log("  on build  3:" + re.ClassSetting + "     " + re.groupData);


        return re;
    }

}

