using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public abstract class BuffSkill : Skill, IBuff
    {
        public int NowVersion => BuffSys.FreshVersion;
        public virtual void Apply()
        {
        }

        public virtual void FreshActive()
        {
        }

        public virtual void FreshRemove()
        {
        }
    }

