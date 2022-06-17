using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pack
{
    public static class OneTargetInputSkill
    {
        public const int FirstNodeKind = 1;

    }
    public abstract partial class InputSkill//static function set
    {
        public static void SetOneTarMode( InputSkill_Delegate s)
        {
            //writting
            s.F_AfterWrite_GetNext.AddNode(new AfterWrite_GetNext_LineNode());

            // test
            s.F_HighLightTest.AddNode(new HighLightTest_ManaEnough());
            s.F_TestNodeUseful.AddNode(new TestNodeUseful_In_Range())
                    .Set(nn.NodeKind, OneTargetInputSkill.FirstNodeKind)
                    .Set(nn.Reach, s.Ex_Ptr<N<float>>(nn.Reach));

            //preview
            s.F_NodeForm.AddNode(new NodeForm_Reach_OneTarget())
                .Set(nn.Reach, s.Ex_Ptr<N<float>>(nn.Reach))
                .Set(nn.HighThrow,s.Ex_Ptr<N<bool>>(nn.HighThrow));
            s.HasMoreThanOneTar = true;
        }
        /// <summary>
        ///  must add *.0f
        /// </summary>
        public static void EnableReach(InputSkill_Delegate s, object o)
        {
            var to= s.Ex_Ptr<N<float>>(nn.Reach).SetIGet(o);
          
            s.F_GetFloat.AddNode(new GetFloat_Reach()).Set(nn.Reach, to);
        }
        public static void EnableThrowLine(InputSkill_Delegate s, object HighThrow, IGet<N<int>> speed)
        {
            s.Ex_Ptr<N<bool>>(nn.HighThrow).SetIGet(HighThrow);
            
            s.F_GetBool.AddNode(new GetBool_IsHighThrow()).Set(nn.HighThrow, HighThrow);
            s.F_GetInt.AddNode(new GetInt_Speed()).Set(nn.speed, speed);
        }
        public static void EnableThrowOffset(InputSkill_Delegate s, IGet<N<float>> OffsetY, IGet<N<float>> OffsetXZ)
        {
            s.F_GetFloat.AddNode(new GetFloat_Offset())
                .Set(nn.OffSetY, OffsetY)
                .Set(nn.OffSetXZ,OffsetXZ);
        }


        //no static
        public static Action<Skill, InputForm, Skill> EmptyApply = null;
    }

    public abstract partial class InputSkill//const preview string
    {
        public const string Pre_Reach = "Pre'Reach";
        public const string Pre_Line = "Pre'Line";
        public const string Pre_Point = "Pre'Point";
        public const string Pre_ThrowLine = "Pre'ThrowLine";
    }


}