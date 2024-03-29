using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public virtual void SetStartGameTime(float t)
    {

    }
}
[RequireComponent(typeof(ResTool))]
public class VFXRes : VFX, IResCreater<VFX>, IRes
{
    public virtual string DirectoryName => "Assets/" + "VFX";

    public virtual string PackName => "E";

    public virtual string KindName => GetType().ToString();


    public virtual VFX GetNew(ResArgs args)
    {
        return this.ExInstantiate(args);
    }
    public object GetNewObject(ResArgs a) { return GetNew(a); }

}

