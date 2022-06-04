using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public class ResArgs
    {
        public Dictionary<string, object> dictionary = new Dictionary<string, object>(); 
    }

    public static class ResArgExtra
    {

        public static ResArgs SetPoss(this ResArgs arg, Vector3 v)
        {
            if (arg == null) return null;
            arg.dictionary.Add(NN.poss, v);
            return arg;
        }
        public static Vector3? GetPoss(this ResArgs arg)
        {
            if (arg == null) return null;
            if (arg.dictionary.TryGetValue(NN.poss, out var re) == false) return null;
           return re as Vector3?;
        }
        public static ResArgs SetParent(this ResArgs arg, Component t)
        {
            if (arg == null) return null;
            arg.dictionary.Add(NN.transf, t.transform);
            return arg;
        }

        public static Transform GetParent(this ResArgs arg)
        {
            if (arg == null) return null;
            if(arg.dictionary.TryGetValue(NN.transf, out var re) == false) return null;
            return re as Transform;
        }
        public static ResArgs SetObj(this ResArgs arg,string key,object obj)
        {
            if (arg == null) return null;
            arg.dictionary[key] = obj;
            return arg;
        }
       public static object GetObj(this ResArgs arg,string key)
        {
            if (arg == null) return null;
            arg.dictionary.TryGetValue(key, out var re);
            return re;
        }
        

        public static ResArgs SetFloat(this ResArgs arg, string key, float v)
        {
            if (arg == null) return null;
            arg.dictionary[key] = v;
            return arg;
        }
        public static float GetFloat(this ResArgs arg, string key, float def = 0)
        {
            if (arg == null) return def;
            if( arg.dictionary.TryGetValue(key, out var v)==false) return def;
            var re = v as float?; 
            return re.HasValue? re.Value : def;
        }

        public static ResArgs SetInt(this ResArgs arg, string key, int v)
        {
            if (arg == null) return null;
            arg.dictionary[key]=v;
            return arg;
        }
        public static int GetInt(this ResArgs arg, string key, int def = 0)
        {
            if (arg == null) return def;
            if (arg.dictionary.TryGetValue(key, out var value) == false) return def;
            var re=value as int?;
            return re.HasValue? re.Value :def;
        }

        public static ResArgs SetBool(this ResArgs arg, string key, bool v)
        {
            if (arg == null) return null;
            arg.dictionary.Add(key, v);
            return arg;
        }
        public static bool GetBool(this ResArgs arg, string key, bool def = false)
        {
            if (arg == null) return def;
            if(arg.dictionary.TryGetValue(key,out var value) == false) return def;
            var re=value as bool?;
            return re.HasValue? re.Value :def;
        }


        public static T Ex_Instantiate<T>(this T obj, ResArgs args) where T : UnityEngine.Object
        {
            var poss = args.GetPoss();
            var par = args.GetParent();
            //if (par) Debug.Log(par.name);
            if (poss != null)
            {
                //Debug.Log(obj + "   poss :  " + poss);

                var re = Object.Instantiate(obj, poss.Value, Quaternion.identity, par);
                //Debug.Log("  "+re+ (re as Component).transform.position);
                return re;
            }
            if (par)
            {
                return Object.Instantiate(obj, par);

            }
            return Object.Instantiate(obj);
        }

    }


}