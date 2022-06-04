using System.Collections.Generic;
using Puerts;
using System;
using UnityEngine;
using System.Reflection;
using System.Linq;

[Configure]
public class ExamplesCfg_Pack
{
    static IEnumerable<Type> b()
    {
        {
            var to = Assembly.Load("Assembly-CSharp").GetExportedTypes().Where(x => x.Namespace == "Pack");
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
            yield return typeof(VarUnit);
            yield return typeof(LSkill);
            yield return typeof(Func<int>);
            yield return typeof(T<int>);
            yield return typeof(LongSkil_Delegate);
            yield return typeof(SkillNodeResult);

            yield return typeof(eve);
        }
    }
    [Binding]
    static IEnumerable<Type> Bindings => b();


}



