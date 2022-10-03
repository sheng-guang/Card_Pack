using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public class ListOption
    {
        public const int Add = 1;
        public const int Remove = 0;
    }
    public class ParamList<T>
    {
        public T this[int index]
        {
            get {return ValueList[index]; }
            set { ValueList[index] = value; }
        }
        public int Count => ValueList.Count;
        public int Index;
        public bool IsConnected = false;
        static Var<bool> IsServer = Setting<bool>.GetSetting(nn.IsServer);
        bool isServer => IsServer.Value;
        List<T> ValueList = new List<T>();

        public void Add(T ne)
        {
            ValueList.Add(ne);
            OnChange(ListOption.Add, ne);
        }
        public void Remove(T to)
        {
            ValueList.Remove(to);
            OnChange(ListOption.Remove, to);
        }
        Action<int,T> onChange = null;
        public void Listen(Action<int, T> act)
        {
            onChange += act;
        }
        void OnChange(int option, T value)
        {
            //todo
            if (isServer&&IsConnected)
            {
                SyncListCollection<T>.OnServerChange(this, option, value);
            }
            onChange?.Invoke(option, value);
        }
        public void ApplyChange(int option,T value)
        {
            if (option ==ListOption.Remove)
            {
                Remove(value);
            }
            else if (option ==ListOption.Add)
            {
                Add(value);
            }
        }

    }
