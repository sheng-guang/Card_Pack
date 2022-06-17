using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Runtime.InteropServices;

    public static class ByteMsgFunctions
    {
        public static void Serialize(this NetworkWriter w, FixedLenByteMsg value)
        {
            w.WriteInt(value.values.Length);
            w.WriteBytes(value.values,0,value.values.Length);
           
        }
        public static FixedLenByteMsg Deserialize(this NetworkReader r)
        {
            var re = new FixedLenByteMsg();
            re.values = new byte[r.ReadInt()];
            r.ReadBytes(re.values, re.values.Length);
            return re;
        }

    }
    public struct  StructInfo<T>where T : unmanaged
    {
        static StructInfo()
        {
            Length = Marshal.SizeOf<T>();
            Debug.Log("get size for [" + typeof(T).Name + "]    " + Length);
        }
        public static int Length { get; private set; }
    } 
    unsafe public class FixedLenByteMsg
    {
        
        public byte[] values;
        public static FixedLenByteMsg GetByteMsg<T>(T value)where T:unmanaged
        {
            var re = new FixedLenByteMsg();
            var p = (byte*)&value;
            var len  = StructInfo<T>.Length;
            re.values = new byte[len];
            for (int i = 0; i < len; i++)
            {
                re.values[i] = p[i];
            }
            return re;
        }
        public static T GetMsg<T>(FixedLenByteMsg m) where T : unmanaged
        {
            if (m.values.Length < Marshal.SizeOf<T>())
            { Debug.LogError("Length not enoutg");
                return default;
            }
            fixed (byte* p = &m.values[0])
            {
                return *(T*)p;
            }

        }
    }


    public static class ByteMsgExtra
    {
        public static FixedLenByteMsg ToByteMsg<T>(this T t)where T : unmanaged
        {
            return FixedLenByteMsg.GetByteMsg(t);
        }
        public static T ToMsg<T>(this FixedLenByteMsg byt) where T : unmanaged
        {
            return FixedLenByteMsg.GetMsg<T>(byt);
        }
    }

