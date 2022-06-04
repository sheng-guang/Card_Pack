using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
//[SerializeField]
public class IndexRoundList<T>
{
    public int CreatedStartIndex = 0;
    public int CreatedEndIndex = -1;

    public void RollTo(int TarStart, int TarEnd, Func<T> GetNew, Action<T, int> OnsetNewIndex, Action<T> remove)
    {
        if (CreatedEndIndex == TarEnd && CreatedStartIndex == TarStart) return;
        //先看是否有交集
        bool haveSame = HaveSamePart(CreatedStartIndex, CreatedEndIndex, TarStart, TarEnd);
        if (haveSame == false)
        {
            //Debug.Log("haveSame  false");
            //如果没有直接顺序重置
            int StartDistance = TarStart - CreatedStartIndex;
            CreatedStartIndex += StartDistance;
            CreatedEndIndex += StartDistance;
            //删除多的
            while (CreatedEndIndex > TarEnd)
            {
                remove(PopLast().value);
                CreatedEndIndex--;
            }
            //从头开始重置
            int toFill = TarStart;
            Node To = FirstNode;
            while (toFill <= TarEnd)
            {
                if (To == null) To = AddAtLast(GetNew());
                OnsetNewIndex(To.value, toFill);
                To = To.Next;
                toFill++;
            }
        }
        else
        {
            //Debug.Log("haveSame  true");
            //如果前面多且后面缺少则向后滚动
            while (CreatedStartIndex < TarStart && CreatedEndIndex < TarEnd)
            { RoToNex(OnsetNewIndex); }
            //                                              则向前滚动
            while (CreatedEndIndex > TarEnd && CreatedStartIndex > TarStart)
            { RoToPrevious(OnsetNewIndex); }

            //补全尾
            while (CreatedEndIndex < TarEnd)
            {
                CreatedEndIndex++;
                var ne = AddAtLast(GetNew());
                OnsetNewIndex(ne.value, CreatedEndIndex);
            }
            //去尾
            while (CreatedEndIndex > TarEnd)
            {
                CreatedEndIndex--;
                remove(PopLast().value);
            }

            //补全头
            while (CreatedStartIndex > TarStart)
            {
                CreatedStartIndex--;
                var ne = AddAtFirst(GetNew());
                OnsetNewIndex(ne.value, CreatedStartIndex);
            }
            //去头
            while (CreatedStartIndex < TarStart)
            {
                CreatedStartIndex++;
                remove(PopFirst().value);
            }

        }

    }
    public void ShowEachValue()
    {
        Debug.Log("show------------------------------------------");
        var to = FirstNode;
        while (to != null)
        {
            if (to.Next != null && to.Previous != null)
                Debug.Log("" + to.Previous.value + "-" + to.value + "-" + to.Next.value);
            else if (to.Next != null) Debug.Log("-" + to.value + "-" + to.Next.value);
            else if (to.Previous != null) Debug.Log(to.Previous.value + "-" + to.value + "-");
            else Debug.Log("-"+to.value+"-");
            to = to.Next;
        }
        Debug.Log("show---------------------");
    }
    public void RoToNex(Action<T, int> OnsetNewIndex)
    {
        CreatedStartIndex++;
        CreatedEndIndex++;
        var changed = rollNext();

        OnsetNewIndex(changed.value, CreatedEndIndex);

    }
    public void RoToPrevious(Action<T, int> OnsetNewIndex)
    {
        CreatedStartIndex--;
        CreatedEndIndex--;
        var changed = rollPrevious();
        OnsetNewIndex(changed.value, CreatedStartIndex);
    }
    public bool HaveSamePart(int CreatedS, int CreatedEnd, int ToStart, int ToEnd)
    {
        if (CreatedEnd >= ToStart) return true;
        if (CreatedS <= ToEnd) return true;
        return false;
    }


    //-------------------------------------------------------------------------------------------------
    public Node FirstNode = null;
    public Node LastNode = null;
    public int Count = 0;
    public Node CreatFirstNode()
    {
        FirstNode = LastNode = new Node() { Master = this };
        return FirstNode;
    }

    public Node PopLast()
    {
        Count--;
        if (LastNode == null) return LastNode;
        if (FirstNode == LastNode)
        {
            FirstNode = null;
            LastNode = null;
            return null;
        }
        var re = LastNode;
        LastNode = LastNode.Previous;
        re.RemoveSelf();
        return re;
    }
    public Node PopFirst()
    {
        Count--;
        if (FirstNode == null) return FirstNode;
        if (FirstNode == LastNode)
        {
            FirstNode = null;
            LastNode = null;
            return null;
        }
        var re = FirstNode;
        FirstNode = FirstNode.Next;
        re.RemoveSelf();
        return re;
    }
    public Node rollNext()
    {
        if (Count == 0) { Debug.LogError(" round list count 0"); return null; }
        if (Count <= 1) { Debug.LogError(" round list count 1"); return FirstNode; }
        var re = FirstNode;
        //Debug.Log(" now first state " + FirstNode.value +"  next  " + FirstNode.Next.value);
        FirstNode = FirstNode.Next;
        FirstNode.Previous = null;
        //Debug.Log("Now first" + FirstNode.value);

        re.Next = null;
        re.Previous = LastNode;
        LastNode.Next = re;
        LastNode = re;
        return re;
    }
    public Node rollPrevious()
    {
        if (Count == 0) return null;
        if (Count <= 1) return LastNode;
        var re = LastNode;
        LastNode = LastNode.Previous;
        LastNode.Next = null;

        re.Previous = null;
        re.Next = FirstNode;
        FirstNode.Previous = re;
        FirstNode = re;
        return re;
    }


    public Node AddAtFirst(T value)
    {
        Count++;
        Node to = null;
        if (FirstNode == null) { to = CreatFirstNode(); }
        else { to = FirstNode.AddPrevious(); }
        FirstNode = to;
        to.value = value;
        return to;
    }
    public Node AddAtLast(T value)
    {
        Count++;
        Node to = null;
        if (LastNode == null) { to = CreatFirstNode(); }
        else { to = LastNode.AddNext(); }
        LastNode = to;
        to.value = value;
        return to;
    }
    //[Serializable]
    //[SerializeField]
    public class Node
    {

        public IndexRoundList<T> Master;
        public T value;
        public Node Previous, Next;
        public Node AddNext()
        {
            var ne = new Node() { Master = Master };
            if (Next != null)
            {
                ne.Next = Next;
                Next.Previous = ne;
            }
            ne.Previous = this;
            Next = ne;

            return ne;
        }
        public Node AddPrevious()
        {
            var ne = new Node() { Master = Master };
            if (Previous != null)
            {
                ne.Previous = Previous;
                Previous.Next = ne;
            }
            ne.Next = this;
            Previous = ne;
            return ne;
        }
        public void RemoveSelf()
        {
            if (Next != null)
            {
                Next.Previous = Previous;
            }
            if (Previous != null)
            {
                Previous.Next = Next;
            }
            Previous = null;
            Next = null;
        }
    }
}
