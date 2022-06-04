using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public static class GameList
    {
        static  GameList()
        {
            NewGameClear.AddToNewGameClearList(Clear) ;
        }
        static void Clear()
        {
            //todo
        }
        public static Driver driver { get;  set; }
        public static Host NowHost { get;set; }
        public static List<Player> playerList { get; private set; }= new List<Player>();
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void AddToCallList(Unit u)
        {
            UnitCallList.Add(u);
            CallSys.AddListener(u);
        }
        public static void RemoveFromCallList(Unit u)
        {
            UnitCallList.Remove(u);
            CallSys.RemoveListener(u);
        }
        public static void ForEachCallList(Action<Unit> a)
        {
            foreach (var item in UnitCallList)
            {
                a(item);
            }
        }
        //todo 改成链表和字典结合
         static List<Unit> UnitCallList { get;  set; }=new List<Unit>();
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void AddToSpaceList(Unit u)
        {
            UnitSpaceList.Add(u);
        }
        public static void RemoveFromSpaceList(Unit u)
        {
            UnitSpaceList.Remove(u);
        }
        static List<Unit> UnitSpaceList { get;  set; }=new List<Unit>();
    }

}