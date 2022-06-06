using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public abstract partial class Host : LayerID//Layer/SetID/Event/CreatGamePlayerForNetPlayer
    {
        public override Host host => this;
        public override Unit unit => null;
        public override Player player => null;

        public abstract int CreatGamePlayerForNetPlayer(int NetPlayerID);

        public override int ID => 0;

        public HostEvent Event = new HostEvent();

        public override void SetID(int id)
        {
            base.SetID(id);
            Event.SetMaster(ID);
            OnSetID_Awake();
        }
    }
}
