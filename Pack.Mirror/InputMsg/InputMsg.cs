using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public static class InputMsgFunctions
    {
        public static void Serialize(this NetworkWriter w, InputMsg value)
        {
            w.WriteInt(value.NetID);
            w.WriteInt(value.Kind);
            if (value.FixedLenByte != null)
            {
                w.WriteInt(0);
                w.Serialize(value.FixedLenByte);
            }
            if (value.Form != null)
            {
                w.WriteInt(1);
                w.Serialize(value.Form);
            }

        }
        public static InputMsg Deserialize(this NetworkReader r)
        {
            var re = new InputMsg();
            re.NetID = r.ReadInt();
            re.Kind = r.ReadInt();
            int kind = r.ReadInt();
            if (kind == 0)
            {
                re.FixedLenByte = ByteMsgFunctions.Deserialize(r);
            }
            else if (kind == 1)
            {
                re.Form = InputFormFunctions.Deserialize(r);
            }

            return re;
        }
    }
    public struct InputMsg : NetworkMessage
    {
        public int GamePlayerID;
        public int NetID;
        public int Kind;
        public FixedLenByteMsg FixedLenByte;
        public InputForm Form;
    }

}
