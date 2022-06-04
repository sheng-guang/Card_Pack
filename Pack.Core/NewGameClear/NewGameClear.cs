using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack
{
    //新游戏开始时调用      运行初始化方法
    public static class NewGameClear
    {
        public static void AddToNewGameClearList( Action a)
        {
            if (acts.Contains(a)) return;
            acts.Add(a);
            act.Add(a);
        }
        static HashSet<Action> acts = new HashSet<Action>();
        static List<Action> act = new List<Action>();
        public static void Clear()
        {
            for (int i = 0; i < act.Count; i++)
            {
                act[i].Invoke();
            }
        }
        //static HashSet<Action> OnStart = new HashSet<Action>();
        //static List<Action> OnstartList = new List<Action>();
        //public static void AddToNewGameInvoke(this Action a)
        //{
        //    if (OnStart.Contains(a)) return;
        //    OnStart.Add(a);
        //    OnstartList.Add(a);
        //}

    }





}
