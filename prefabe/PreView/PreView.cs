using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public interface IPreViewMaster
{

    int Count { get; }
    IPreViewMasterNode GetNode(int index);
}
public interface IPreViewMasterNode
{
    IPreViewMasterNode UPViewNode { get; }
    public void ForEachNode(Action<string> act);
    public N<int> PreInt(string DataName);
    public N<Vector3> PreV3(string DataName);
    public N<float> PreFloat(string DataName);
    public N<bool> PreBool(string DataName);


}

public interface IPreView
{
    void SetNewMaster(IPreViewMaster orig);
    void Fresh();
    void Clear();
}
public interface IPreViewNode : IPoolObj_Str
{
    IPreViewMasterNode Master { get; set; }
    void Fresh();
}
class PreViewNodeGroup : IPreView
{
    //public LinkedList<IPreViewNode> nodes = new LinkedList<IPreViewNode>();
    IPreViewMaster master;
    public void SetNewMaster(IPreViewMaster master) { Clear(); this.master = master; }
    //HashSet<IPreViewMasterNode> Created = new HashSet<IPreViewMasterNode>();
    int ToCreatIndex = 0;
    List<IPreViewNode> Nodes = new List<IPreViewNode>();
    public void Fresh()
    {
        if (master == null) return;
        //0<1
        while (ToCreatIndex < master.Count) { ToCreatIndex++; Ensure(master.GetNode(ToCreatIndex - 1)); }
        //Debug.Log("fresh node count  "+Nodes.Count);
        for (int i = 0; i < Nodes.Count; i++) { Nodes[i].Fresh(); }
    }
    public void Ensure(IPreViewMasterNode node)
    {
        node.ForEachNode((s) =>
        {
            var ne = GetNewNode(s);
            ne.Master = node;
            Nodes.Add(ne);
        });
    }

    public void Clear()
    {
        master = null;
        ToCreatIndex = 0;
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].SaveToPool_Str();
        }
        Nodes.Clear();
    }
    public static IPreViewNode GetNewNode(string kind)
    {
        var re = Pool_Str<IPreViewNode>.Get(kind) ?? Creater<IPreViewNode>.GetNew(kind);
        return re;
    }

}
public class PreView
{
    public static IPreView GetPreView() { return new PreViewNodeGroup(); }
}

[RequireComponent(typeof(ResTool))]
public abstract class PreViewNodeMono : MonoBehaviour, IPreViewNode, IResCreater<IPreViewNode>, IRes
{
    public virtual void Awake() { DontDestroyOnLoad(gameObject); }


    public IPreViewNode GetNew(ResArgs args) { return this.ExInstantiate(args).GetComponent<IPreViewNode>(); }
    public object GetNewObject(ResArgs a) { return GetNew(a); }

    public string Kind => this.FullName();
    public IPreViewMasterNode Master { get; set; }
    public int DataIndex { get; set; }

    public string DirectoryName => "Pack/prefabe/PreView";

    public string PackName => "Pre";

    public abstract string KindName { get; }

    public abstract void Fresh();
    public void ToHide() { gameObject.SetActive(false); }
    public void ToShow() { gameObject.SetActive(true); }
}
public class PreViewNode : IPreViewNode
{
    public virtual string Kind => "";

    public IPreViewMasterNode Master { get; set; }
    public int DataIndex { get; set; }

    public virtual void Fresh() { }
    public virtual void ToHide() { }
    public virtual void ToShow() { }
}

