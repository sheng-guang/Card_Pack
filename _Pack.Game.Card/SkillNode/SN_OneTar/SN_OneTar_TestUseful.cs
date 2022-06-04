using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// NodeKind Reach  reversal
    /// </summary>
    public abstract class TestNodeUseful_Node : Func1Node<InputSkill, InputNode, bool> { }

    public class TestNodeUseful_IsStackSkill : TestNodeUseful_Node
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == NN.NodeKind) o.TryToIGet_ref(ref NodeKind);
            else if (key == NN.reversal) o.TryToIGet_ref(ref reversal);
        }
        IGet<int> NodeKind = OneTargetInputSkill.FirstNodeKind.ToIGet<int>();
        IGet<bool> reversal = false.ToIGet<bool>();
        bool IsStackSkill_Need => reversal.Value ? false : true;

        public override void Invoke(InputNode p1)
        {
            if (p1.NodeKind != NodeKind.Value) return;
            if (p1.LayerID.HasValue == false) { re(false); return; }
            var to= IDs<Skill>.Get(p1.LayerID.Value);
            if(to == null) { re(false); return; }
            //GameLogicFunctions.RemoveStackSkill(to);
            //Debug.Log(to);
            //IDs<IInputUser>.Get(p1.LayerID)
        }
    }
    public class TestNodeUseful_In_Range : TestNodeUseful_Node
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == NN.NodeKind) o.TryToIGet_ref(ref NodeKind);
            else if (key == NN.Reach) o.TryToIGet_ref(ref Reach);
            else if (key == NN.reversal) o.TryToIGet_ref(ref reversal);
        }

        IGet<int> NodeKind = OneTargetInputSkill.FirstNodeKind.ToIGet<int>();
        IGet<N<float>> Reach;
        IGet<bool> reversal = false.ToIGet<bool>();
        bool InRange_Need => reversal.Value ? false : true;
        public override void Invoke(InputNode p1)
        {

            if (Reach.Value.HasValue == false) return;
            if (p1.NodeKind != NodeKind.Value) return;
            bool InRange = self.NodeReachable(p1, Reach.Value.Value);
            if (InRange_Need != InRange) { re(false); return; }
        }

    }

}