using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using Puerts.Editor;
using UnityEditor;
public class AfterCompiled 
{
    [DidReloadScripts]
    public static void ReCreatPuerTs()
    {
        //Debug.Log(EditorApplication.isPlayingOrWillChangePlaymode);
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;
        Debug.Log("┌─GenerateDTS");
        Puerts.Editor.Generator.UnityMenu.GenerateDTS();
        GenFullNames.Gen();
        Debug.Log("└─GenerateDTS");
        

    }

}
