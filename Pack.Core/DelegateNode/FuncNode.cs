using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public abstract class FuncMix<c, Tr, TGroup, TNode> : NodeMixSelf<c, TGroup, TNode>
        where TGroup : FuncMix<c, Tr, TGroup, TNode>.Func
        where TNode : FuncMix<c, Tr, TGroup, TNode>.Node
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

    public class Func0<c, Tr> : FuncMix<c, Tr, Func0<c, Tr>, Func0Node<c, Tr>>.Func
    {
        public override void InvokeOnPoint() { OnPoint.Invoke(); }
    }
    public abstract class Func0Node<c, Tr> : FuncMix<c, Tr, Func0<c, Tr>, Func0Node<c, Tr>>.Node
    {
        public abstract void Invoke();
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Func1<c, T1, Tr> : FuncMix<c, Tr, Func1<c, T1, Tr>, Func1Node<c, T1, Tr>>.Func
    {
        public T1 param1;
        public Tr Invoke(T1 p1)
        {
            param1 = p1;
            return Invoke();
        }

        public override void InvokeOnPoint() { OnPoint.Invoke(param1); }
    }
    public abstract class Func1Node<c, T1, Tr> : FuncMix<c, Tr, Func1<c, T1, Tr>, Func1Node<c, T1, Tr>>.Node
    {
        public abstract void Invoke(T1 p1);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Func2<c, T1, T2, Tr> : FuncMix<c, Tr, Func2<c, T1, T2, Tr>, Func2Node<c, T1, T2, Tr>>.Func
    {
        public T1 param1;
        public T2 param2;

        public Tr Invoke(T1 p1, T2 p2)
        {
            param1 = p1;
            param2 = p2;
            return Invoke();
        }

        public override void InvokeOnPoint() { OnPoint.Invoke(param1, param2); }
    }
    public abstract class Func2Node<c, T1, T2, Tr> : FuncMix<c, Tr, Func2<c, T1, T2, Tr>, Func2Node<c, T1, T2, Tr>>.Node
    {
        public abstract void Invoke(T1 p1, T2 p2);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Func3<c, T1, T2, T3, Tr> : FuncMix<c, Tr, Func3<c, T1, T2, T3, Tr>, Func3Node<c, T1, T2, T3, Tr>>.Func
    {
        public T1 param1;
        public T2 param2;
        public T3 param3;

        public Tr Invoke(T1 p1, T2 p2, T3 p3)
        {
            param1 = p1;
            param2 = p2;
            param3 = p3;
            return Invoke();
        }

        public override void InvokeOnPoint()
        {
            OnPoint.Invoke(param1, param2, param3);
        }
    }
    public abstract class Func3Node<c, T1, T2, T3, Tr> : FuncMix<c, Tr, Func3<c, T1, T2, T3, Tr>, Func3Node<c, T1, T2, T3, Tr>>.Node
    {
        public abstract void Invoke(T1 p1, T2 p2, T3 p3);
    }



}




//public abstract class FuncMix<c, Tr, TGroup,TNode> 
//    where TGroup : FuncMix<c, Tr, TGroup,TNode>.Func
//    where TNode :FuncMix<c,Tr,TGroup,TNode>.Node
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