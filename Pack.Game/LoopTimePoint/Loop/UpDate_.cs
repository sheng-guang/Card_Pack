using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IUpdata_
    {
        void Update_();
    }
    public static class UpDate_
    {
        static UpDate_() { NewGameClear.AddToNewGameClearList(NewGame_Clear); }

        static void NewGame_Clear() { collection.NewGame_Clear(); }

        public static UpdCollection<IUpdata_> collection = new UpdCollection<IUpdata_>();

        public static void Update() { collection.ForEach(x => x.Update_()); }

        public static void AddToUpDate_List(this IUpdata_ u)
        {
            collection.AddToList(u);
        }
    }
