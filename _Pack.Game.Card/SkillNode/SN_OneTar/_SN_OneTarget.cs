using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{

    public class HighLightTest_ManaEnough : Func0Node<InputSkill, bool>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);

        }
        public override void Invoke()
        {
            if (self.unit.ManaCost.Value_Buffed.HasValue == false) return;
            if (self.player.Mana.Value < self.unit.ManaCost.Value_Buffed) { re(false); return; }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// reach
    /// </summary>
    public class NodeForm_Reach_OneTarget : Func1Node<InputSkill, int, IEnumerable<string>>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == NN.Reach) o.TryToIGet_ref(ref Reach);
            else if (key == NN.HighThrow) 
            {
                o.TryToIGet_ref(ref HighThrow);
                //Debug.Log(HighThrow);
            }
        }
        IGet<N<float>> Reach = null;
        IGet<N<bool>> HighThrow = null;

        static List<string> empty 
            = new List<string>();
        static List<string> NodeForm_Reach 
            = new List<string>() { InputSkill.Pre_Reach };

        static List<string> NodeForm_Line_Point 
            = new List<string>() { InputSkill.Pre_Line, InputSkill.Pre_Point };
        static List<string> NodeForm_Line_Point_Throw
            =new List<string> { InputSkill.Pre_Line, InputSkill.Pre_Point,InputSkill.Pre_ThrowLine };

        public override void Invoke(int p1)
        {
            if (p1 == 0) 
            { re(Reach!=null&&Reach.Value.HasValue ? NodeForm_Reach : empty); return; }
            else if (p1 == 1)
            {

                re(HighThrow!=null&&Reach.Value.HasValue? NodeForm_Line_Point_Throw:NodeForm_Line_Point);
                
                return; 
            }
            re(empty);
        }

    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public abstract class GetFloat_Node : Func2Node<InputSkill, string, int, N<float>> { }

    public class GetFloat_Reach : GetFloat_Node
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == NN.NodeKind) o.TryToIGet_ref(ref kind);
            else if (key == NN.Reach) o.TryToIGet_ref(ref reach);
        }
        public IGet<int> kind;
        public IGet<N<float>> reach;
        public override void Invoke(string p1, int p2)
        {
            if (p1 != NN.Reach) return;
            if(kind!=null&&kind.Value!=p2) return;
            if (reach.IsNull_or_EqualNull()) return;
            if (reach.Value.HasValue == false) return;
            re(reach.Value);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public  class GetFloat_Offset : Func2Node<InputSkill, string, int, N<float>>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == NN.NodeKind) o.TryToIGet_ref(ref kind);
            else if (key == NN.OffSetY) o.TryToIGet_ref(ref OffSetY);
            else if (key == NN.OffSetXZ) o.TryToIGet_ref(ref OffSetXZ);
        }
        public IGet<int> kind;
        public IGet<N<float>> OffSetY;
        public IGet<N<float>> OffSetXZ;
        public override void Invoke(string p1, int p2)
        {
            if (kind != null && kind.Value != p2) return;
            if (p1 == NN.OffSetY)
            {
                if (OffSetY.IsNull_or_EqualNull()) return;
                if (OffSetY.Value.HasValue == false) return;
                re(OffSetY.Value);
            }
            else if(p1== NN.OffSetXZ)
            {
                if (OffSetXZ.IsNull_or_EqualNull()) return;
                if (OffSetXZ.Value.HasValue == false) return;
                re(OffSetXZ.Value);
            }

        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public abstract class GetInt_Node : Func2Node<InputSkill, string, int, N<int>> { }
    public class GetInt_Speed : GetInt_Node
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == NN.NodeKind) o.TryToIGet_ref(ref kind);
            else if (key == NN.speed) o.TryToIGet_ref(ref Speed);
        }
        public IGet<int> kind;
        public IGet<N<int>> Speed;
        public override void Invoke(string p1, int p2)
        {
            if (p1 != NN.speed) return;
            if (kind != null && kind.Value != p2) return;
            if(Speed.IsNull_or_EqualNull()) return;
            if(Speed.Value.HasValue == false) return;
            re(Speed.Value);
        }
    }
    //----------------------------
    public abstract class GetBool_Node : Func2Node<InputSkill, string, int, N<bool>> { }
    public class GetBool_IsHighThrow : GetBool_Node
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == NN.NodeKind) o.TryToIGet_ref(ref kind);
            else if (key == NN.HighThrow) 
            {
                o.TryToIGet_ref(ref HighThrow);

            }
        }
        public IGet<int> kind;
        public IGet<N<bool>> HighThrow;
        public override void Invoke(string p1, int p2)
        {
            if (p1 != NN.HighThrow) return;
            if (kind != null && kind.Value != p2) return;
            if (HighThrow.IsNull_or_EqualNull()) return;
            if (HighThrow.Value.HasValue == false) return;
            re(HighThrow.Value);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// MaxInde
    /// </summary>
    public class AfterWrite_GetNext_LineNode : Func2Node<InputSkill, InputNode, int, InputNode>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == NN.MaxInde) o.TryToIGet_ref(ref MaxNodeIndex);
        }
        public IGet<int> MaxNodeIndex = (1).ToIGet<int>();
        public override void Invoke(InputNode p1, int p2)
        {
            if (MaxNodeIndex.Value < 1) { re(InputNode.FinishNode); return; }
            if (p1.NodeKind == 0) { re(p1.CreatChild(1)); return; }
            else if (p1.NodeKind == MaxNodeIndex.Value)
            {
                if (p2.MeansToNext() && self.TestNodeUseful(p1))
                {
                    re(InputNode.FinishNode); return;
                }
            }
            else if (p1.NodeKind < MaxNodeIndex.Value)
            {
                if (p2.MeansToNext() && self.TestNodeUseful(p1))
                { re(p1.CreatChild(p1.NodeKind + 1)); return; }
            }
            re(p1);
        }

    }



}