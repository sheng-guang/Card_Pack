using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    //public static class HighLightExtra
    //{
    //    public static void TurnOnHightLight<T>(this T i) where T : IHighLightAble
    //    {
    //        HighLightSet<T>.TurnOnHightLight(i);
    //    }
    //}

    public interface IHighLightAble
    {
        void SetHighLight(bool b);
    }
    public static class HighLightSet<T> where T : IHighLightAble
    {
        static List<T> OldList = new List<T>();
        //static HashSet<ITarget> Set = new HashSet<ITarget>();

        internal static List<T> NewList = new List<T>();
        internal static HashSet<T> NewSet = new HashSet<T>();
        //---------------------------------------------------------------------------------------------------------------------------------------
        //关闭旧的进入新的循环
        public static void TurnOffOld()
        {
            foreach (var item in OldList)
            {
                if (NewSet.Contains(item) == false) item.SetHighLight(false);
            }
            var temp = OldList;
            OldList = NewList;
            NewList = temp;

            NewList.Clear();
            NewSet.Clear();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        internal static bool AllOffed = true;
        public static void TurnOffAll()
        {
            if (AllOffed) return;
            AllOffed = true;
            foreach (var item in NewList) { item.SetHighLight(false); }
            foreach (var item in OldList) { item.SetHighLight(false); }
            NewList.Clear();
            OldList.Clear();
            NewSet.Clear();
        }
        public static void TurnOnHightLight(T t)
        {
            AllOffed = false;
            if (NewSet.Contains(t)) return;
            //如果还没亮 就变成亮的
            t.SetHighLight(true);
            NewSet.Add(t); NewList.Add(t);
        }
    }



