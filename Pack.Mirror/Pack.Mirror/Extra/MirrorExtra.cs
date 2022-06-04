using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace Pack
{
    //Mirror °üÀ©Õ¹
    public static class MirrotExtra
    {
        public static void WriteNInt(this NetworkWriter w,N<int> value)
        {
            w.WriteBool(value.HasValue);
            if (value.HasValue) { w.WriteInt(value.Value); }
        }
        public static N<int> ReadNInt(this NetworkReader r)
        {
            var havesV = r.ReadBool();
            if (havesV == false) { return null; }
            return r.ReadInt();
        }

        public static void WriteNullableInt(this NetworkWriter w, int? value)
        {
            w.WriteBool(value.HasValue);
            if (value.HasValue) { w.WriteInt(value.Value); }
        }

        public static int? ReadNullableInt(this NetworkReader r)
        {
            var havesV = r.ReadBool();
            if (havesV == false) { return null; }
            return r.ReadInt();
        }
        public static void WriteNV3(this NetworkWriter w, N<Vector3> value)
        {
            w.WriteBool(value.HasValue);
            if (value.HasValue) { w.WriteVector3(value.Value); }
        }
        public static N<Vector3> ReadNV3(this NetworkReader r)
        {
            var havesV = r.ReadBool();
            if (havesV == false) { return null; }
            return r.ReadVector3();
        }
        public static void WriteNullableV3(this NetworkWriter w, Vector3? value)
        {
            w.WriteBool(value.HasValue);
            if (value.HasValue) { w.WriteVector3(value.Value); }
        }

        public static Vector3? ReadNullableV3(this NetworkReader r)
        {
            var havesV = r.ReadBool();
            if (havesV == false) { return null; }
            return r.ReadVector3();
        }

    }
}
