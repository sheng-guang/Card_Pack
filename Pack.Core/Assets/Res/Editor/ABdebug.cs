using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public static class ABdebug 
{
    [MenuItem("AssetBundle/show loaded")]
    public static void ChangeUseAB()
    {
        var abs= AssetBundle.GetAllLoadedAssetBundles();
        int count=0;
        foreach (var item in abs)
        {
            count++;
            var names= item.GetAllAssetNames();
            foreach (var name in names)
            {
                //Debug.Log(name);
            }

            //Debug.Log("--------------------------------------------------");
        }
        Debug.Log(count+"        "+nameof( AssetBundle)+"   loaded");
    }
}
