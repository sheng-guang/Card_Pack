using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class UpdCollection<T>
    {
        public void NewGame_Clear()
        {
            Linked.Clear();
            Dic.Clear();
            foreach (var item in KeepSet) { AddToList(item, true); }
        }

        public LinkedList<T> Linked = new LinkedList<T>();
        public Dictionary<T, LinkedListNode<T>> Dic = new Dictionary<T, LinkedListNode<T>>();

        public HashSet<T> KeepSet = new HashSet<T>();
        public  void AddToList(T to,bool Keep=false)
        {
            EnsureInList(to);
            SetDoNotClear(to, Keep);
        }
        void EnsureInList(T to)
        {
            if (Dic.ContainsKey(to)) return;
            Linked.AddLast(to);
            Dic.Add(to, Linked.Last);
        }
        void SetDoNotClear(T to, bool Keep = false)
        {
            //����
            if (Keep) { if (KeepSet.Contains(to) == false) KeepSet.Add(to); }
            //������
            else { if (KeepSet.Contains(to)) KeepSet.Remove(to);  }
        }

        public  void AddToUpdateList_Follow(T follower, T master)
        {

            if (Dic.ContainsKey(master) == false) { AddToList(master); }
            var MasterNode = Dic[master];

            {
                LinkedListNode<T> FollowNode = null;
                if (Dic.ContainsKey(follower))
                {
                    FollowNode = Dic[follower];
                    Dic.Remove(follower);
                    Linked.Remove(FollowNode);
                }
                if (FollowNode == null)
                {
                    FollowNode = new LinkedListNode<T>(follower);
                }
                Linked.AddAfter(MasterNode, FollowNode);
                Dic.Add(follower, FollowNode);
            }
        }

        public void ForEach(Action<T> a)
        {
            LinkedListNode<T> to = Linked.First;
            while (to != null)
            {
                var onUpdate = to;
                to = to.Next;
                if (onUpdate.Value.NotNull_and_NotEqualNull()) a(onUpdate.Value); 
                else Linked.Remove(onUpdate);
            }

        }
    }
