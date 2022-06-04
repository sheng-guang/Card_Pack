namespace Pack
{
    //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID  

    public abstract partial class LayerID : ISpawnable//spawn
    {
        public int NetID => ID;
        //SpawnSetID
        public virtual void ClientSetNetID(int NetID) { SetID(NetID); }
        public static void CreatSpawnable(SpawnMsg m) { driver.ClientCreatSpawnable(m); }
    }
}
