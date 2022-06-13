using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Pack
{
    public class ActMix<s, TGroup, TNode> : NodeMixSelf<s, TGroup, TNode>
        where TGroup : ActMix<s, TGroup, TNode>.Act
        where TNode : ActMix<s, TGroup, TNode>.Node
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


    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public  class Act0<s> : ActMix<s, Act0<s>, Act0Node<s>>.Act
    {
        public override void InvokeOnPoint() { OnPoint.invoke(); }
        public void AddAct(Action<s> c) { Add(new Act0Node_Act<s>() { act = c }); }
    }
    public abstract class Act0Node<s> : ActMix<s, Act0<s>, Act0Node<s>>.Node
    { public abstract void invoke(); }


    public class Act0Node_Act<s> : Act0Node<s>
    {
        public Action<s> act;
        public override void invoke() { act?.Invoke(self); }
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Act1<s, T1> : ActMix<s, Act1<s, T1>, Act1Node<s, T1>>.Act
    {
        public void Invoke(T1 p1) { param1 = p1; Invoke(); }
        public T1 param1;
        public override void InvokeOnPoint() { OnPoint.invoke(param1); }
        public void AddAct(Action<s,T1> c) { Add(new Act1Node_Act<s,T1>() { act = c }); }
    }
    public abstract class Act1Node<s, T1> : ActMix<s, Act1<s, T1>, Act1Node<s, T1>>.Node
    { public abstract void invoke(T1 p1); }



    public class Act1Node_Act<s,T1> : Act1Node<s,T1>
    {
        public Action<s,T1> act;
        public override void invoke(T1 t1) { act?.Invoke(self,t1); }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Act2<s, T1, T2> : ActMix<s, Act2<s, T1, T2>, Act2Node<s, T1, T2>>.Act
    {
        public T1 param1;
        public T2 param2;
        public void Invoke(T1 p1, T2 p2)
        {
            param1 = p1;
            param2 = p2;
            Invoke();
        }
        public override void InvokeOnPoint() { OnPoint.invoke(param1, param2); }
        public void AddAct(Action<s, T1,T2> c) { Add(new Act2Node_Act<s, T1,T2>() { act = c }); }
    }
    public abstract class Act2Node<s, T1, T2> : ActMix<s, Act2<s, T1, T2>, Act2Node<s, T1, T2>>.Node
    { public abstract void invoke(T1 p1, T2 p2); }

    public class Act2Node_Act<s, T1,T2> : Act2Node<s, T1,T2>
    {
        public Action<s, T1,T2> act;
        public override void invoke(T1 t1,T2 t2) { act?.Invoke(self, t1,t2); }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Act3<s, T1, T2, T3> : ActMix<s, Act3<s, T1, T2, T3>, Act3Node<s, T1, T2, T3>>.Act
    {
        public T1 param1;
        public T2 param2;
        public T3 param3;

        public void Invoke(T1 p1, T2 p2, T3 p3)
        {
            param1 = p1;
            param2 = p2;
            param3 = p3;
            Invoke();
        }

        public override void InvokeOnPoint() { OnPoint.invoke(param1, param2, param3); }
        public void AddAct(Action<s, T1, T2,T3> c) { Add(new Act3Node_Act<s, T1, T2,T3>() { act = c }); }
    }
    public abstract class Act3Node<s, T1, T2, T3> : ActMix<s, Act3<s, T1, T2, T3>, Act3Node<s, T1, T2, T3>>.Node
    { public abstract void invoke(T1 p1, T2 p2, T3 p3); }
    public class Act3Node_Act<s, T1, T2,T3> : Act3Node<s, T1, T2,T3>
    {
        public Action<s, T1, T2,T3> act;
        public override void invoke(T1 t1, T2 t2,T3 t3) { act?.Invoke(self, t1, t2,t3); }
    }

}


//public class ActionMix<s, TGroup, TNode>
//    where TGroup : ActionMix<s, TGroup, TNode>.Act
//    where TNode : ActionMix<s, TGroup, TNode>.node
//{
//    public abstract class Act
//    {
//        public TNode First, Last, OnPoint;
//        public TGroup SetSelf(c o) { this.Self = o; return this as TGroup; }
//        public c Self;
//        public TNode Add(TNode ne)
//        {
//            ne.Func = this as TGroup;
//            if (First == null) { First = ne; Last = ne; }
//            else { Last.Next = ne; Last = ne; }
//            return ne;
//        }
//        public void Invoke()
//        {
//            OnPoint = First;
//            while (OnPoint != null)
//            {
//                InvokeOnPoint();
//                if (OnPoint == null) break;
//                OnPoint = OnPoint.Next;
//            }
//        }
//        public abstract void InvokeOnPoint();
//        public void re() { OnPoint = null; }
//        public void ToNext() { if (OnPoint != null) OnPoint = OnPoint.Next; }
//    }
//    public abstract class node
//    {
//        public c self => Func.Self;
//        public virtual TNode SetParam(string param) { return this as TNode; }
//        public TGroup Func;
//        public TNode Next;
//        public void re() { Func.re(); }
//        public void ToNext() { Func.ToNext(); }
//    }
//}
