using Mirror;


    //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer  
     partial class Layer //net
    {
        public virtual bool IsLocal => Driver.LocalPlayerID == player.ID;
        public bool isServer => NetworkServer.active;

    }
