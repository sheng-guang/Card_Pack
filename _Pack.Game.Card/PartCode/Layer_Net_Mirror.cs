using Mirror;

namespace Pack
{
    //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer  
    public abstract partial class Layer //net
    {
        public virtual bool IsLocal => Driver.LocalPlayerID == player.ID;
        public bool isServer => NetworkServer.active;

    }
}
