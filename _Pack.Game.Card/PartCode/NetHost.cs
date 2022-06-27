using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Host      //Host      //Host      //Host      //Host      //Host      //Host      //Host      //Host      //Host      //Host      //Host      //Host      //Host  

    [RequireComponent(typeof(ResTool))]
    public abstract partial class Host : IResGetter<Host>
    {
        //base
        public override bool IsLocal => false;
        //res
        public override string DirectoryName => "Assets/" + nameof(Host);
        //IResGetter
        public Host GetNew(ResArgs args)
        {
           return this.ExInstantiate(args);
        }
        public object GetNewObject(ResArgs a) { return GetNew(a); }

        //id
        public override void OnSetID_Awake()
        {
            base.OnSetID_Awake();
            NowTurnPlayer.SetID(ID);

        }
        public Param<int> NowTurnPlayer = Param<int>.GetNew(nameof(NowTurnPlayer));
        //public SyncMonitor<float> GameTime = SyncMonitor<float>.GetNew(nameof(GameTime));   

    }

