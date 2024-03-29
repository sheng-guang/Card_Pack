﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract partial class LayerID//ToString
{
    public override string ToString()
    {
        return "(" + this.FullName() + ")[" + ID + "]";
    }
}
//layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID  
public abstract partial class LayerID : IRes, IResCreater<ISpawnable>//res
{

    //res
    public virtual string DirectoryName => "Assets/" + nameof(LayerID);
    public virtual string PackName => "-";
    public virtual string KindName => GetType().ToString();
    //spawn
    public string kindID => this.FullName();
    public string dirPass => DirectoryName;


    //IResGetter
    ISpawnable IResCreater<ISpawnable>.GetNew(ResArgs args)
    {
        Debug.Log("here");
        return this.ExInstantiate(args);
    }
    object IResGetter.GetNewObject(ResArgs args)
    {
        return this.ExInstantiate(args);
    }
}
public abstract partial class LayerID : IRealPoss//poss
{
    //poss
    public virtual Vector3 RealPoss => Vector3.zero;
    public virtual Vector3 VisualPoss => RealPoss + Vector3.up * 0.5f;
}
