using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

    public class ResGetter_JS : IResGetter
    {
        public string path;
        public object GetNewObject(ResArgs a)
        {
            var to = File.ReadAllText(path);
            //Debug.Log(to);
            if (Creater_TXT.LoadFromString == null) { Debug.Log("null create function"); return null; }
            try
            {
                return Creater_TXT.LoadFromString.Invoke(to, a);

            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return null;
        }
    }
    public class Creater_TXT
    {
        public static Func<string,ResArgs, object> LoadFromString = null;

        public static bool CanCreate(string dir, string kind)
        {
            string part = dir + "/" + kind + "/" + kind + ".js.txt";
            {
                string full1 = Application.streamingAssetsPath + "/" + part;
                if (File.Exists(full1) == false) goto step2;
                loaded[kind] = new ResGetter_JS() { path = full1 };
                return true;
            }
        step2:
            {
                string full2 = Application.dataPath + "/" + part;
                if (File.Exists(full2)==false) goto step3;
                loaded[kind] = new ResGetter_JS() { path = full2 };
                return true;
            }
        step3:
            return false;
        }
        static Dictionary<string, ResGetter_JS> loaded = new Dictionary<string, ResGetter_JS>();
        public static object GetNew(string dir, string kind, ResArgs args = null)
        {
            if (loaded.TryGetValue(kind, out ResGetter_JS obj) == false || obj == null) return default;
            return obj.GetNewObject(args);
        }
    }
