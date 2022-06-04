using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    //Driver------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public abstract partial class Driver//base
    {
        public void NewGame(string FullName)
        {
            NewGameClear.Clear();
            GameList.NowHost = CreatHost(FullName);
        }
        public virtual Host CreatHost(string FullName)
        {
            var re = Creater<Host>.GetNew(FullName);
            re.SetID(0);
            return re;
        }
        public virtual Player CreatPlayer(string FullName)
        {
            var re = Creater<Player>.GetNew(FullName);
            re.SetID(IDs.NextID);
            return re;
        }
        public virtual Unit CreatUnit(string FullName, ResArgs args =null)
        {
            var re = Creater<Unit>.GetNew(FullName,args);
            //Debug.Log("   "+re+re.transform.position);
            re.SetID(IDs.NextID);
            return re;
        }
        public virtual ILayerComp CreatComp(string FullName, LayerID m)
        {
            var re = Creater<ILayerComp>.GetNew(FullName,new ResArgs().SetParent(m));
            re.SetMaster(m.ID);
            return re;
        }
        //skill====================================================
        public virtual SkillBuilder CreatSkill(string FullName)
        {
            return new SkillBuilder().SetName(FullName);
        }

    }

}
