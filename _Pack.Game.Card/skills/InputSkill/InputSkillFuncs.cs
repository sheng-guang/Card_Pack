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
                    .Set(NN.NodeKind, OneTargetInputSkill.FirstNodeKind)
                    .Set(NN.Reach, s.Ex_Ptr<N<float>>(NN.Reach));

            //preview
            s.F_NodeForm.AddNode(new NodeForm_Reach_OneTarget())
                .Set(NN.Reach, s.Ex_Ptr<N<float>>(NN.Reach))
                .Set(NN.HighThrow,s.Ex_Ptr<N<bool>>(NN.HighThrow));
            s.HasMoreThanOneTar = true;
        }
        /// <summary>
        ///  must add *.0f
        /// </summary>
        public static void EnableReach(InputSkill_Delegate s, object o)
        {
            var to= s.Ex_Ptr<N<float>>(NN.Reach).SetIGet(o);
          
            s.F_GetFloat.AddNode(new GetFloat_Reach()).Set(NN.Reach, to);
        }
        public static void EnableThrowLine(InputSkill_Delegate s, object HighThrow, IGet<N<int>> speed)
        {
            s.Ex_Ptr<N<bool>>(NN.HighThrow).SetIGet(HighThrow);
            
            s.F_GetBool.AddNode(new GetBool_IsHighThrow()).Set(NN.HighThrow, HighThrow);
            s.F_GetInt.AddNode(new GetInt_Speed()).Set(NN.speed, speed);
        }
        public static void EnableThrowOffset(InputSkill_Delegate s, IGet<N<float>> OffsetY, IGet<N<float>> OffsetXZ)
        {
            s.F_GetFloat.AddNode(new GetFloat_Offset())
                .Set(NN.OffSetY, OffsetY)
                .Set(NN.OffSetXZ,OffsetXZ);
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