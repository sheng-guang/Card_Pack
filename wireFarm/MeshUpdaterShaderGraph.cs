using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Digicrafts.WireframePro;

[ExecuteInEditMode]
public class MeshUpdaterShaderGraph : MonoBehaviour {

// #if UNITY_EDITOR
	// Use this for initialization
	void Awake () {
			
		MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter && meshFilter.sharedMesh)
            {
                WireframeShaderUtils.updateMeshVertexColor(meshFilter.sharedMesh);
            }
        }

        SkinnedMeshRenderer[] skinMeshs = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer skinMesh in skinMeshs)
        {
            if (skinMesh && skinMesh.sharedMesh)
            {
                WireframeShaderUtils.updateMeshVertexColor(skinMesh.sharedMesh);
            }
        }


	}
// #endif
	
}
