using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GraphSetting
{    public const int Def = 1;
    public const string StrDef = nameof(Def);
    public const int HDRP = 2;
    public const string StrHDRP = nameof(HDRP);
     static GraphSetting()
    {
        var t = QualitySettings.renderPipeline;
        if (t == null)
        {
            PipLineKind = Def;
            PipLineName = nameof(Def);
            return;
        }
        Debug.Log(t.GetType().Name);
        PipLineName = nameof(HDRP);
        PipLineKind = HDRP;
    }
    public static int PipLineKind { get; private set; } = 0;
    public static string PipLineName { get; private set; } = "";
    public static string PipLineTag => "'" + PipLineName;
}
public class graphSetting : MonoBehaviour
{
    //GraphSetting()
    //{
    //    var t = QualitySettings.renderPipeline;
    //    if (t == null) { PipLineKind = RenderPipLineKind.Def; return; }
    //    Debug.Log(t.GetType().Name);
    //    PipLineKind = RenderPipLineKind.HDRP;
    //}
    //public static int PipLineKind { get; private set; } = 0;
    public int targetFrameRate = 30;
    private void Start()
    {
        

        Fresh();
    }
    [ContextMenu("fresh")]
    public void Fresh()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
