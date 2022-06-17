#if MIRROR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

    public static class Connections
    {
        [RuntimeInitializeOnLoadMethod]
        static void Load()
        {
            Debug.Log("[Load]" + "connections");
            NewGameClear.AddToNewGameClearList(clear);
            NetworkClient.RegisterHandler<GamePlayerIDMsg>(ClientGetPlayerID);
        }
  
        public static void clear()
        {
            LocalPlayerID = -1;
            PassedDic.Clear();
            List_NetPlayerIndex.Clear();
            List_ConnIndex.Clear();
            List_GameIndex.Clear();
        }
        public static int LocalPlayerID { get; set; }
        public static Dictionary<string, NetPlayer> PassedDic = new Dictionary<string, NetPlayer>();
        public static List<NetPlayer> List_NetPlayerIndex = new List<NetPlayer>();
        public static List<NetPlayer> List_ConnIndex = new List<NetPlayer>();
        public static List<NetPlayer> List_GameIndex = new List<NetPlayer>();

        public static int ToGameID(this NetworkConnection c)
        {
            if (List_ConnIndex.Count > c.connectionId == false) return default;
            if(List_ConnIndex[c.connectionId] == null) return default;
            return List_ConnIndex[c.connectionId].GamePlayerID.Value;
        }
        public static void ClientGetPlayerID(GamePlayerIDMsg m)
        {
            LocalPlayerID = m.ID;
        }
        public static NetPlayer OnServerGetPass( NetworkConnection c, string pass)
        {
            NetPlayer re = null;
            if (PassedDic.TryGetValue(pass, out re))
            {
                re.conn.Disconnect();
                re.conn = c;
                Debug.Log("ReConn[" + c.connectionId + "]  ip:" + c.address + "-->index  " + re.NetPlayerID);

                //re = PassList[pass];
            }
            else
            {
                re = new NetPlayer() { conn = c, pass = pass };
                PassedDic[pass] = re;
                List_NetPlayerIndex.Add(re);
                re.NetPlayerID = List_NetPlayerIndex.IndexOf(re);
                Debug.Log("NewConn[" + c.connectionId + "]  ip:"
                    + c.address + "-->index  " + re.NetPlayerID + "    pass" + pass);

            }
            List_ConnIndex.EnsureIndex_ThenSet(c.connectionId, re);
            return re;
        }
        public static void OnSererSetGamePlayerID(this NetPlayer p, int id)
        {
            p.GamePlayerID = id;
            List_GameIndex.EnsureIndex_ThenSet(id, p);
        }

    }

#endif