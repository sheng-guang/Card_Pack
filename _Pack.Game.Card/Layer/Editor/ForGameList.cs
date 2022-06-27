using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ForGameList 
{

    [MenuItem("DebugLog/GameList ")]
    public static void LogGameList()
    {
        GameList.ForEachCallList(x => { Debug.Log(x); });
    }


}
