using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public struct PauseSkill
{
    public int kk;
}
public struct InputDataSkill : IInputData
{
    public Skill skill;
    public N<Vector3> Point => skill.up.RealPoss;

    public N<int> LayerId => skill.ID;
}
partial class Skill : ITarget//作为 目标
{
    InputDataSkill tar = new InputDataSkill();
    public IInputData GetData()
    {
        tar.skill = this;
        return tar;
    }
}
partial class Skill : IInputUser // 网络数据包
{
    public virtual void ServerUseInput(InputMsg msg)
    {
        if (msg.Kind == Pause_Off)
        {
            ServerCancelPausing(msg.GamePlayerID);
        }
        if (msg.Kind == Pause_On)
        {
            ServerSetPausing(msg.GamePlayerID);
        }
    }
}
partial class Skill //暂停
{
    public const int Pause_Off = 10001;
    public const int Pause_On = 10002;

    PauseSkill pause = new PauseSkill();
    public void ClientCancelPause() { this.ClientSendInput(pause, Pause_Off); }
    public void ClientPause() { this.ClientSendInput(pause, Pause_On); }
    public void ServerSetPausing(int ID) { PausingIDs.Add(ID); }
    public void ServerSetPausingExcept(int ID)
    {
        foreach (var o in GameList.playerList) { if (o.ID != ID) PausingIDs.Add(o.ID); }
    }
    public void ServerSetPausingAll()
    { foreach (var o in GameList.playerList) PausingIDs.Add(o.ID); }
    public void ServerCancelPausing(int ID) { PausingIDs.Remove(ID); }
    public bool TestIsPausing(int id) => PausingIDs.Contains(id);
    HashSet<int> PausingIDs = new HashSet<int>();

    
    public void ServerSetPauseTime(float f)
    {
        PauseTimeLeft = f;
    }
    public float PauseTimeLeft { get; set; } = 0f;
    public bool IsPausing => PausingIDs.Count > 0||PauseTimeLeft>0;
}


partial class Skill //种类 名称 Res
{
    public Skill()
    {
        FullName = GetType().ToString();
    }
    public virtual string FullName { get; set; }
    public string PartDIrPath = "";
}


partial class Skill //是否可见
{
    public virtual bool Visible => true;
}

partial class Skill //一个目标的技能
{
    public static void ApplyOneTargetInput(Skill skill, InputForm p1)
    {
        skill.groupData.Ex<Vector3>(nn.Input_StartV3 + skill.GroupIndex)
            .SetIGet(skill.unit.RealPoss);
        skill.groupData.Ex<Vector3>(nn.Input_DirectionV3 + skill.GroupIndex)
             .SetIGet((p1.Child.Point.Value - skill.unit.RealPoss).normalized);
        skill.groupData.Ex<Vector3>(nn.Input_TargetV3 + skill.GroupIndex)
            .SetIGet(p1.Child.Point.Value);
        skill.groupData.Ex<N<int>>(nn.LayerID + skill.GroupIndex)
            .SetIGet(p1.Child.LayerID);
    }
    public string GroupIndex = "";
    public Vector3 StartV3()
    {
        return groupData.Ex<Vector3>(nn.Input_StartV3 + GroupIndex).Value;
    }
    public Vector3 DirV3()
    {
        return groupData.Ex<Vector3>(nn.Input_DirectionV3 + GroupIndex).Value;
    }
    public Vector3 TarV3()
    {
        var to = TarID().To<IIDTarget>();
        if (to != null) { return to.RealPoss; }
        return groupData.Ex<Vector3>(nn.Input_TargetV3 + GroupIndex).Value;
    }
    public Vector3 TarV3Visual()
    {
        var to = TarID().To<IIDTarget>();
        if (to != null) { return to.VisualPoss; }
        return groupData.Ex<Vector3>(nn.Input_TargetV3 + GroupIndex).Value;
    }
    public N<int> TarID()
    {
        return groupData.Ex<N<int>>(nn.LayerID + GroupIndex).Value;
    }
    public Unit TarUnit()
    {
        return TarID().To<Unit>();
    }
    public Skill TarSkill()
    {
        return TarID().To<Skill>();
    }
}
partial class Skill //转string ID 网络同步数据
{
    public override string ToString()
    {
        return FullName + "[" + ID + "]";
    }
    public int ID_ = IDs.NullID;
    public int ID { get => ID_; set { ID_ = value; } }
    public int upID { get => UPID.Value; set => UPID.Value = value; }
    public Param<int> UPID = Param<int>.GetNew(nameof(UPID)).Act(x => x.TrySetDefault(IDs.NullID));
    public Param<int> _groupData = Param<int>.GetNew(nameof(_groupData));
    public Param<int> _classSetting = Param<int>.GetNew(nameof(_classSetting));

