using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



    //player      //player      //player      //player      //player      //player      //player      //player      //player      //player      //player      //player      //player      //player  

    public abstract partial class Player//tostring
    {

    }
    [RequireComponent(typeof(ResTool))]
    public abstract partial class Player : IResGetter<Player>
    {
        //id
        public int ID_;
        public override int ID { get => ID_; set { ID_ = value; } }
        //res
        public override string DirectoryName => "Assets/" + nameof(Player);
        public Player GetNew(ResArgs args)
        {
            return this.ExInstantiate(args);
        }
        public object GetNewObject(ResArgs a) { return GetNew(a); }
        public override void OnSetID_Awake()
        {
            base.OnSetID_Awake();
            HeroID.SetID(ID);
            WS.SetID(ID);
            DA.SetID(ID);
            CamFoward.SetID(ID);
            DeckCount.SetID(ID);
            HandCount.SetID(ID);
        }
        public override Vector3 RealPoss => hero ? hero.RealPoss : Vector3.zero;
        public override Hero hero => MainHero;
        Hero _Hero = null;
        public Hero MainHero
        {
            get { if ((_Hero == null) || (_Hero.ID != HeroID.Value)) _Hero = IDs<Unit>.Get(HeroID.Value).CanBeHero; return _Hero; }
            set { _Hero = value; HeroID.Value = value.ID; }
        }


        //values
        public Param<int> HeroID = Param<int>.GetNew(nameof(HeroID));
        public Param<int> WS = Param<int>.GetNew(nameof(WS));
        public Param<int> DA = Param<int>.GetNew(nameof(DA));
        public Param<Vector3> CamFoward = Param<Vector3>.GetNew(nameof(CamFoward));

        public Param<int> DeckCount = Param<int>.GetNew(nameof(DeckCount));
        public Param<int> HandCount = Param<int>.GetNew(nameof(HandCount));

        public Param<int> Mana = Param<int>.GetNew(nameof(Mana));
        public ParamBuffable<int> ManaMax = new ParamBuffable<int>(nameof(ManaMax));

        public ParamList<Unit> deck = new ParamList<Unit>();
        public ParamList<Unit> hand = new ParamList<Unit>();
        public ParamList<Unit> space = new ParamList<Unit>();



        public override void RemoveUnitFollower(Unit u)
        {
            base.RemoveUnitFollower(u);
            if (u.Space.Value == UnitSpace.Deck) { deck.Remove(u); DeckCount.Value = deck.Count; }
            else if (u.Space.Value == UnitSpace.Hand)
            {
                hand.Remove(u);
                HandCount.Value = hand.Count;
            }
            else if (u.Space.Value == UnitSpace.Space) space.Remove(u);
            u.HandIndex.Value = -1;
            if (u.Space.Value == UnitSpace.Hand) { for (int i = 0; i < hand.Count; i++) { hand[i].HandIndex.Value = i; } }
        }
        public override void AddUnitFollower(Unit u)
        {
            base.AddUnitFollower(u);
            if (u.Space.Value == UnitSpace.Deck) { deck.Add(u); DeckCount.Value = deck.Count; }
            else if (u.Space.Value == UnitSpace.Hand)
            {
                hand.Add(u);
                HandCount.Value = hand.Count;
            }
            else if (u.Space.Value == UnitSpace.Space) space.Add(u);

            u.HandIndex.Value = -1;
            if (u.Space.Value == UnitSpace.Hand)
            { for (int i = 0; i < hand.Count; i++) { hand[i].HandIndex.Value = i; } }

        }

    }
