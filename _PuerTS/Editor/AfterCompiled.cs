using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using Puerts.Editor;
public class AfterCompiled 
{
    [DidReloadScripts]
    public static void ReCreatPuerTs()
    {
        Debug.Log("©°©¤GenerateDTS");
        Puerts.Editor.Generator.Menu.GenerateDTS();
        Debug.Log("©¸©¤GenerateDTS");


    }

}
