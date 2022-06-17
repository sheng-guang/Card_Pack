using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public abstract class FuncMix<s, Tr, TGroup, TNode> : NodeMixSelf<s, TGroup, TNode>
        where TGroup : FuncMix<s, Tr, TGroup, TNode>.Func
        where TNode : FuncMix<s, Tr, TGroup, TNode>.Node
    {
        public abstract class Func : group_Self
        {
            public Tr result;
            public TGroup SetDef(Tr def) { this.def = def; return this as TGroup; }
            public Tr def;
            public Tr Invoke()
            {
                result = def;
                OnPoint = First;
                while (OnPoint != null)
                {
                    InvokeOnPoint();
                    if (OnPoint == null) break;
                    OnPoint = OnPoint.Next;
                }
                return result;
            }
            public abstract void InvokeOnPoint();
            public void re(Tr re) { result = re; OnPoint = null; }

        }
        public abstract class Node : node_Self
        {
            public void re(Tr re) { group.re(re); }
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Func0<s, Tr> : FuncMix<s, Tr, Func0<s, Tr>, Func0Node<s, Tr>>.Func
    {
        public override void InvokeOnPoint() { OnPoint.Invoke(); }
        public void AddFunc(Action<s> f) { AddNode(new Func0Node_Func<s, Tr>() { func = f }); }
    }
    public abstract class Func0Node<s, Tr> : FuncMix<s, Tr, Func0<s, Tr>, Func0Node<s, Tr>>.Node
    {
        public abstract void Invoke();
    }
    public class Func0Node_Func<s, Tr> : Func0Node<s, Tr>
    {
        public Action<s> func;
        public override void Invoke()        {            func?.Invoke(self);        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Func1<s, T1, Tr> : FuncMix<s, Tr, Func1<s, T1, Tr>, Func1Node<s, T1, Tr>>.Func
    {
        public T1 param1;
        public Tr Invoke(T1 p1) { param1 = p1; return Invoke(); }
        public override void InvokeOnPoint() { OnPoint.Invoke(param1); }
        public void AddFunc(Action<s, T1> f) { AddNode(new Func1Node_Func<s,T1, Tr>() { func = f }); }

    }
    public abstract class Func1Node<s, T1, Tr> : FuncMix<s, Tr, Func1<s, T1, Tr>, Func1Node<s, T1, Tr>>.Node
    {
        public abstract void Invoke(T1 p1);
    }
    public class Func1Node_Func<s, T1, Tr> : Func1Node<s, T1, Tr>
    {
        public Action<s,T1> func;
        public override void Invoke(T1 p1)
        {
            func?.Invoke(self, p1);
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Func2<s, T1, T2, Tr> : FuncMix<s, Tr, Func2<s, T1, T2, Tr>, Func2Node<s, T1, T2, Tr>>.Func
    {
        public T1 param1;
        public T2 param2;

        public Tr Invoke(T1 p1, T2 p2) { param1 = p1; param2 = p2; return Invoke(); }

        public override void InvokeOnPoint() { OnPoint.Invoke(param1, param2); }
        public void AddFunc(Action<s, T1, T2> f) { AddNode(new Func2Node_Func<s, T1, T2, Tr>() { func = f }); }

    }
    public abstract class Func2Node<s, T1, T2, Tr> : FuncMix<s, Tr, Func2<s, T1, T2, Tr>, Func2Node<s, T1, T2, Tr>>.Node
    {
        public abstract void Invoke(T1 p1, T2 p2);
    }
    public class Func2Node_Func<s, T1, T2, Tr> : Func2Node<s, T1, T2, Tr>
    {
        public Action<s, T1,T2> func;
        public override void Invoke(T1 p1, T2 p2)        {            func?.Invoke(self, p1, p2);        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Func3<s, T1, T2, T3, Tr> : FuncMix<s, Tr, Func3<s, T1, T2, T3, Tr>, Func3Node<s, T1, T2, T3, Tr>>.Func
    {
        public T1 param1;
        public T2 param2;
        public T3 param3;

        public Tr Invoke(T1 p1, T2 p2, T3 p3) { param1 = p1; param2 = p2; param3 = p3; return Invoke(); }

        public override void InvokeOnPoint()
        {
            OnPoint.Invoke(param1, param2, param3);
        }
        public void AddFunc(Action<s, T1, T2, T3> f) { AddNode(new Func3Node_Func<s, T1, T2, T3, Tr>() { func = f }); }

    }
    public abstract class Func3Node<s, T1, T2, T3, Tr> : FuncMix<s, Tr, Func3<s, T1, T2, T3, Tr>, Func3Node<s, T1, T2, T3, Tr>>.Node
    {
        public abstract void Invoke(T1 p1, T2 p2, T3 p3);
    }
    public class Func3Node_Func<s, T1, T2, T3, Tr> : Func3Node<s, T1, T2, T3, Tr>
    {
        public Action<s, T1, T2, T3> func;
        public override void Invoke(T1 p1, T2 p2,T3 p3) { func?.Invoke(self, p1, p2,p3); }
    }






//public abstract class FuncMix<s, Tr, TGroup,TNode> 
//    where TGroup : FuncMix<s, Tr, TGroup,TNode>.Func
//    where TNode :FuncMix<s,Tr,TGroup,TNode>.Node
//{
//    public abstract class Func
//    {
//        public Tr result;
//        public TGroup SetDef(Tr def) { this.def = def; return this as TGroup; }
//        public Tr def;

//        public TNode First, Last, OnPoint;
//        public TGroup SetSelf(c o) { Self = o; return this as TGroup; }
//        public c Self;
//        public void Add(TNode ne)
//        {
//            ne.Func = this as TGroup;
//            if (First == null) { First = ne; Last = ne; }
//            else { Last.Next = ne; Last = ne; }
//        }

//        public Tr Invoke()
//        {
//            result = def;
//            OnPoint = First;
//            while (OnPoint != null)
//            {
//                InvokeOnPoint();
//                if (OnPoint == null) break;
//                OnPoint = OnPoint.Next;
//            }
//            return result;
//        }
//        public abstract void InvokeOnPoint();
//        public void re(Tr re) { result = re; OnPoint = null; }
//        public void ToNext() { if (OnPoint != null) OnPoint = OnPoint.Next; }
//    }

//    public abstract class Node
//    {
//        public c Self => Func.Self;
//        public TGroup Func;
//        public TNode Next;
//        public void re(Tr re) { Func.re(re); }
//        public void ToNext() { Func.ToNext(); }
//    }
//}