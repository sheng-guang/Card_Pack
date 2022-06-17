using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ActionGroup<Tr>
    {
        public ActionGroup(Tr def) { this.def = def; }
        protected Tr def;
        protected Result<Tr> result = new Result<Tr>();

    }

    public class ActionGroup<Tr, T1> : ActionGroup<Tr>
    {
        public ActionGroup(Tr def) : base(def) { }

        Action<Result<Tr>, T1> act = null;
        public void Add(Action<Result<Tr>, T1> a) { act += a; }
        public Tr Invoke(T1 p1)
        {
            if (act == null) return def;
            result.value = def;
            act(result, p1);
            return result.value;
        }
    }

    public class ActionGroup<Tr, T1, T2> : ActionGroup<Tr>
    {
        public ActionGroup(Tr def) : base(def) { }

        Action<Result<Tr>, T1, T2> act = null;
        public void Add(Action<Result<Tr>, T1, T2> a) { act += a; }
        public Tr Invoke(T1 p1, T2 p2)
        {
            if (act == null) return def;
            result.value = def;
            act(result, p1, p2);
            return result.value;
        }
    }
    public class ActionGroup<Tr, T1, T2, T3> : ActionGroup<Tr>
    {
        public ActionGroup(Tr def) : base(def) { }

        Action<Result<Tr>, T1, T2, T3> act = null;
        public void Add(Action<Result<Tr>, T1, T2, T3> a) { act += a; }
        public Tr Invoke(T1 p1, T2 p2, T3 p3)
        {
            if (act == null) return def;
            result.value = def;
            act(result, p1, p2, p3);
            return result.value;
        }
    }

    public class Result<T>
    {
        public Result() { }
        public Result(T def) { value = def; }
        public T value;

    }
