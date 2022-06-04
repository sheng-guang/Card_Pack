using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public static class CreaterObject
    {


        public static string StaticDir { get; private set; } = "Assets/null";


        public static object GetNew(string FullName, ResArgs args = null) { return GetNew(StaticDir, FullName, args); }
        public static object GetNew(string FullName) { return GetNew(StaticDir, FullName, null); }
        public static object GetNew(string PartDirPath, string kind, ResArgs args = null)
        {
            int k = Ensure(PartDirPath, kind);

            if (k == 1)
            {
                //Debug.Log("1   create from prefabe" + kind);
                return Creater_Prefabe.GetNew(kind, args);
            }
            else if (k == 2)
            {
                //Debug.Log("2   create from ab " + kind);
                return Creater_AssetBundle.GetNew(PartDirPath, kind, args);
            }
            else if (k == 3)
            {
                //Debug.Log("3   create from class " + kind);
                return Creater_Class.GetNew(kind, args);
            }
            return default;
        }
        static Dictionary<string, int> loaded = new Dictionary<string, int>();

        static int Ensure(string dir, string kind)
        {
            if (loaded.TryGetValue(kind, out var re)) return re;
            //
            if (Creater_Prefabe.CanCreate(kind))
            {
                //Debug.Log("1 ["+kind+"]  from prefabe  ");
                loaded[kind] = 1;
                return 1;
            }
            //
            if (Creater_AssetBundle.CanCreate(dir, kind))
            {
                //Debug.Log("2 [" + kind + "]  from AB  ");
                loaded[kind] = 2; 
                return 2; 
            }
            //
            if (Creater_Class.CanCreate(kind))
            {
                //Debug.Log("3 [" + kind + "]  from class  ");
                loaded[kind] = 3;
                return 3; 
            }
            Debug.Log("error: -1 [" + kind + "]  no found ");
            return -1;

        }

    }
}