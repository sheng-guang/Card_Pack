using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public class HashSet_List_Loop<T>
    {

        HashSet<T> WholeFreshAdded = new HashSet<T>();
        HashSet_List<T> set_list = new HashSet_List<T>();
        //List<T> _list { get { return set_list.List; } }

        public T Get(int index)=>set_list.Get(index);

        public int OneLoopCount => set_list.Count;
        public void Add(T v)
        {
            if (WholeFreshAdded.Contains(v))
            {
                if (set_list.Contains(v)) return;
                Debug.Log("last loop added");
                return;
            }
            WholeFreshAdded.Add(v);
            set_list.EnsureAdd(v);

        }

        public void ClearAll()
        {
            WholeFreshAdded.Clear();
            set_list.Clear();
        }
        public void ClearOneLoop()
        {
            set_list.Clear();
        }
    }

    public class HashSet_List<T>
    {

        HashSet<T> added = new HashSet<T>();
        List<T> list = new List<T>();

        public List<T> List => list;
        public int Count => list.Count;
        public T Get(int index)
        {
            return list[index];
        }
        public bool Contains(T v)
        {
            return added.Contains(v);
        }
        public void EnsureAdd(T v)
        {
            if(added.Contains(v)) return; 
            added.Add(v);
            list.Add(v);
        }
        public void Clear()
        {
            added.Clear();
            list.Clear();
        }
    }
    public static class Dictionary_ListExtra
    {
        public static V Ensure<K,V>(this Dictionary_List<K,V>  dic,K key) where V : new()
        {
            if (dic.DicTryGetGet(key,out var re) == false)
            {
                re= new V();
                dic.Add(key,re);
            }
            return re;
        }
        public static V Ensure_SetKey<K, V>(this Dictionary_List<K, V> dic, K key) where V :ISet<K>, new()
        {
            if (dic.DicTryGetGet(key, out var re) == false)
            {
                re = new V();
                re.Value = key;
                dic.Add(key, re);
            }
            return re;
        }
    }
    public class Dictionary_List<K,V>
    {
        Dictionary<K, V> Added = new Dictionary<K, V>();
        List<V> list = new List<V>();
        public void ForEach(Action<V> act)
        {
            for(int i = 0; i < list.Count; i++)
            {
                act(list[i]);
            }
        }
        public int Count=>list.Count;
        public V Get(int index) { return list[index]; }
        public bool DicTryGetGet(K key,out V re) 
        {
           return Added.TryGetValue(key, out re);
        }
        public bool ContainsKey(K k)
        {
            return Added.ContainsKey(k);
        }

        public void Add(K key,V v)
        {
            if (Added.ContainsKey(key)) return;
            Added.Add(key,v);
            list.Add(v);
        }
        public void Clear()
        {
            Added.Clear();
            list.Clear();
        }
    }


    public class Dictionary_List_Loop<K,V>
    {
        Dictionary<K,V> WholeFreshAdded = new Dictionary<K,V>();
        Dictionary_List<K,V> dictionary_List=new Dictionary_List<K,V>();
        public int OneLoopCount => dictionary_List.Count;
        public V Get(int index)
        {
            return dictionary_List.Get(index);
        }
        
        public void Add(K k,V v)
        {
            if (WholeFreshAdded.ContainsKey(k))
            {
                if (dictionary_List.ContainsKey(k)) return;
                Debug.Log("last loop added");
                return;
            }
            WholeFreshAdded.Add(k,v);
            dictionary_List.Add(k,v);

        }


        public void ClearAll()
        {
            WholeFreshAdded.Clear();
            dictionary_List.Clear();
        }
        public void ClearOneLoop()
        {
            dictionary_List.Clear();
        }
    
        public bool Contain(K k)
        {
            return WholeFreshAdded.ContainsKey(k);
        }


    }

