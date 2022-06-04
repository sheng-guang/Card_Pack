using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public abstract partial class Player : LayerID//base
    {
        
        public override Player player => this;
        public override Unit unit => null;
        public PlayerEvent Event = new PlayerEvent();
        public override  void SetID(int id)
        {
            base.SetID(id);
            IDs<Player>.Add(this,id);
            Event.SetMaster(ID);
            OnSetID_Awake();
        }
    }

}
