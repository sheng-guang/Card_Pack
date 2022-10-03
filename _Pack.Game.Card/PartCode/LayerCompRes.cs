using UnityEngine;


////LayerComp      //LayerComp      //LayerComp      //LayerComp      //LayerComp      //LayerComp      //LayerComp      //LayerComp      //LayerComp      //LayerComp      
[RequireComponent(typeof(ResTool))]
public abstract partial class LayerCompRes : IRes
{
    public virtual string DirectoryName => "Assets/" + nameof(ILayerComp);
    public virtual string PackName => "-";
    public virtual string KindName => GetType().ToString();

    public bool HasPipLineTag => true;
}
