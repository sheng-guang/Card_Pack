using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Pack
{
   
    public  class AssetsDB<T>
    {
        public static Dictionary<string, AssetSeat<T>> dic = new Dictionary<string, AssetSeat<T>>();
        public static AssetSeat<T> getSeat(string s)
        {
            if (dic.TryGetValue(s, out var re)) return re;
            re = new AssetSeat<T>();
            dic[s] = re;
            return re;
        }
    }

    public class AssetSeat<T>
    {
        public string Key;
        public T value=default;

        public void ListenOnLoad(Action<T> act)
        {
            if (state== AssetState.Loaded) { act(value);return; }
            OnLoad += act;
        }
        Action<T> OnLoad;
        public void OnLoaded(T v)
        {
            value = v;
            OnLoad(v);
            state = AssetState.Loaded;
        }
        public AssetState state= AssetState.Empty;
    }
    public enum AssetState
    {
        Empty,
        Loading,
        Loaded,
    }
}
