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


    public abstract partial class LayerID //skill
    {
        public HashSet_NodeList<Skill> skills { get; private set; } = new HashSet_NodeList<Skill>();
        //Inputs
        public List<InputSkill> InputSkills = new List<InputSkill>();
        public List<IBeCallSkill> loopSkills = new List<IBeCallSkill>();
        public List<IBeCallSkill50ms> loopSkills50ms = new List<IBeCallSkill50ms>();
        public List<ITriggerSkill> TriggerSkills = new List<ITriggerSkill>();
        public void AddSkill(object to,int ToAddMask,byte inputSkillKind=0)
        {
            if(to is Skill)
            {
                Skill s = to as Skill;
                skills.EnsureAdd(s);
                s.SetUP(this);
            }
            if (ToAddMask.MaskContain(SkillKind.Input)&&to is InputSkill)
            {
                InputSkill s=to as InputSkill;
                s.SkillKind = inputSkillKind;
                InputSkills.EnsureIndex_ThenSet(s.SkillKind, s);
                InputSkillVersion++;
            }
            if(ToAddMask.MaskContain(SkillKind.becall)&& to is IBeCallSkill)
            {
                IBeCallSkill s = to as IBeCallSkill;
                loopSkills.Add(s);
            }
            if (ToAddMask.MaskContain(SkillKind.becall50) && to is IBeCallSkill50ms)
            {
                IBeCallSkill50ms s = to as IBeCallSkill50ms;
                loopSkills50ms.Add(s);
            }
            if (ToAddMask.MaskContain(SkillKind.TriggerSkill) && to is ITriggerSkill)
            {
                ITriggerSkill s = to as ITriggerSkill;
                TriggerSkills.Add(s);
            }
        }



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
