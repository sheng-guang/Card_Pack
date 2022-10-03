using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{

    public  class NodeMix<TGroup,TNode>
        where TGroup : NodeMix<TGroup, TNode>.groupBase
        where TNode : NodeMix<TGroup, TNode>.nodeBase
    {
        public abstract class groupBase
        {
            public TNode First, Last, OnPoint;
            public TNode AddNode(TNode ne)
            {
                if (ne.Null_Or_EqualNull()) { Debug.LogWarning("error node null");return null; }
                ne.group = this as TGroup;
                ne.OnSetSelf();
                if (First == null)
                { First = ne; Last = ne; OnPoint = First; }
                else { Last.Next = ne; Last = ne; }
                return ne;
            }
            public void Clear()
            {
                First= null;
                Last= null;
                OnPoint = null;
            }
        }
        public abstract class nodeBase
        {
            public virtual void OnSetSelf() { }
            public TNode Set(string key, object v)
            {
                Set_(key, v);

                return this as TNode;
            }

            public virtual void Set_(string key,object o) { }
            public TGroup group;
            public TNode Next;
        }
    }


    public  class NodeMixSelf<c,TGroup, TNode>:NodeMix<TGroup,TNode>
    where TGroup : NodeMixSelf<c,TGroup, TNode>.group_Self
    where TNode : NodeMixSelf<c,TGroup, TNode>.node_Self
    {
        public abstract class group_Self:groupBase
        {
            public TGroup SetSelf(c o) { self = o; return this as TGroup; }
            public c self;
        }
        public abstract class node_Self:nodeBase
        {
            public c self => group.self;
            public c GetSelf() { return self; }
        }
    }
}

//public static class StringParam
//{
//    public static string GetString(this string[]ss,int index,string def = null)
//    {
//        if (ss.HaveIndex(index) == false) return def;
//        return ss[index];
//    }


//    //------------------------------------------------------------------------------------------------------------------------------
//    public static int GetInt(this string[] ss, int index,int def=0)
//    {
//        if (ss.HaveIndex(index) == false) return def;
//        if (int.TryParse(ss[index], out var re) == false) return def;
//        return re;
//    }

//    //------------------------------------------------------------------------------------------------------------------------------
//    public static int GetInt(this string s,int def=0)
//    {
//        if (int.TryParse(s, out var re) == false) return def;
//        return re;
//    }
//    public static int? GetInt_(this string s, int? def = null)
//    {
//        if (int.TryParse(s, out var re) == false) return def;
//        return re;
//    }





//    //------------------------------------------------------------------------------------------------------------------------------
//    public static float GetFloat(this string s, float def = 0)
//    {
//        if (float.TryParse(s, out var re) == false) return def;
//        return re;
//    }

//    public static float? GetFloat_(this string s, float? def = 0)
//    {
//        if (float.TryParse(s, out var re) == false) return def;
//        return re;
//    }
//    //------------------------------------------------------------------------------------------------------------------------------
//    public static float GetFloat(this string[] ss, int index,float def=0)
//    {
//        if (ss.HaveIndex(index) == false) return def;
//        if( float.TryParse(ss[index], out var  re)==false)return def;
//        return re;
//    }
//    public static float? GetFloat_(this string[] ss, int index, float? def = null)
//    {
//        if (ss.HaveIndex(index) == false) { return def; }
//        if (float.TryParse(ss[index], out var re) == false) { return def; }
//        return re;
//    }
//}

