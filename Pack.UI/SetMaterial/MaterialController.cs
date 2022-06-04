using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public void Awake()
    {
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        //MaterialPropertyBlock  ֻ����unity�¼��д�������ᱨ��
        materialPropertyBlock = new MaterialPropertyBlock();
    }
    public MeshRenderer meshRenderer;
    public MaterialPropertyBlock materialPropertyBlock;

    public void SetColor(string name, Color c)
    {

        if(materialPropertyBlock == null) materialPropertyBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor(name, c);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
    public void SetColor_Power(string name, Color c, float power)
    {
        var to = c * power;
        if (materialPropertyBlock == null) materialPropertyBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor(name, to);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}

