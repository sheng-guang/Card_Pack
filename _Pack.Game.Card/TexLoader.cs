using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexLoader:MonoBehaviour
{
    public MeshRenderer ImgMesh;
    public float ImageW_H()
    {
        var scale = ImgMesh.transform.lossyScale;
        return scale.x / scale.y;
    }
    public string ParamName="_MainTex";
    public int which=0;
    public void LoadTexture(string fullname)
    {
        ImgMesh.LoadTexture(ParamName, ImageW_H(), fullname, which);

    }

}

