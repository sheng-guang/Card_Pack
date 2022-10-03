using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(BlockLayout))]

public class ForBlockLayout : Editor<BlockLayout>
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("create")) tar.Create();
        if (GUILayout.Button("clear")) tar.ClearAll();
    }
}
