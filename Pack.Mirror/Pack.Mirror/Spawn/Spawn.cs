using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;


    public interface ISpawnable
    {
        int NetID { get; }
        string dirPass { get; }
        string kindID { get; }

        void ClientSetNetID(int NetID);
    }
    public static class SpawnCollection
    {

        [RuntimeInitializeOnLoadMethod]
        static void Load()
        {
            Debug.Log("[Load]" + "spawn");
            NewGameClear.AddToNewGameClearList(clear);
            ServerEvent.OnConnectionConnected += AddNewListener;
            NetworkClient.RegisterHandler<SpawnMsg>
                ((m) => { if (NetworkServer.active) return; spawn?.Invoke(m); });
        }
        static void clear()
        { 
            ToSpawn.Clear();
            playerList.Clear();
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        static List<ISpawnable> ToSpawn = new List<ISpawnable>();
        public static void OnServerSpawn(this ISpawnable spawn)
        {
            ToSpawn.EnsureIndex_ThenSet(spawn.NetID, spawn);
            for (int i = 0; i < playerList.Count; i++)
            {
                var to = playerList[i];
                to?.Send(new SpawnMsg() { Kind = spawn.kindID, NetID = spawn.NetID });
            }
        }

        static List<NetworkConnection> playerList = new List<NetworkConnection>();
        public static void AddNewListener(NetworkConnection p)
        {
           
            playerList.EnsureIndex_ThenSet(p.connectionId,p);
            for (int i = 0; i < ToSpawn.Count; i++)
            {
                var to = ToSpawn[i];
                if (to == null) continue;
                p.Send(new SpawnMsg() { NetID = to.NetID, Kind = to.kindID });
            }
        }
        //spawn---------------------------------------------------------------------------------------------------------------------------------------------------
        public static void ChangeSpawnHandel(Action<SpawnMsg> a) { spawn = a; }
        static Action<SpawnMsg> spawn;//=Layer.CreatSpawnable;

    }
    public struct SpawnMsg:NetworkMessage
    {
        public int NetID;
        public string dir;
        public string Kind;
    }
