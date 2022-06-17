using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathForAsset
{
    public static string AssetsResPath( string kind)
    {
        return Application.dataPath + "\\Assets\\" + kind;
    }
}