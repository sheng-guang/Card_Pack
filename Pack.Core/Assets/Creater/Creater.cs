using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;


namespace Pack
{
    public class Creater<T>
    {

        public static void ChangeDirName(string dirName)
        {
            StaticDir = dirName;
        }
        public static string StaticDir { get; private set; } ="Assets/"+typeof(T).Name;


        public static T GetNew(string FullName, ResArgs args = null) { return GetNew(StaticDir, FullName, args); }

        public static T GetNew(string PartDirPath,string kind,ResArgs args=null)
        {
            //Debug.Log("create skill: "+PartDirPath + "|" + kind);
           int k= Ensure(PartDirPath, kind);

            if (k == 1)
            {
                //Debug.Log("1   create from prefabe" + kind);
                return Creater_Prefabe<T>.GetNew(kind, args);
            }
            else if (k == 2)
            {
                //Debug.Log("2   create from ab " + kind);
                return Creater_AssetBundle<T>.GetNew(PartDirPath, kind, args);
            }
            else if (k == 3)
            {
                //Debug.Log("3   create from class " + kind);
                return Creater_Class<T>.GetNew(kind, args);
            }
            else if(k == 4)
            {
                //Debug.Log("4   create from txt " + kind);

                try
                {
                    return (T)Creater_TXT.GetNew(PartDirPath, kind, args);

                }
                catch { }
            }
            Debug.Log(PartDirPath + "|" + kind + " can not be creat");
            return default; 
        }
        static Dictionary<string, int> loaded = new Dictionary<string, int>();

        static int Ensure(string dir,string kind)
        {
            if (loaded.TryGetValue(kind, out var re)) return re;
            //Debug.Log("1   Ensure from prefabe" + kind);
            if (Creater_Prefabe<T>.CanCreate(kind)) { loaded[kind] = 1;return 1; }
            //Debug.Log("2   Ensure from ab " + kind);
            if (Creater_AssetBundle<T>.CanCreate(dir, kind)) { loaded[kind] = 2;return 2; }
            //Debug.Log("3   Ensure from class " + kind);
            if (Creater_Class<T>.CanCreate(kind)) { loaded[kind] = 3;return 3; }
            if(Creater_TXT.CanCreate(dir, kind)) { loaded[kind] = 4; return 4; }
            return -1;
            
        }

    }
}

