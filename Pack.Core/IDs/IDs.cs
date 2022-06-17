using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    //存储带有唯一id的实例
    public static class IDs<T>
    {
        static IDs()
        {
            //新游戏开始时清空记录
            NewGameClear.AddToNewGameClearList(clear);
        }
        public static void clear()
        {
            Values.Clear();
        }


        static List<T> Values = new List<T>();
        //添加新的记录
        public static void Add(T v, int id)
        {
            Values.EnsureIndex_ThenSet(id, v);
        }
        //获取记录的单位
        public static T Get(int id)
        {
            return Values.GetElement(id);
        }


    }
    //用于产生唯一ID
    public static class IDs
    {
        public const int NullID = -1;
        static IDs()
        {      
            //新游戏开始时ID归零
            NewGameClear.AddToNewGameClearList(clear);
        }
        public static void clear()
        {
            lastID = 0;
        }

        static int lastID = 0;
        public static int NextID => ++lastID;
    }

