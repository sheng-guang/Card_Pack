using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
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
    partial class Skill : ITarget//tar
    {
        InputDataSkill tar=new InputDataSkill();
        public IInputData GetData()
        {
            tar.skill = this;
            return tar;
        }
    }
    public partial class Skill : IInputUser // msg
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
    public abstract partial class Skill //Pasue
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
        public bool IsPausing => PausingIDs.Count > 0;
    }


    public abstract partial class Skill //res
    {
        public Skill()
        {
            FullName = GetType().ToString();
        }
        public virtual string FullName { get; set; }
        public string PartDIrPath = "";
    }


    public abstract partial class Skill //Visible
    {
        public virtual bool Visible => true;
    }
    partial class Skill
    {
        public Unit SelfHero()
        {
            return unit.hero;
        }
    }
    public abstract partial class Skill //One Target Input
    {
        public static void ApplyOneTargetInput(Skill skill, InputForm p1)
        {
            skill.groupData.Ex_Ptr<Vector3>(NN.Input_StartV3 + skill.GroupIndex)
                .SetIGet(skill.unit.RealPoss);
            skill.groupData.Ex_Ptr<Vector3>(NN.Input_DirectionV3 + skill.GroupIndex)
                 .SetIGet((p1.Child.Point.Value - skill.unit.RealPoss).normalized);
            skill.groupData.Ex_Ptr<Vector3>(NN.Input_TargetV3 + skill.GroupIndex)
                .SetIGet(p1.Child.Point.Value);
            skill.groupData.Ex_Ptr<N<int>>(NN.LayerID + skill.GroupIndex)
                .SetIGet(p1.Child.LayerID);
        }
        public string GroupIndex = "";
        public Vector3 StartV3()
        {
            return groupData.Ex_Ptr<Vector3>(NN.Input_StartV3 + GroupIndex).Value;
        }
        public Vector3 DirV3()
        {
            return groupData.Ex_Ptr<Vector3>(NN.Input_DirectionV3 + GroupIndex).Value;
        }
        public Vector3 TarV3()
        {
            var to = TarID().To<IIDTarget>();
            if (to != null) { return to.RealPoss; }
            return groupData.Ex_Ptr<Vector3>(NN.Input_TargetV3 + GroupIndex).Value;
        }
        public Vector3 TarV3Visual()
        {
            var to = TarID().To<IIDTarget>();
            if (to != null) { return to.VisualPoss; }
            return groupData.Ex_Ptr<Vector3>(NN.Input_TargetV3 + GroupIndex).Value;
        }
        public N<int> TarID()
        {
            return groupData.Ex_Ptr<N<int>>(NN.LayerID + GroupIndex).Value;
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
    public abstract partial class Skill //Param /ID /to string
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
        public Param<int> _classSetting => Param<int>.GetNew(nameof(_classSetting));

        public int groupData { get => _groupData.Value; set { _groupData.Value = value; } }
        public int ClassSetting { get => _classSetting.Value; set { _classSetting.Value = value; } }
    }
    public abstract partial class Skill //layer 
    {
        public virtual LayerID up { get => IDs<LayerID>.Get(upID); private set { upID = value.ID; } }
        public virtual Unit unit => up.unit;
        public virtual Player player => up.player;
    }
    public abstract partial class Skill //copy/branch/origin 
    {
        public Skill GetCopy() { return Layer.CreatSkill(FullName).SetUp(up).Copy(this); }
        public Skill GetBranch(string Name) { return Layer.CreatSkill(Name).SetUp(up).Branch(this); }
        public Skill GetOrigin(string Name) { return Layer.CreatSkill(Name).SetUp(up).Origin(); }
    }
    public abstract partial class Skill : ISpawnable//spawn/ kind / NetID
    {
        public int NetID => ID;
        public virtual string dirPass => "";
        public virtual string kindID => GetType().Name;
        public void ClientSetNetID(int NetID) { SetID(NetID); }
    }
    partial class Skill : IIDTarget
    {
        public Vector3 RealPoss => up.RealPoss;

        public Vector3 VisualPoss => up.VisualPoss;
    }
    public partial class Skill //SetID/  load structure
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
            GameList.AddToIDs(this,i);
            UPID.SetID(ID);
            _groupData.SetID(ID);
            _classSetting.SetID(ID);
            if (UPID.Value != IDs.NullID && ToLoad) { ToLoad = false; OnSetID_LoadStructure(); }
        }
        public virtual void OnSetID_LoadStructure() { }

    }

    public abstract partial class Skill //comp
    {
        public void EnsureComp()
        {
            if (Comp != null) return;
            Comp = Creater<SkillComp>.GetNew("-'" + nameof(SkillComp));
            Comp.SetSkill(this);
        }
        public SkillComp Comp { get; set; }

    }

    public abstract partial class Skill //input Msg  /static
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

    public abstract partial class Skill : ISetKV //set  kv
    {
        public virtual void setKV(string key, object o) { }
    }

    partial class Skill//stack ¿¨ÅÆ¶Ñµþ
    {
        public int StackInde = 0;
        public int StackTotal = 0;
        public void SetStackIndex(int index,int total)
        {
            StackInde = index;
            StackTotal = total;
        }
        public bool InStack => StackInde >= 0;
        public bool IsStackTop =>InStack&& (StackInde == (StackTotal - 1));
    }
    partial class Skill
    {
        public bool HasMoreThanOneTar { get; protected set; } =false;
    }

}
