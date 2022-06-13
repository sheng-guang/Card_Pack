using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
public class CallSys
{
    public static HashSet_NodeList<ICallListener> listeners = new HashSet_NodeList<ICallListener>();
    public static void AddListener(ICallListener listener)
    {
        listeners.EnsureAdd(listener);
    }
    public static void RemoveListener(ICallListener listener)
    {
        listeners.Remove(listener);
    }
    public static void DoCall_Destory()
    {
        DoCall(new Call(CallKind.Destory));
    }
    public static void DoCall(Call c)
    {
        listeners.ForEach(c.ApplyToListener);
        c.DoReaction();
    }
}

namespace Pack
{

    public class Call
    {
        public string Kind = "";

        public Call(string kind)
        {
            Kind = kind;
        }

        public void ApplyToListener(ICallListener l) { l.DoCall(this); }
        public void AddReaction(ICallReaction reaction)
        {
            reactions.Add(reaction);
        }
        public void AddReactionAct(Action reaction)
        {
            reactions.Add(new CallReactionAct() { act=reaction});
        }
        
        List<ICallReaction> reactions = new List<ICallReaction>();
        public void DoReaction()
        {
            for (int i = 0; i < reactions.Count; i++) { reactions[i].Do(); }
        }
    }
    public class CallReactionAct : ICallReaction
    {
        public Action act;
        public void Do()
        {
            act?.Invoke();
        }
    }
    public interface ICallListener
    {
        void DoCall(Call call);
    }
    public interface ICallReaction
    {
        void Do();
    }
}
