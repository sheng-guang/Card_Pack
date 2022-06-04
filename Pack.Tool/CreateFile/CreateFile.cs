using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateFile : MonoBehaviour
{
    [MenuItem("Assets/Create/.ts", false, 1)]
    private static void CreateNewAsset()
    {
        ProjectWindowUtil.CreateAssetWithContent(
            "ts.ts",
            string.Empty);
    }
}
