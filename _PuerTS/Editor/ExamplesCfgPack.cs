using System.Collections.Generic;
using Puerts;
using System;
using UnityEngine;
using System.Reflection;
using System.Linq;
using Pack;

[Configure]
public class ExamplesCfg_Pack
{
    static IEnumerable<Type> b()
    {
        {
            var to = Assembly.Load("Assembly-CSharp")
                .GetExportedTypes()
                .Where(x => {
                    return 
                    x.Namespace == "Pack"
                    || x.GetCustomAttribute<ApiAttribute>() != null
                    ;
                    });
            foreach (var t in to) yield return t;
        }
        {
            var to = Assembly.Load("Pack.Core").GetExportedTypes();
            foreach (var t in to) yield return t;
        }
        {
            var to = Assembly.Load("Pack.Game").GetExportedTypes();
            foreach (var t in to) yield return t;
        }
        {
            yield return typeof(ts);
            yield return typeof(eve);

        }
        {
            yield return typeof(TsInit);
        }

        {
            yield return typeof(VarUnit);


            yield return typeof(Func<int>);
            yield return typeof(T<int>);



            yield return typeof(Skill);

            yield return typeof(Skill_Delegate);
            yield return typeof(ISkill_Delegate);

            yield return typeof(LongSkil_Delegate);
            yield return typeof(ILongSkil_Delegate);
            yield return typeof(BeCallSkill_Delegate);
            yield return typeof(IBeCallSkill_Delegate);
            yield return typeof(InputSkill_Delegate);
            yield return typeof(IInputSkill_Delegate);
            yield return typeof(StackSkill_Delegate);
            yield return typeof(IStackSkill_Delegate);
            yield return typeof(MixSkill_Delegate);
            yield return typeof(IMixSkill_Delegate);
            yield return typeof(TriggerSkill_Delegate);
            yield return typeof(ITriggerSkill_Delegate);
            yield return typeof(CallReaction_Delegate);

            yield return typeof(Buff_Delegate);
        }
        {//skill
            yield return typeof(SkillNodeGroup);
            yield return typeof(NodeMixSelf<Skill, SkillNodeGroup, SKillNode>.group_Self);
            yield return typeof(NodeMixSelf<Skill, SkillNodeGroup, SKillNode>.node_Self);

            yield return typeof(NodeMix< SkillNodeGroup, SKillNode>.groupBase);
            yield return typeof(NodeMix< SkillNodeGroup, SKillNode>.nodeBase);

        }
        { 


            yield return typeof(SkillNodeResult);
            yield return typeof(CallSys);

            yield return typeof(FuncMix<int, int, Func0<int, int>, Func0Node<int, int>>.Func);


            yield return typeof(Func0<int, int>);
            yield return typeof(N<int>);
            yield return typeof(Func1<int, int, int>);
            yield return typeof(Func2<int, int, int,int>);
            yield return typeof(Func3<int, int, int,int,int>);

            yield return typeof(Act0<int>);
            yield return typeof(Act1<int,int>);
            yield return typeof(Act2<int, int, int>);
            yield return typeof(Act3<int, int, int, int>);
            

            yield return typeof(ParamBuffable<int>);
        }
        {

        }
    }
    [Binding]
    static IEnumerable<Type> Bindings => b();


}



