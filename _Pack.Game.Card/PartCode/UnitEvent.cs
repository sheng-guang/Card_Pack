using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public partial class HostEvent:IUpdata_
    {

        public override void Awake_OnSetMaster()
        {
            base.Awake_OnSetMaster();
            this.AddToUpDate_List();
            HostTurnTimeLeft = host.ExParam<float>("TurnTimeLeft");
        }
        IParam<float> HostTurnTimeLeft;
        public VarChangeEvent<int> TimeLeftCall=new VarChangeEvent<int>();

        public void Update_()
        {
            if (HostTurnTimeLeft != null)TimeLeftCall.NewData((int)HostTurnTimeLeft.Value);
        }
    }













    public partial class PlayerEvent:IUpdata_
    {
        public override void Awake_OnSetMaster()
        {
            base.Awake_OnSetMaster();
            this.AddToUpDate_List();
            player.Mana.Listen(() => NowManaCall.NewData(player.Mana.Value));
           
        }

        public VarChangeEvent<int> FollowerVersionCall = new VarChangeEvent<int>();
        public VarChangeEvent<bool> IsLocalCall = new VarChangeEvent<bool>();
        public VarChangeEvent<int> NowManaCall = new VarChangeEvent<int>();
        public VarChangeEvent<int> MaxManaCall = new VarChangeEvent<int>();

        public Action<int> HandChangeCall;
        public void Update_()
        {
            IsLocalCall.NewData(player.IsLocal);
            MaxManaCall.NewData(player.ManaMax.Value_Buffed);
            //FollowerVersionCall.NewData();
        }
    }

















    public partial class UnitEvent:IUpdata_
    {
        public override void Awake_OnSetMaster()
        {
            base.Awake_OnSetMaster();
            this.AddToUpDate_List();
            unit.Space.Listen(() => SpaceCall.NewData(unit.Space.Value));
            unit.Space.Listen(() => SpaceCall_InHandCall.NewData(unit.Space.Value == UnitSpace.Hand));
            unit.State.Listen(() => StateCall.NewData(unit.State.Value));

            unit.UPID.Listen(() => UPIDCall.NewData(unit.UPID.Value));
            unit.UPID.Listen(() => UPIDCall_IsFollowingCall.NewData(unit.IsFollowing()));
            
        }
        public VarChangeEvent<int> SpaceCall = new VarChangeEvent<int>();
        public VarChangeEvent<bool> SpaceCall_InHandCall = new VarChangeEvent<bool>();
        public VarChangeEvent<int> StateCall = new VarChangeEvent<int>();

        public VarChangeEvent<int> UPIDCall = new VarChangeEvent<int>();
        public VarChangeEvent<bool> UPIDCall_IsFollowingCall = new VarChangeEvent<bool>();

        public void Update_()
        {
            HandIndexCall.NewData(unit.HandIndex.Value);
            SkillVersionCall.NewData(unit.InputSkillVersion);

            InputHighLigh.NewData  (unit.IsHighLightInput.Value);
            TargetHighLight.NewData (unit.IsHighLightTarget.Value);

            ManaCostCall.NewData(unit.ManaCost.Value_Buffed);
            
            HPCall.NewData(unit.HP.Value);
            SpeedCall.NewData(unit.speed.Value_Buffed);
            AtkCall.NewData(unit.atk.Value_Buffed);

        }


        //todo ¼ì²éÊÇ·ñÎª NullAble
        public VarChangeEvent<int> HandIndexCall = new VarChangeEvent<int>();
        public VarChangeEvent<int> SkillVersionCall = new VarChangeEvent<int>();

        public VarChangeEvent<bool> InputHighLigh = new VarChangeEvent<bool>();
        public VarChangeEvent<bool> TargetHighLight = new VarChangeEvent<bool>();

        public VarChangeEvent<int> ManaCostCall = new VarChangeEvent<int>();

        //public VarChangeEvent<NullAble<int>> ManaCostCall = new VarChangeEvent<NullAble<int>>();
        
        public VarChangeEvent<N<int>> HPCall = new VarChangeEvent<N<int>>();
        public VarChangeEvent<N<int>> SpeedCall = new VarChangeEvent<N<int>>();
        public VarChangeEvent<N<int>> AtkCall = new VarChangeEvent<N<int>>();
    }       

