using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public interface IAfterSimulate
    {
        void AfterSimulate_();
    }
    public class AfterSimulate
    {
        static AfterSimulate() { NewGameClear.AddToNewGameClearList(Clear); }
        static void Clear() { collection.NewGame_Clear(); }
        public static UpdCollection<IAfterSimulate> collection = new UpdCollection<IAfterSimulate>();
        public static void Update() { collection.ForEach(x => x.AfterSimulate_()); }
    }
}