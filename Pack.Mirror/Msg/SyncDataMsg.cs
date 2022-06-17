using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public struct TPair<T>
    {
        public int i;
        public T v;
    }
    public static class SyncController
    {
        static class Temp<T> where T : IEquatable<T>
        {
            static List<TPair<T>> Values = new List<TPair<T>>();
            public static void Add(int index, T value)
            {
                Values.Add(new TPair<T>() { i = index, v = value });
            }
            public static TPair<T>[] GetData()
            {
                NetDataCollection<T>.Send(Add);
                var re = Values.ToArray();
                Values.Clear();
                return re;
            }
            public static void Apply(TPair<T>[] list)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    NetDataCollection<T>.ClientSetData(list[i].i, list[i].v);
                }
            }
        }
        public static SyncDataMsg GetAllChangedData()
        {
            var re = new SyncDataMsg();
            re.Ints = Temp<int>.GetData();
            re.Floats = Temp<float>.GetData();
            re.Bools = Temp<bool>.GetData();
            re.V3s = Temp<Vector3>.GetData();

            return re;
        }

        public static void ApplySyncData(SyncDataMsg msg)
        {
            Temp<int>.Apply(msg.Ints);
            Temp<float>.Apply(msg.Floats);
            Temp<bool>.Apply(msg.Bools);
            Temp<Vector3>.Apply(msg.V3s);
        }

    }
    public partial struct SyncDataMsg : NetworkMessage
    {

        public TPair<int>[] Ints;
        public TPair<float>[] Floats;
        public TPair<bool>[] Bools;
        public TPair<Vector3>[] V3s;
    }
    public static class SyncDataMsgFunctions
    {
        public static void Serialize(this NetworkWriter w, SyncDataMsg v)
        {
            w.WriteInt(v.Ints.Length);
            w.WriteInt(v.Floats.Length);
            w.WriteInt(v.Bools.Length);
            w.WriteInt(v.V3s.Length);

            for (int i = 0; i < v.Ints.Length; i++) { w.WriteInt(v.Ints[i].i); w.WriteInt(v.Ints[i].v); }
            for (int i = 0; i < v.Floats.Length; i++) { w.WriteInt(v.Floats[i].i); w.WriteFloat(v.Floats[i].v); }
            for (int i = 0; i < v.Bools.Length; i++) { w.WriteInt(v.Bools[i].i); w.WriteBool(v.Bools[i].v); }
            for (int i = 0; i < v.V3s.Length; i++) { w.WriteInt(v.V3s[i].i); w.WriteVector3(v.V3s[i].v); }
        }
        public static SyncDataMsg Deserialize(this NetworkReader r)
        {
            var re = new SyncDataMsg();

            re.Ints = new TPair<int>[r.ReadInt()];
            re.Floats = new TPair<float>[r.ReadInt()];
            re.Bools = new TPair<bool>[r.ReadInt()];
            re.V3s = new TPair<UnityEngine.Vector3>[r.ReadInt()];

            for (int i = 0; i < re.Ints.Length; i++) { re.Ints[i].i = r.ReadInt(); re.Ints[i].v = r.ReadInt(); }
            for (int i = 0; i < re.Floats.Length; i++) { re.Floats[i].i = r.ReadInt(); re.Floats[i].v = r.ReadFloat(); }
            for (int i = 0; i < re.Bools.Length; i++) { re.Bools[i].i = r.ReadInt(); re.Bools[i].v = r.ReadBool(); }
            for (int i = 0; i < re.V3s.Length; i++) { re.V3s[i].i = r.ReadInt(); re.V3s[i].v = r.ReadVector3(); }
            return re;
        }
    }
