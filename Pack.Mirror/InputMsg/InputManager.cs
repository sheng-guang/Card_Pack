using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace Pack
{
    public interface IInputUser
    {
        int NetID { get; }
        void ServerUseInput(InputMsg msg);
    }
    
    public static class MsgManager
    {
        [RuntimeInitializeOnLoadMethod]
        static void Load()
        {
            Debug.Log("[Load]" + nameof(MsgManager));
            NewGameClear.AddToNewGameClearList(Clear);
        }
        public static void Clear()
        {
            NetworkServer.RegisterHandler<InputMsg>(ServerGetInput);
        }
        public static void ServerGetInput(NetworkConnection conn, InputMsg m)
        {
            m.GamePlayerID=conn.ToGameID();
            inputs.Add(m);
        }
        static List<InputMsg> inputs = new List<InputMsg>();
        public static void UseAllInput()
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                var item = inputs[i];
                var to = IDs<IInputUser>.Get(item.NetID);
                if (to == null)
                {
                    Debug.Log("use input: id  ["+item.NetID + "] no found");
                    continue;
                }
                to.ServerUseInput(item);
            }
            inputs.Clear();
        }
        public static void ClientSendInput<T>(this IInputUser o,T value, int kind) where T : unmanaged
        {
            var Msg = new InputMsg();
            Msg.NetID = o.NetID;
            Msg.Kind = kind;
            Msg.FixedLenByte = FixedLenByteMsg.GetByteMsg(value);
            NetworkClient.Send(Msg);
        }
        public static void ClientSendInput(InputForm m)
        {
            var Msg = new InputMsg();
            Msg.NetID = m.up.ID;
            Msg.Kind = m.SkillKind;
            Msg.Form = m;
            NetworkClient.Send(Msg);
        }

    }

}
