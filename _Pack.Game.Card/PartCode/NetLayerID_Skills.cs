using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pack
{
    //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID  

    public abstract partial class LayerID //add comp   add mesh 
    {
        //componet
        Dictionary<string, ILayerComp> Componets = new Dictionary<string, ILayerComp>();
        public ILayerComp EnsureComponet(string FullName)
        {
            if (Componets.ContainsKey(FullName)) return Componets[FullName];
            var ne = CreatComp(FullName, this);
            Componets.Add(FullName, ne);
            return ne;
        }
    }

    public abstract partial class LayerID //add mesh 
    {
        //mesh
        public MeshRes AddMesh(string FullName)
        {
            var ne = Creater<MeshRes>.GetNew(FullName, new ResArgs().SetParent(this));
            return ne;
        }
    }

    public abstract partial class LayerID:IBuffOwner //buff
    {
        //buff
        public void AddBuff(string s)
        {
            Buff b = Creater<Buff>.GetNew(s);
            AddBuff(b);
        }
        public void AddBuff(Buff b)
        {
            b.SetUp(this);
            buffs.EnsureAdd(b);
        }
        public HashSet_NodeList<BuffBase> buffs { get; private set; } = new HashSet_NodeList<BuffBase>();
        public void RemoveBuff(BuffBase b)
        {
            buffs.Remove(b);
        }


    }
    public abstract partial class LayerID //space state  change 
    {

    }

    public class SkillKind
    {
        public const int zero=0;
        public const int Input = 2;
        public const int becall = 4;
        public const int becall50 = 8;


    }
    public abstract partial class LayerID //skill
    {
        public HashSet_NodeList<Skill> skills { get; private set; } = new HashSet_NodeList<Skill>();
        //Inputs
        public List<InputSkill> InputSkills = new List<InputSkill>();
        public List<IBeCallSkill> loopSkills = new List<IBeCallSkill>();
        public List<IBeCallSkill50ms> loopSkills50ms = new List<IBeCallSkill50ms>();
        public void AddSkill_2input_4call_8call50(object to,int AddOption,byte Ex_kind=0)
        {
            if(to is Skill)
            {
                Skill s = to as Skill;
                skills.EnsureAdd(s);
                s.SetUP(this);
            }
            if (AddOption.MaskContain(2)&&to is InputSkill)
            {
                InputSkill s=to as InputSkill;
                s.SkillKind = Ex_kind;
                InputSkills.EnsureIndex_ThenSet(s.SkillKind, s);
                InputSkillVersion++;
            }
            if(AddOption.MaskContain(4)&& to is IBeCallSkill)
            {
                IBeCallSkill s = to as IBeCallSkill;
                loopSkills.Add(s);
            }
            if (AddOption.MaskContain(8) && to is IBeCallSkill50ms)
            {
                IBeCallSkill50ms s = to as IBeCallSkill50ms;
                loopSkills50ms.Add(s);
            }

        }


        //public void AddAnySkill(object to)
        //{
        //    if (to is Skill == false) return;
        //    Skill s = to as Skill;
        //    skills.EnsureAdd(s);
        //    s.SetUp(this);
        //}
        //public void AddInputSkill(object to, byte kind)
        //{
        //    if (to is InputSkill == false) return;
        //    InputSkill s = to as InputSkill;
        //    skills.EnsureAdd(s);
        //    s.SetUp(this);

        //    s.SkillKind = kind;
        //    InputSkills.EnsureIndex_ThenSet(s.SkillKind, s);
        //    InputSkillVersion++;
        //}
        //public void AddBeCallSkill(object to)
        //{
        //    if (to is IBeCallSkill == false) return;
        //    Skill s = to as Skill;
        //    skills.EnsureAdd(s);
        //    s.SetUp(this);

        //    IBeCallSkill ss = to as IBeCallSkill;
        //    loopSkills.Add(ss);
        //}
        //public void AddBeCallSkill_50(object to)
        //{
        //    if (to is IBeCallSkill50ms == false) return;
        //    Skill s = to as Skill;
        //    skills.EnsureAdd(s);
        //    s.SetUp(this);

        //    IBeCallSkill50ms ss = to as IBeCallSkill50ms;
        //    loopSkills50ms.Add(ss);
        //}

        //inputskill---------------------------------------------------------------------------------
        public int InputSkillVersion { get; private set; }
        public void GetInputSkill(Func<InputSkill, bool> sendFunc)
        {
            for (int i = 0; i < InputSkills.Count; i++)
            {
                if (sendFunc(InputSkills[i])) break;
            }
        }
        public InputSkill GetInputSkill(int which)
        {
            if ((which < InputSkills.Count == false) || (which < 0)) return null;
            return InputSkills[which];
        }
        //todo 可能 把 技能 的loop移到别的地方
        //skill loop
        public virtual void Fix()
        {
            for (int i = 0; i < loopSkills.Count; i++)
            {
                loopSkills[i].Fix();
            }
        }
        public virtual void Fix50ms()
        {
            for (int i = 0; i < loopSkills50ms.Count; i++)
            {
                loopSkills50ms[i].Fix50ms();
            }
        }
    }
    public partial class LayerID:IInputUser//input
    {
        public virtual void ServerUseInput(InputMsg m)
        {

            var to = GetInputSkill(m.Kind);
            if (to == null) { Debug.Log(this.FullName() + "  skill  [" + m.Kind + "] no found"); return; }
            to.ServerTestToRun(m.Form);
        }
    }



    public abstract partial class LayerID //
    {
        
    }
    public abstract partial class LayerID //IAfterSimulate   
    {
        public virtual IAfterSimulate AsIAfterSimulate => null;

    }
    public abstract partial class LayerID //add follow   
    {
        public virtual bool CanAddFollower(Unit u) { return true; }
        public virtual void AddUnitFollower(Unit u) { }
        public virtual void RemoveUnitFollower(Unit u) { }
    }


    public abstract partial class LayerID //UPID  OnSetID
    {
        public override int upID { get => UPID.Value; set => UPID.Value = value; }
        public Param<int> UPID = Param<int>.GetNew("UPID");
        /// <summary>
        /// SC both
        /// </summary>
        public override void OnSetID_Awake() 
        {
            base.OnSetID_Awake();
            UPID.SetID(ID); 
        
        }


    }
}
