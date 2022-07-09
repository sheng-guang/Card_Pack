using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class OneTargetInputSkill
{
    public const int FirstNodeKind = 1;
}

partial class InputSkill//static function set
{
    public static void SetOneTarMode(InputSkill_Delegate s)
    {
        s.HasMoreThanZeroTar = true;
        s.visible.AddFunc((skill) =>   
        {
            s.visible.re(eve.IsCardInHand(s.unit));
        });
        //s.afterWrite_GetNext.AddNode(new AfterWrite_GetNext_LineNode());
        s.afterWrite_GetNext.AddFunc((inpusk, node, Extrainfo) =>
        {
            //Debug.Log(node.NodeKind);
            if (node.NodeKind == 0)
            {
                s.afterWrite_GetNext.re(node.CreatChild(1)); return;
            }
            else if (node.NodeKind == 1)
            {
                if (Extrainfo.MeansToNext() && s.TestNodeUseful(node))
                { s.afterWrite_GetNext.re(InputNode.FinishNode); return; }
            }
            s.afterWrite_GetNext.re(node);
        });

        // test
        s.highLightTest.AddFunc((skill) =>
        {
            if (s.unit.ManaCost.Value_Buffed.HasValue == false) return;
            if (s.player.Mana.Value < s.unit.ManaCost.Value_Buffed) { s.highLightTest.re(false); return; }
        });
        //preview
        s.forEachNodeForm.AddAct((s, act, kin) =>
        {
            if (kin == 1) { act(Pre_Line); act(Pre_Point); return; }
        });
    }

    public static void EnableBranckSkill(InputSkill_Delegate s, object skillName,object ListKind)
    {
        s.ClassSetting.Str(nn.SkillName,skillName);
        s.ClassSetting.Int(nn.SkillListKind, ListKind);
        s.tested_Then_RunSkill.AddAct((sk, inputForm) =>
        {
            var to = eve.CreatSkill(sk.ClassSetting.Str(nn.SkillName)).Branch(sk);
            eve.SetSkillUp(to, sk.up);
            eve.ApplyOneTarInputToSkill(to, inputForm);
            eve.AddToSkillList(to, sk.ClassSetting.Int(nn.SkillListKind));
        });
    }

    /// <summary>
    ///  must add *.0f
    /// </summary>
    public static void EnableReach(InputSkill_Delegate s, object reac)
    {
        var reach = s.NFloat(nn.Reach, reac);
        s.testNodeUseful.AddFunc((sk, node) =>
        {
            if (reach.Value.HasValue == false) return;
            if (node.NodeKind != 1) return;
            bool inRange = s.NodeReachable(node, reach.Value.Value);
            if (inRange == false) { s.testNodeUseful.re(false); return; }
        });
        s.forEachNodeForm.AddAct((sk, act, kind) =>
        {
            if (kind == 0) act(Pre_Reach);
        });
        s.preFloat.AddFunc((sk, key, kind) =>
        {
            if (key != nn.Reach) return;
            s.preFloat.re(reach.Value);
        });
    }
    public static void EnableThrowLine(InputSkill_Delegate s, Func<bool> HighThrow)
    {
        EnableThrowLine(s, new IGetFunc<bool> (HighThrow));
    }
    public static void EnableThrowLine(InputSkill_Delegate s, object HighThrow)
    {
        var highThrow = s.ClassSetting.NBool(nn.HighThrow,HighThrow);
        var ss = s.ClassSetting.NInt(nn.speed,s.unit.speed.Value_Buffed_IGet);
        s.forEachNodeForm.AddAct((s, act, kind) =>
        {
            if (kind == 1) act(Pre_ThrowLine);
        });
        s.preBool.AddFunc((sk, key, kind) =>
        {
            if (key != nn.HighThrow) return;
            if (kind != 1) return;
            s.preBool.re(highThrow);
        });
        s.preInt.AddFunc((sk, key, kind) =>
        {
            if (key != nn.speed) return;
            if (kind != 1) return;
            s.preInt.re(ss);
        });

    }
    public static void EnableThrowOffset(InputSkill_Delegate s, object OffsetY,object OffsetXZ)
    {
        var Y = s.ClassSetting.NFloat(nn.OffSetY, OffsetY);
        var XZ = s.ClassSetting.NFloat(nn.OffSetY, OffsetXZ);

        s.preFloat.AddFunc((x, key, NodeKind) =>
        {
            if (NodeKind != 1) return;
            if (key == nn.OffSetY)
            {
                s.preFloat.re(Y);
            }
            else if (key == nn.OffSetXZ)
            {
                s.preFloat.re(XZ);
            }
        });
    }


    //no static
    public static Action<Skill, InputForm, Skill> EmptyApply = null;
}

partial class InputSkill//const preview string
{
    public const string Pre_Reach = "Pre'Reach";
    public const string Pre_Line = "Pre'Line";
    public const string Pre_Point = "Pre'Point";
    public const string Pre_ThrowLine = "Pre'ThrowLine";
}


