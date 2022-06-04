using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
using System.IO;
using System;

public class SaveCameraPic : MonoBehaviour
{
    [Header("pic name")]
    public string PictureName;


    public int PossIndex;
    public List<Ro_Dis_Group> ro_dis = new List<Ro_Dis_Group>();

    //[Range(-2f, 10f)]
    public int BackGroundIndex;
    public List<GameObject> backgrounds = new List<GameObject>();
    [Header("ro")]
    public Transform ro;
    [Header("dis")]
    public Transform dis;
    public void OnUserSave()
    {
        Save(path, CreateFrom(rt));
    }
    public string Dir = "Img";
    public int which;
    public void Save(string path, Texture2D texture2D)
    {
        Debug.Log("Save Path:" + path);
        var bytes = texture2D.EncodeToPNG();
        //var bytes = texture2D.EncodeToJPG();
        System.IO.File.WriteAllBytes(path, bytes);
    }

    public Texture2D CreateFrom(RenderTexture renderTexture)
    {
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false,true);
        var previous = RenderTexture.active;
        RenderTexture.active = renderTexture;

        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

        RenderTexture.active = previous;

        texture2D.Apply();

        return texture2D;
    }

    string fullName()
    {
        return PictureName;

        throw new Exception();
    }
    public string DirPath=> (Dir + "/" + fullName()).ToStreamingAssetFullPath() + "/" ;
    public string path=> (Dir + "/" + fullName()).ToStreamingAssetFullPath() + "/" + which + ".png";
    //public Transform ResParent;
    [Header("")]
    [SerializeField]
    RenderTexture rt;

    [SerializeField]
    Camera cam;
    public void TakePhoto_SavePNG()
    {
        RenderTexture mRt = new RenderTexture(rt.width, rt.height, rt.depth, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        mRt.antiAliasing = rt.antiAliasing;

        var tex = new Texture2D(mRt.width, mRt.height, TextureFormat.ARGB32, false);
        cam.targetTexture = mRt;
        cam.Render();
        RenderTexture.active = mRt;

        tex.ReadPixels(new Rect(0, 0, mRt.width, mRt.height), 0, 0);
        tex.Apply();
        if (Directory.Exists(DirPath)==false)
        {
            Directory.CreateDirectory(DirPath);//不存在就创建文件夹
        }
        File.WriteAllBytes(path, tex.EncodeToPNG());
        Debug.Log("Saved file to: " + path);

        DestroyImmediate(tex);

        cam.targetTexture = rt;
        cam.Render();
        RenderTexture.active = rt;

        DestroyImmediate(mRt);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}




[Serializable]
public struct Ro_Dis_Group
{
    public Vector3 ro;
    public Vector3 dis;
}