    public int groupData { get => _groupData.Value; set { _groupData.Value = value; } }
    public int ClassSetting { get => _classSetting.Value; set { _classSetting.Value = value; } }
}
[Api]
public abstract partial class Skill //上级单位
{
    public Unit SelfHero()
    {
        return unit.hero;
    }
    public virtual LayerID up { get => IDs<LayerID>.Get(upID); private set { upID = value.ID; } }
    public virtual Unit unit => up.unit;
    public virtual Player player => up.player;
}
partial class Skill //创建 复制 分支 原始
{
    public Skill GetCopy() { return Layer.CreatSkill(FullName).SetUp(up).Copy(this); }
    public Skill GetBranch(string Name) { return Layer.CreatSkill(Name).SetUp(up).Branch(this); }
    public Skill GetOrigin(string Name) { return Layer.CreatSkill(Name).SetUp(up).Origin(); }
}
partial class Skill : ISpawnable//网络同步 spwan
{
    public int NetID => ID;
    public virtual string dirPass => "";
    public virtual string kindID => GetType().Name;
    public void ClientSetNetID(int NetID) { SetID(NetID); }
}
partial class Skill : IIDTarget//真实坐标  虚拟坐标
{
    public Vector3 RealPoss => up.RealPoss;

    public Vector3 VisualPoss => up.VisualPoss;
}
partial class Skill //设置ID 设置上级 初始化函数
{
    public virtual void SetUP(LayerID l)
    {
        up = l;
        if (ID != IDs.NullID && ToLoad) { ToLoad = false; OnSetID_LoadStructure(); }
    }
    bool ToLoad = true;
    public virtual void SetID(int i)
    {
        ID = i;
        GameList.AddToIDs(this, i);
        UPID.SetID(ID);
        _groupData.SetID(ID);
        _classSetting.SetID(ID);
        if (UPID.Value != IDs.NullID && ToLoad) { ToLoad = false; OnSetID_LoadStructure(); }
    }
    public virtual void OnSetID_LoadStructure() { }

}

partial class Skill //技能组件 comp
{
    HashSet<string>Comps=new HashSet<string>();

    public void EnsureComp(string FullName)
    {
        if(Comps.Contains(FullName)) return;
        Comps.Add(FullName);
        var c = Creater<SkillComp>.GetNew(FullName);
        c.SetSkill(this);
    }
    public bool HasStackComp = true;

    public void EnsureStackComp()
    {
        if (HasStackComp == false) return;
        eve.EnsureStackComp(this);
    }
    public bool HasLongSkillComp = true;
    public void EnsureLongSkillComp()
    {
        if (HasLongSkillComp == false) return;
        eve.EnsureLongSkillComp(this);

    }
}

partial class Skill //创建 技能输入 网络包 input Msg  /static
{
    public static InputMsg CreatInput(byte kind)
    {
        InputMsg to = new InputMsg();
        to.Kind = kind;
        to.Form = new InputForm();
        to.Form.SkillKind = kind;
        return to;
    }
}

partial class Skill : ISetKV //var 设置数据 set  kv
{
    public virtual void SetKV(string key, object o) { }
}

partial class Skill//卡牌堆叠 stack 
{
    public int StackInde = -1;
    public int StackTotal = 0;
    public void SetStackIndex(int index, int total)
    {
        StackInde = index;
        StackTotal = total;
    }
    public bool InStack => StackInde >= 0;
    public bool IsStackTop => InStack && (StackInde == (StackTotal - 1));
}
partial class Skill//long list  index
{
    public int LongIndex = -1;
    public int LongTotal = 0;
    public void SetLongListIndex(int index, int total)
    {
        LongIndex = index;
        LongTotal = total;
    }
    public bool InList => LongIndex >= 0;
    public bool ListTop => InList && (LongIndex == (LongTotal - 1));

}
partial class Skill//是否有超过一个的单位
{
    public bool HasMoreThanZeroTar { get; protected set; } = false;
}

