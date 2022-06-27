using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResTool))]
public class MeshRes : MonoBehaviour,IRes,IResGetter<MeshRes>,IResData
{
    public string DirectoryName => "Assets/" + "Mesh";
    public string PackName_ = "M";
    public string PackName => PackName_;

    public string KindName_ = "n";
    public string KindName => KindName_;
    public static string kind => "Res";
    public string DataKind => kind;

    public string FullName => PackName + "'" + KindName;

    public MeshRes GetNew(ResArgs a)
    {
        return this.ExInstantiate(a);
    }
    public object GetNewObject(ResArgs a) { return GetNew(a); }
}
