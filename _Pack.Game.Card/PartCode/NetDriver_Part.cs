using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //driver      //driver      //driver      //driver      //driver      //driver      //driver      //driver      //driver      //driver      //driver      //driver      //driver      //driver  
    public abstract partial class Driver : NetworkManager
    {
        public static int LocalPlayerID { get; protected set; } = 0;
        public virtual void ClientCreatSpawnable(SpawnMsg m)
        {
            Debug.Log("Spawn" + m.Kind);
            var ne = Creater<ISpawnable>.GetNew(m.dir, m.Kind);
            ne.ClientSetNetID(m.NetID);
        }
        public override void Awake()
        {
            base.Awake();
            GameList.driver = this;
        }
    }

