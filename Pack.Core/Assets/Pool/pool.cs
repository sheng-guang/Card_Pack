using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static class PoolExtra
    {
        public static void SaveToPool_Str<T>(this T to)where T : IPoolObj_Str
        { Pool_Str<T>.Save(to); }

        public static void SaveToPool_Kind<T>(this T to) where T : IPoolObj_Int
        { Pool_Int<T>.Save(to); }

        public static void SaveToPool<T>(this T to) where T : IPoolObj
        { Pool<T>.Save(to); }

    }

    public interface IPoolObj
    {
        void ToHide();
        void ToShow();
    }
    public static class Pool<T>where T:IPoolObj
    {
        public static Stack<T> stack = new Stack<T>();
        public static void Save(T to) { to.ToHide(); stack.Push(to); }
        public static T Get(Func<T> creater = null)
        {
            if (stack.Count == 0) return creater != null ? creater() : default;
            stack.Pop().MarkAs(out var re).ToShow();
            return re;
        }
    }
    public interface IPoolObj_Int
    {
        int Kind { get; }
        void ToHide();
        void ToShow();
    }
    public static class Pool_Int<T>where T:IPoolObj_Int
    {
       static List<Stack<T>> pool = new List<Stack<T>>();
        public static void Save(T to)
        { to.ToHide(); pool.EnsureElement(to.Kind,()=>new Stack<T>()).Push(to); }
        public static T Get(int kind,Func<T>creater=null)
        {
            if (pool.EnsureElement(kind,()=>new Stack<T>()).Count == 0) return creater!=null?creater():default;
             pool[kind].Pop().MarkAs(out var re).ToShow();
            return re;
        }
    }
    public interface IPoolObj_Str
    {
        string Kind { get; }
        void ToHide();
        void ToShow();
    }
    public static class Pool_Str<T>where T: IPoolObj_Str
    {
        static Dictionary<string, Stack<T>> pool = new Dictionary<string, Stack<T>>();
      public  static void Save(T to) { to.ToHide(); pool.EnsureElement(to.Kind).Push(to); }
        public static T Get(string kind, Func<T> creater = null)
        {
            if (pool.EnsureElement(kind).Count == 0) return creater != null ? creater() : default;
            pool[kind].Pop().MarkAs(out var re).ToShow();
            return re;
        }
    }
    //public static class PoolAny<T>
    //{
    //    public static Stack<T> stack = new Stack<T>();
    //    public static void Save(T to)
    //    {
    //        stack.Push(to);
    //    }
    //    public static T Get()
    //    {
    //        if (stack.Count == 0) return default;
    //        var re = stack.Pop();
    //        return re;
    //    }
    //}
