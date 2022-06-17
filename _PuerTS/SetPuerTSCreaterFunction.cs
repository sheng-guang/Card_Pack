using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Puerts;
using System;
public static class Creater_TXT_PuerTS
{
    public static void EnsureExports(this JsEnv env)
    {
        env.Eval("if(!exports) var exports={}");
    }
    [RuntimeInitializeOnLoadMethod]
    static void Load()
    {
        Debug.Log("[Load]" + nameof(Creater_TXT_PuerTS));
        Creater_TXT.LoadFromString = Create;
    }
    public static object Create(string s, ResArgs a)
    {
        JsEnv env = new JsEnv();
        env.UsingAction<string, object>();
        env.UsingFunc<Vector3>();
        env.UsingFunc<int>();

        env.EnsureExports();
        env.Eval(s);
        var to = env.Eval<Func<ResArgs, object>>("create");
        if (to == null) return null;
        return to.Invoke(a);
    }
}
