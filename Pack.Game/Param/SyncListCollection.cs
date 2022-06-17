using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SyncListCollection<T> 
    {

        static Dictionary<int, ParamList<T>> values = new Dictionary<int, ParamList<T>>();

        public static void ApplyChange(int index,int option, T value)
        {
            values[index].ApplyChange(option, value);
        }

        public static void Send(Action<int, int, T> writeFunc)
        {

        }
        public static void OnServerChange(ParamList<T> list,int option,T value)
        {

        }
        List<int> ids = new List<int>();
        List<int> options = new List<int>();
        List<T> data = new List<T>();
    }

