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
        //ids------------------------------------------------------------------------------------
        public static void AddToIDs(object o,int id)
        {
            TryAdd<IIDTarget>(o,id);
            TryAdd<IInputUser>(o, id);
            if (TryAdd<Skill>(o, id)) return;

            TryAdd<LayerID>(o, id);
            if (TryAdd<Unit>(o, id)) return;
            if (TryAdd<Player>(o, id)) return;
            if (TryAdd<Host>(o, id)) return;
        }
        public static bool TryAdd<T>(object o,int id)where T : class
        {
            if (o is T == false) return false;
            IDs<T>.Add((o as T), id);
            return true;
        }
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
            for (int i = 0; i < UnitCallList.Count; i++)
            {
                a(UnitCallList[i]);
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