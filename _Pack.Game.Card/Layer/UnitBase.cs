 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{

   public abstract partial class Unit: LayerID//base
    {
        public override Unit unit => this;
        public UnitEvent Event = new UnitEvent();
        public override void SetID(int id)
        {
            base.SetID(id);
            Event.SetMaster(ID);
            //Debug.Log(this + "2   " + transform.position);
            OnSetID_Awake();
            //Debug.Log(this + "3   " + transform.position);
        }
    }

}
