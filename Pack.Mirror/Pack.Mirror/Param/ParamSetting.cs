using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public class ParamSetting
    {
        [RuntimeInitializeOnLoadMethod]
        static void OnLoadNewGame()
        {
            NewGameClear.AddToNewGameClearList(FuncOnNewGame);
        }
        static void FuncOnNewGame()
        {
#if MIRROR
            IsServer = Mirror.NetworkServer.active;
#endif
        }
        public static bool IsServer = false;
    }

}
