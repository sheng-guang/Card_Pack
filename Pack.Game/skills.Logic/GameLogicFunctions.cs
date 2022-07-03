using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public static class GameLogicFunctions
{
    static GameLogicFunctions()
    {
        NewGameClear.AddToNewGameClearList(clear);
    }

    static Action clear = () =>
    {
        stackSkills.Clear();
        LongSkills.Clear();
    };

    //瞬间技能
    //----------------------------------------------------------------------------------------------------------------------------------------------
    public static void AddToStackSkillList(this IStackSkill s) { AddStackSkill(s); }
    public static void AddStackSkill(IStackSkill s)
    {
        s.EnsureStackComp();
        Debug.Log("┌─stack skill   [" + s + "]");

        stackSkills.Add(s);
        FreshStackIndex();
        s.SetExistStackAction(RemoveStackSkill);
        s.Stack_Start();
    }
    static List<IStackSkill> stackSkills = new List<IStackSkill>();
    public static bool DoStack_IsEnd(out int count)
    {
        count = 0;
        bool isEnd = true;
        while (stackSkills.Count != 0)
        {
            count++;
            var toStackSkill = stackSkills[stackSkills.Count - 1];
            if (toStackSkill.Run_ToBreak()) { isEnd = false; break; ; }
            //防止卡住
            if (count % 30 == 0)
            { Debug.Log("stack30 times break once"); isEnd = false; break; }
        }
        FreshStackIndex();
        return isEnd;
    }
    public static void FreshStackIndex()
    {
        for (int i = 0; i < stackSkills.Count; i++)
        {
            stackSkills[i].SetStackIndex(i, stackSkills.Count);
        }
    }

    public static void RemoveStackSkill(object s)
    {
        Debug.Log("└─Stack skill   [" + s + "]");
        var to = s as IStackSkill;
        to.SetStackIndex(-1, stackSkills.Count);
        stackSkills.Remove(to);
    }
    //长技能
    //-----------------------------------------------------------------------------------------------------------------------------------------------
    public static void AddToLongSkillList(this ILongSkill s) { AddLongSkill(s); }
    public static void AddLongSkill(ILongSkill s)
    {
        s.EnsureLongSkillComp();
        Debug.Log("    ┌─Long skill   [" + s + "]");
        LongSkills.AddLast(s);
        s.SetExitListAction(RemoveLongSkill);
        s.Fix_Start();
    }
    static LinkedList<ILongSkill> LongSkills = new LinkedList<ILongSkill>();
    public static void RemoveLongSkill(object s)
    {
        Debug.Log("    └─Long skill   [" + s + "]");
        var to =s as ILongSkill;
        to.SetLongListIndex(-1, LongSkills.Count);
        LongSkills.Remove(to);
    }
    //会先运行第一个技能
    //即driver的fix技能
    public static void DoSkillList(bool is50)
    {
        var To = LongSkills.Last;
        while (To != null)
        {
            var now = To;
            To = To.Previous;
            now.Value.Fix();
        }

        if (is50 == false) return;
        To = LongSkills.Last;
        while (To != null)
        {
            var now = To;
            To = To.Previous;
            now.Value.Fix50();
        }
        FreshLongSkillIndex();
    }
    public static void FreshLongSkillIndex()
    {
        var to = LongSkills.First;
        int ToIndex = 0;
        int total = LongSkills.Count;
        while (to != null)
        {
            var now = to;
            to = to.Next;
            now.Value.SetLongListIndex(ToIndex, total);
            ToIndex++;
        }

    }



}
