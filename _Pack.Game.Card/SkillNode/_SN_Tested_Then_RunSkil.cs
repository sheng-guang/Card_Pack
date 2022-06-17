using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    //1   get new skill
    public class SN_GetCopySkill : Act1Node<InputSkill, InputForm>, IGet<Skill>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if(key==nn.result)result=o.ToISet<Skill>();
        }
        ISet<Skill> result;
        public Skill Value { get; private set; }
        public override void invoke(InputForm p1)
        {
            var ne = self.GetCopy();
            if(result.NotNull_and_NotEqualNull())result.Value = ne;
            Value = ne;
        }
    }
    public class SN_GetBranchSkill : Act1Node<InputSkill, InputForm>, IGet<Skill>
    {
        public Skill Value { get; private set; }
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == nn.SkillName) o.TryToIGet_ref(ref skillname);
        }
        IGet<string> skillname;
        public override void invoke(InputForm p1)
        {
            if (skillname.IsNull_or_EqualNull()) return;
            var ne = self.GetBranch(skillname.Value);
            //Debug.Log("new branch: "+ne);
            Value= ne;
        }
    }
    //2   apply input
    public class SN_ApplyOneTar_To_Skill : Act1Node<InputSkill, InputForm>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == nn.Skill) skill = o.ToIGet<Skill>();
        }
        IGet<Skill> skill = (null as object).ToIGet<Skill>();
        public override void invoke(InputForm p1)
        {
            var to = skill.Value;
            if(to == null) return;
            Skill.ApplyOneTargetInput(to, p1);
            //Debug.Log("apply input to" + to);
        }
    }
    //3   move skill
    public class SN_MoveSkillToList : Act1Node<InputSkill, InputForm>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if(key==nn.Skill) skill = o.ToIGet<Skill>();
            if(key==nn.SkillListKind_1Stack_2Long)SkillList=o.ToIGet<int>();
        }
        IGet<Skill> skill = (null as object).ToIGet<Skill>();
        IGet<int> SkillList = 0.ToIGet<int>();

        public override void invoke(InputForm p1)
        {
            var s = skill.Value;
            var to = SkillList.Value;
            //Debug.Log("move skill " + s + " to " + to);
            if (to.MaskContain(1)&&s is IStackSkill)
            {
                //Debug.Log("move skill " + s + " to stack" + to);
                (s as IStackSkill).AddToStackSkillList();
            }
            if(to.MaskContain(2)&&s is ILongSkill)
            {
                //Debug.Log("move skill " + s + " to long" + to);
                (s as ILongSkill).AddToLongSkillList();
            }
        }
    }
    public class SN_MoveSkillToLongList : Act1Node<InputSkill, InputForm>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if(key==nn.Skill)skill=o.ToIGet<Skill>();
        }
       IGet<Skill> skill=(null as object).ToIGet<Skill>();
        public override void invoke(InputForm p1)
        {
            Skill s = skill.Value;
            if (s is ILongSkill==false) return;
            (s as ILongSkill).AddToLongSkillList();

        }
    }
    public class SN_MoveSkillToStackList:Act1Node<InputSkill, InputForm>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == nn.Skill) skill = o.ToIGet<Skill>();
        }
        IGet<Skill> skill = (null as object).ToIGet<Skill>();
        public override void invoke(InputForm p1)
        {
            Skill s = skill.Value;
            if (s is IStackSkill == false) return;
            (s as IStackSkill).AddToStackSkillList();

        }
    }
    //mix---------------------------------------------------------------------------------------------------------------------------------------------
    //这里 主要是 验证输入后的操作
    public class SN_CopyToSkillList : Act1Node<InputSkill, InputForm>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
             if (key == nn.SkillKind_1Stack_2Long) o.TryToIGet_ref(ref ListKind);
            else if (key == nn.Action) o.TryToIGet_ref(ref TransferAction);
        }
        IGet<int> ListKind=0.ToIGet<int>();
        IGet<Action<Skill, InputForm, Skill>> TransferAction;


        public override void invoke(InputForm p1)
        {
            var ne = self.GetCopy();

            if (ne.IsNull_or_EqualNull()) { return; }
            if (TransferAction.NotNull_and_NotEqualNull() && TransferAction.Value != null)
            {
                TransferAction.Value.Invoke(self,p1, ne);
            }

            if (ListKind.Value.MaskContain(1) && ne is IStackSkill)
            {
                var s = ne as IStackSkill;
                s.AddToStackSkillList();
            }
            if (ListKind.Value.MaskContain(2) && ne is ILongSkill)
            {
                var s = ne as ILongSkill;
                s.AddToLongSkillList();
            }
        }
    }

    public class SN_StartNewSkill : Act1Node<InputSkill, InputForm>
    {
        public override void Set_(string key, object o)
        {
            base.Set_(key, o);
            if (key == nn.SkillName) o.TryToIGet_ref(ref SkillName);
            else if (key == nn.SkillKind_1Stack_2Long) o.TryToIGet_ref(ref ListKind);
            else if (key == nn.Action) o.TryToIGet_ref(ref TransferAction);
        }
        IGet<int> ListKind;
        IGet<string> SkillName;
        IGet<Action<Skill,InputForm, Skill>> TransferAction;
        //string SkillName;
        public override void invoke(InputForm p1)
        {
            var ne = self.GetBranch(SkillName.Value);
            if (ne.IsNull_or_EqualNull()) { return; }
            if (TransferAction.NotNull_and_NotEqualNull()&&TransferAction.Value!=null)
            {
                TransferAction.Value.Invoke(self,p1, ne);
            }

            if (ListKind.Value.MaskContain(1)&&ne is IStackSkill)
            {
                var s = ne as IStackSkill;
                s.AddToStackSkillList();
            }
            if (ListKind.Value.MaskContain(2)&& ne is ILongSkill)
            {
                var s = ne as ILongSkill;
                s.AddToLongSkillList();
            }

        }
    }

