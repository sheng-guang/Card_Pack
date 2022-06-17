using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public partial interface ILayerComp//base
    {
        void SetMaster(int g);
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public abstract partial class LayerCompClass : ILayerComp//base
    {
        public virtual LayerID up { get; private set; }
        public Driver driver => Layer.driver;
        public virtual Host host => up.host;
        public virtual Player player => up.player;
        public virtual Unit unit => up.unit;
        public void SetMaster(int g)
        {
            up = IDs<LayerID>.Get(g);
            Awake_OnSetMaster();
        }
        public virtual void Awake_OnSetMaster() { }
    }

    public abstract partial class LayerComp : MonoBehaviour,ILayerComp//base
    {
        public virtual LayerID up { get; protected set; }
        public Driver driver => GameList.driver;
        public virtual Host host => up?up.host:null;
        public virtual Player player => up?up.player:null;
        public virtual Unit unit =>up? up.unit:null;

        public virtual void Awake()
        {
#if UNITY_EDITOR
            if (GameList.driver == null) return;
#endif
            Awake_FindMasterBySelf();
        }

        public void SetMaster(int g)
        {
            //mono类不通过set  方法设置master
            //mono类通过 Awake_FindMasterBySelf 找master
            //if (up != null) return;
            //up = IDs<LayerID>.Get(g);
            //Awake_OnSetMaster();
        }
    }
    public abstract partial class LayerComp : ILayerLinkFollower
    {
        public void Awake_FindMasterBySelf()
        {
            if (up != null) return;
            if (up == null) { up = GetComponentInParent<LayerID>(); }
            if (up == null)
            {
                var to = GetComponentInParent<ILayerLinkUp>();
                if (to.NotNull_and_NotEqualNull())
                {
                    to.Listen(this);
                }
            }
            if (up == null) return;
            Awake_OnSetMaster();
        }
        public virtual void Awake_OnSetMaster()
        {
            //Debug.Log("ID: "+up.ID);
        }
        public void SetUpLayer(LayerID layer)
        {
            if (up != null) return;
            up = layer;
            Awake_OnSetMaster();
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public abstract partial class LayerCompRes : ILayerLinkUp
    {
        public void Listen(ILayerLinkFollower Linstener)
        {
            if (up != null) { Linstener.SetUpLayer(up);return; }
            CallOnGetUpLayer += Linstener.SetUpLayer;
        }
        Action<LayerID> CallOnGetUpLayer=null;
        public override void Awake_OnSetMaster()
        {
            base.Awake_OnSetMaster();
            CallOnGetUpLayer?.Invoke(up);
        }
    }

    public abstract partial class LayerCompRes : LayerComp, IResGetter<ILayerComp>//base
    {

        public ILayerComp GetNew(ResArgs args)
        {
            return this.Ex_Instantiate(args);
        }
        public object GetNewObject(ResArgs a) { return GetNew(a); }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public abstract partial class LayerCompResUI : LayerCompRes//base
    {
        public canvas_kind kind;

        public override void Awake_OnSetMaster()
        {
            base.Awake_OnSetMaster();
            //Debug.Log(GetType()+" "+  up );
            //if (up == null) return;
            SpaceCanvas.ToCanvas_Space(transform, kind);
            //Debug.Log(transform.parent);
            //Debug.LogError("pause");
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public abstract partial class LayerCompResMesh : LayerCompRes//base
    {

        public override void Awake_OnSetMaster()
        {
            base.Awake_OnSetMaster();
            //if (up == null) return;
            //transform.MoveTransfTo(up.transf);
        }

    }


