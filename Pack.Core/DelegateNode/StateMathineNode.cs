using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
public class StateMix<c, TGroup, TNode> : NodeMixSelf<c, TGroup, TNode>
        where TGroup : StateMix<c, TGroup, TNode>.Act
        where TNode : StateMix<c, TGroup, TNode>.Node
    {
        public abstract class Act : group_Self
        {
            public void Invoke()
            {
                OnPoint = First;
                while (OnPoint != null)
                {
                    InvokeOnPoint();
                    if (OnPoint == null) break;
                    OnPoint = OnPoint.Next;
                }
            }
            public abstract void InvokeOnPoint();
            public void re() { OnPoint = null; }
        }
        public abstract class Node : node_Self
        {
            public void re() { group.re(); }
        }
    }
