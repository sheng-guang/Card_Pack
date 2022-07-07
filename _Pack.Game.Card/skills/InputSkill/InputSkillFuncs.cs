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
        s.afterWrite_GetNext.AddFunc((inpusk, node, Extrainfo) =>
        {
            if (node.NodeKind == 0) { s.afterWrite_GetNext.re(node.CreatChild(1)); return; }
            else if (node.NodeKind == 1)
            {
                if (Extrainfo.MeansToNext() && s.TestNodeUseful(node))
                { s.afterWrite_GetNext.re(InputNode.FinishNode); return; }
            }
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
        //testThenRun
        s.tested_Then_RunSkill.AddAct((s, inputForm) =>
        {
            eve.ApplyOneTarInputToSkill(s, inputForm);
        });
    }


    /// <summary>
    ///  must add *.0f
    /// </summary>
    public static void EnableReach(InputSkill_Delegate s, object reac)
    {
        var reach= s.Ex<N<float>>(nn.Reach).SetIGet(reac);
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
        s.getFloat.AddFunc((sk, key, kind) =>
        {
            if (key != nn.Reach) return;
            s.getFloat.re(reach.Value);
        });
    }
    public static void EnableThrowLine(InputSkill_Delegate s, object HighThrow, IGet<N<int>> speed)
    {
        var highThrow= s.Ex<N<bool>>(nn.HighThrow).SetIGet(HighThrow);

        s.forEachNodeForm.AddAct((s, act, kind) =>
        {
            if (kind == 1) act(Pre_ThrowLine);
        });
        s.getBool.AddFunc((sk, key, kind) =>
        {
            if (key != nn.HighThrow) return;
            if (kind != 1) return;
            if (highThrow.Value.HasValue == false) return;
            s.getBool.re(highThrow.Value);
        });
        s.getInt.AddFunc((sk, key, kind) =>
        {
            if (key != nn.speed) return;
            if (kind != 1) return;
            if (s.unit.speed.Value_Buffed.HasValue == false) return;
            s.getInt.re(s.unit.speed.Value_Buffed);
        });


        //s.Ex<N<bool>>(nn.HighThrow).SetIGet(HighThrow);

        //s.getBool.AddNode(new GetBool_IsHighThrow()).Set(nn.HighThrow, HighThrow);
        //s.getInt.AddNode(new GetInt_Speed()).Set(nn.speed, speed);
    }
    public static void EnableThrowOffset(InputSkill_Delegate s, IGet<N<float>> OffsetY, IGet<N<float>> OffsetXZ)
    {
        s.getFloat.AddFunc((x, key, NodeKind) =>
        {
            if (NodeKind != 1) return;
            if (key == nn.OffSetY)
            {
                s.getFloat.re(OffsetY.Value);
            }
            else if (key == nn.OffSetXZ)
            {
                s.getFloat.re(OffsetXZ.Value);
            }
        });
        //s.getFloat.AddNode(new GetFloat_Offset())
        //    .Set(nn.OffSetY, OffsetY)
        //    .Set(nn.OffSetXZ, OffsetXZ);
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


