using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pack;

namespace Pack
{    
    
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public static class SkillNodeGroupExtra
    {
        public enum SkillNodeGroupKind
        {
            FixStart,
            Fix,
            Fix50,
            StackStart,
            Stack_ToBreak,

        }
        public static SKillNode Add(this MixSkill_Delegate skill,SkillNodeGroupKind k, string FullName)
        {
            SkillNodeGroup g = null;
            switch (k)
            {
                case SkillNodeGroupKind.FixStart:
                    g = skill.A_FixStart;
                    break;
                case SkillNodeGroupKind.Fix:
                    g = skill.A_Fix;
                    break;
                case SkillNodeGroupKind.Fix50:
                    g = skill.A_Fix50;
                    break;
                case SkillNodeGroupKind.StackStart:
                    g = skill.A_StackStart;
                    break;
                case SkillNodeGroupKind.Stack_ToBreak:
                    g = skill.A_Run_ToBreak;
                    break;
                default:
                    Debug.Log("no " + k);
                    return null;
                    break;
            }
 
            var ne = CreaterObject.GetNew(FullName);
            var node = ne as SKillNode;
            if (ne == null) return null;
            g.AddNode(node);
            return node;
        }

        public static SKillNode Add(this SkillNodeGroup g,string FullName)
        {
           var ne= CreaterObject.GetNew(FullName);
            var node = ne as SKillNode;
            if (ne == null) return null;
            g.AddNode(node);
            return node;
        }
        public static SKillNode AddNod(this SkillNodeGroup g, SKillNode node)
        {
            g.AddNode(node);
            return node;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public abstract class SkillNodeGroup : NodeMixSelf<Skill, SkillNodeGroup, SKillNode>.group_Self
    {
        public void AddFunc(Func<int> f)
        {
            AddNode(new SkillNodeFunc() { func = f });
        }
        public void SetExistAction(Action<object> a) { ExistAct = a; }
        protected virtual void Exist() { ExistAct?.Invoke(self); }
        Action<object> ExistAct;
    }



    public abstract class SKillNode: NodeMixSelf<Skill, SkillNodeGroup, SKillNode>.node_Self
    {
        public override string ToString()
        {
            return GetType().Name;
        }
        public Host host =>self.up.host;
        public Host GetHost() { return host; }

        public Player player => self.up.player;
        public Player GetPlayer() { return player; }

        public Unit unit => self.up.unit;
        public Unit GetUnit() { return unit; }

        public Driver driver => Layer.driver;
        public Driver GetDriver() { return driver; }
        //public void CastToIGet<T>(object o,ref IGet<T> re)
        //{
        //    if(o is ParamName)
        //}
        public abstract int Fix_1Exit_2ToNext_4Break();
        public const int Exit = 1;
        public const int ToNext = 2;
        public const int Break_TryThisAgain = 4;
    }

    public class SkillNodeFunc : SKillNode
    {
        public Func<int> func;
        public override int Fix_1Exit_2ToNext_4Break()
        {
            if (func != null) return func.Invoke();
            return ToNext;
        }
    }
}
public class SkillNodeResult
{
    public const int Exit = SKillNode.Exit;
    public const int ToNext = SKillNode.ToNext;
    public const int Break_TryThisAgain = SKillNode.Break_TryThisAgain;
}
