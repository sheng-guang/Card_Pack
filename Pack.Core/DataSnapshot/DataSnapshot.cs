using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //public interface IGetT<T>
    //{
    //    T value { get; }
    //    string index { get; }
    //}
    public interface ISnapshotAbleData
    {
         bool IsChanged(string DataSnapshotKey);
    }
    public class DataSnapshot<T>
    {
        static Dictionary<string,T>LastData=new Dictionary<string,T>();
        public static bool NewData_Changed(string Key,T value)
        {
            if (LastData.ContainsKey(Key) == false)
            {
                LastData[Key] = value;
                return true;
            }
            var re = !LastData[Key].Equals(value);
            //Debug.Log(Key+"]    "+value + "  ==?  " + LastData[Key]+"  "+re);

            LastData[Key] = value;
            return re;

        }
    }


