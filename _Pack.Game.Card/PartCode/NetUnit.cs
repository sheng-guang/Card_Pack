using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //unit      //unit      //unit      //unit      //unit      //unit      //unit      //unit      //unit      //unit      //unit      //unit      //unit      //unit  

    partial class Unit : ICallListener //触发 call 
    {
        public virtual void DoCall(Call call) 
        {
            if(call.Kind== CallKind.Destory) OnGetDestoryCall(call);
            for (int i = 0; i < TriggerSkills.Count; i++)
            {
                var to = TriggerSkills[i];
                to.OnCall(call);
            }
        }
        public virtual void OnGetDestoryCall(Call c)
        {
            bool ToDes=false;
            if (MaxHP.Value_Base.HasValue&&HP.Value.HasValue)
            {
                if(HP.Value<=0)ToDes=true;
            }
            if (ToDestory.Value_Buffed) 
            {
                ToDes=true;
            }
            if (ToDes)
            {
                c.AddReaction(new DesReaction() { tar = this }) ;
            }
        }
        public class DesReaction : ICallReaction
        {
            public Unit tar;
            public void Do()
            {
                eve.SetState(tar,UnitState.Card);
            }
        }
    }

    partial class Unit //初始化函数  awake functions 
    {
        public override void OnSetID_Awake()
        {
            base.OnSetID_Awake();
            LoadUnitData();

            //Debug.Log(this + "2.0   " + transform.position);

            OnSetID_Awake_Param();
            //Debug.Log(this + "2.1   " + transform.position);

            EnsureComponet("-'" + nameof(ComUnit3DUI));
            //Debug.Log(this + "2.2   " + transform.position);
            //这里cardmesh会将卡牌位置归零
            EnsureComponet("-'" + nameof(CardMesh));
            //Debug.Log(this + "2.3   " + transform.position);

        }
        public override void Awake()
        {
            base.Awake();
            if(rig==null) rig = GetComponent<Rigidbody>();
        }
    }
    partial class Unit//配置的数据 unit Data
    {
        public virtual bool CreatUnitData => true;
        UnitData _BaseData = null;
        public UnitData BaseData
        {
            get
            {
                if (_BaseData == null) _BaseData = UnitData.GetData(this.FullName());
                return _BaseData;
            }
        }
        public void LoadUnitData()
        {
            Debug.Log(this.FullName());
            ManaCost.Value_Base = BaseData.Mana;
            atk.Value_Base = BaseData.atk;
            speed.Value_Base = BaseData.speed;
            MaxHP.Value_Base = BaseData.MaxHp;
            HP.Value = BaseData.MaxHp;
        }
    }
    partial class Unit //网络同步属性 param 
    {
        public float UIHeight => BaseData.UIHeight;
        public float UIWide => BaseData.WideR;

        public SyncMonitor<Vector3> PossMonitor = SyncMonitor<Vector3>.GetNew(nameof(PossMonitor));
        public SyncMonitor<Vector3> EulaAngleMonitor = SyncMonitor<Vector3>.GetNew(nameof(EulaAngleMonitor));
        public SyncMonitor<Vector3> RigVMonitor = SyncMonitor<Vector3>.GetNew(nameof(RigVMonitor));

        public Param<int> Space =  Param<int>.GetNew(nameof(Space));
        public Param<int> State =  Param<int>.GetNew(nameof(State));
        public Param<int> HandIndex =  Param<int>.GetNew(nameof(HandIndex));

       
        public ParamBuffable<N<int>> ManaCost = new ParamBuffable<N<int>>(nameof(ManaCost));
        
        public ParamBuffable<N<int>> atk = new ParamBuffable<N<int>>(nameof(atk));
        public ParamBuffable<N<int>> speed = new ParamBuffable<N<int>>(nameof(speed));
        public ParamBuffable<N<float>> MaxHP = new ParamBuffable<N<float>>(nameof(MaxHP));
        public Param<N<int>> HP =  Param<N<int>>.GetNew(nameof(HP));

        public ParamBuffable<bool>ToDestory=new ParamBuffable<bool>(nameof(ToDestory));
        public void OnSetID_Awake_Param()
        {
            PossMonitor.SetID(ID).SetGetSetFunction(() => transform.position, (v) => transform.position = v);
            EulaAngleMonitor.SetID(ID).SetGetSetFunction(() => transform.eulerAngles, (v) => transform.eulerAngles = v);
            RigVMonitor.SetID(ID).SetGetSetFunction(() => rig.velocity, (v) => rig.velocity = v);

            Space.SetID(ID);
            State.SetID(ID);
            HandIndex.SetID(ID);

            ManaCost.SetID(ID);

            atk.SetID(ID);
            speed.SetID(ID);
            HP.SetID(ID);
            MaxHP.SetID(ID);

            ToDestory.SetID(ID);
        }

        public bool SpaceIsInHand => Space.Value == UnitSpace.Hand;
        public bool OnLand = false;

    }
    
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ResTool))]
    [RequireComponent(typeof(ComRig))]
    partial class Unit // 实现layer相关属性和函数  layer
    {
        public override Unit TopUnit { get { if (up.unit == null) { return this; } else return up.TopUnit; } }
        //hero
        public virtual Hero CanBeHero => null;
        public virtual bool IsHero => false;
        //id
        public int ID_;
        public override int ID { get => ID_; set { ID_ = value; } }
        public Rigidbody rig;
    }

    partial class Unit : IResGetter<Unit>  //资源创建 res
    {
        public override string DirectoryName => "Assets/" + nameof(Unit);
        public Unit GetNew(ResArgs args)
        {
            //Debug.Log(name + "  arg poss" + args.GetPoss());
            var re = this.Ex_Instantiate(args);
            //Debug.Log(re+"  result  poss   " + re.transform.position);
            return re;
        }
        public object GetNewObject(ResArgs a) { return GetNew(a); }
    }

    partial class Unit : IAfterSimulate//物理刷新   是否在地上  onland
    {
        public virtual void AfterSimulate_()
        {
            if (unit.IsFollowing() || unit.Space.Value != UnitSpace.Space) { OnLand = false; return; }
            if (Physics.Raycast(transform.position + statrtPoint, Vector3.down, len + 0.05f) == false) OnLand = false;
            else OnLand = true;
        }
        public override IAfterSimulate AsIAfterSimulate => this;
        static float len = 0.1f;
        static Vector3 statrtPoint = new Vector3(0, len, 0);
    }


    partial class Unit : ITarget   //作为 输入目标
    {
        //target
        public IInputData GetData()
        {
            d.SetData(this);
            return d;
        }
        static InputDataUnit d = new InputDataUnit();
    }



    partial class Unit //空间坐标  realposs poss 
    {
        public void _SetPoss(Vector3 v) { transform.position = v; }
        public override Vector3 RealPoss
        {
            get
            {
                //print(Space);
                if (Space.Value == UnitSpace.Space)
                {
                    if (this.IsFollowing()) { return TopUnit.RealPoss; }
                    else return transform.position;
                }
                else
                {
                    return up ? up.RealPoss : Vector3.zero;
                }
            }
        }
    }



    partial class Unit //添加或移除 附着卡牌 follower 
    {
        public override void AddUnitFollower(Unit u)
        {
            base.AddUnitFollower(u);
            if (Follower != null) return;
            u.MoveTransfTo(FollowerTransform);
            Follower = u;
        }
        public override bool CanAddFollower(Unit u)
        {
            if (Follower != null) return false;
            return true;
        }
        public Unit Follower;
        public Transform _FollowerTransform;
        public virtual Transform FollowerTransform
        {
            get
            {
                if (_FollowerTransform == null)
                {
                    _FollowerTransform = new GameObject("followerPoss").transform;
                    _FollowerTransform.MoveTransfTo(transform);
                    _FollowerTransform.localPosition = Vector3.up;
                }
                return _FollowerTransform;

            }
        }
        public override void RemoveUnitFollower(Unit u)
        {
            base.RemoveUnitFollower(u);
            if (Follower != u) return;
            u.MoveTransfTo(null);
            Follower = null;
        }

    }

    //---------------------------------------------------------------------------------------------------------------------------
    public class UnitSpace
    {
        public enum en : int
        {
            Nul = UnitSpace.Nul,
            Deck = UnitSpace.Deck,
            Hand = UnitSpace.Hand,
            Space = UnitSpace.Space,

        }
        public const int Nul = 1;
        public const int Deck = 2;
        public const int Hand = 4;
        public const int Space = 8;
    }

    public class UnitState
    {
        public enum en : int
        {
            Card = UnitState.Card,
            Space = UnitState.Space,
        }
        public const int Card = 1;
        public const int Space = 2;
    }



    //---------------------------------------------------------------------------------------------------------------------------
    public abstract class Hero : Unit//英雄单位
    {
        //private void Awake()
        //{
        //    IRealPoss r= this;
        //}
        public override Hero CanBeHero => this;
        public override bool IsHero => player.hero == this;
    }
