using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Digicrafts.WireframePro;

[ExecuteInEditMode]
public class MeshUpdater : MonoBehaviour {

// #if UNITY_EDITOR
	// Use this for initialization
	void Start () {

        MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter && meshFilter.sharedMesh)
            {
                WireframeShaderUtils.updateMesh(meshFilter.sharedMesh);
            }
        }

        SkinnedMeshRenderer[] skinMeshs = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer skinMesh in skinMeshs)
        {
            if (skinMesh && skinMesh.sharedMesh)
            {
                WireframeShaderUtils.updateMesh(skinMesh.sharedMesh);
            }
        }

    }
// #endif
	
}
