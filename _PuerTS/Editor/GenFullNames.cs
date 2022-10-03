using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class GenFullNames 
{
    [MenuItem("Puerts/Gen FullNames")]
    public static void Gen()
    {
        Debug.Log("GenFullNames");
        Gen0();
        Gen1();
        Gen2();
        //string dir= Application.dataPath + "/Assets";
        //DirectoryInfo info = new DirectoryInfo(dir);

        //List<string> ts = new List<string>();
        //List <string> js=new List<string> ();
        //ts.Add("declare module 'FullName'{");
        //ts.Add("  namespace fullN {");
        //js.Add("fullN={}");
        //foreach (var kind in info.GetDirectories())
        //{
        //    if (kind.Name.StartsWith('.')) continue;
        //    ts.Add("    class " + kind.Name + " {");
        //    js.Add("  fullN." + kind.Name + "={ }");
        //    foreach (var item in kind.GetDirectories())
        //    {
        //        var name = item.Name.Replace('\'', '_');
        //        name=name.Replace('-', '_');
        //        //string example= " public static UseMagic : string=\"UseMagic\";";
        //        string line= $"      public static {name} : string=\"{item.Name}\";";
        //        ts.Add(line);

        //        string linejs = $"    fullN.{kind.Name}.{name}=\"{item.Name}\"";
        //        js.Add(linejs);
        //    }
        //    ts.Add("    }");
        //}
        //ts.Add("  }");
        //ts.Add("}");
        //js.Add("exports.fullN=fullN;");
        //File.WriteAllLines(dir + "/DTS/FullName.d.ts", ts);
        //Debug.Log(dir + "/DTS/FullName.d.ts");
        //File.WriteAllLines(dir + "/DTS/Resources/FullName.mjs", js);
        //Debug.Log(dir + "/DTS/Resources/FullName.mjs");

    }
    class Str2
    {
        public string Name;
        public string Value;
    }
    public static void Gen0()
    {
        dics.Clear();
        string dir = Application.dataPath + "/Assets";
        DirectoryInfo info = new DirectoryInfo(dir);
        foreach (var kind in info.GetDirectories())
        {
            if (kind.Name.StartsWith('.')) continue;
            var KindSet = new Dictionary<string, string>();
            dics.Add(kind.Name, KindSet);

            foreach (var item in kind.GetDirectories())
            {
                var Val = item.Name;
                Val = Val.Replace("'"+GraphSetting.StrDef, "");
                Val = Val.Replace("'"+GraphSetting.StrHDRP, "");

                var Key = Val.Replace('\'', '_');
                Key = Key.Replace('-', '_');
                if (KindSet.ContainsKey(Key) == false) KindSet.Add(Key,Val);
            }

        }
    }
    static Dictionary<string, Dictionary<string, string>> dics = new Dictionary<string, Dictionary<string, string>>();
    public static void Gen1()
    {
        ts.Clear();
        js.Clear();
        ts.Add("declare module 'FullName'{");
        ts.Add("  namespace fullN {");
        js.Add("fullN={}");

        foreach (var item in dics)
        {
            var Dir = item.Key;
            ts.Add("    class " + Dir + " {");
            js.Add("  fullN." + Dir + "={ }");
            foreach (var pair in item.Value)
            {
                string line = $"      public static {pair.Key} : string=\"{pair.Value}\";";
                ts.Add(line);

                string linejs = $"    fullN.{Dir}.{pair.Key}=\"{pair.Value}\"";
                js.Add(linejs);
            }
            ts.Add("    }");
        }
        ts.Add("  }");
        ts.Add("}");
        js.Add("exports.fullN=fullN;");

    }
    static List<string> ts = new List<string>();
    static List<string> js = new List<string>();
    public static void Gen2()
    {
        string dir = Application.dataPath + "/Assets";
        File.WriteAllLines(dir + "/DTS/FullName.d.ts", ts);
        Debug.Log(dir + "/DTS/FullName.d.ts");
        File.WriteAllLines(dir + "/DTS/Resources/FullName.mjs", js);
        Debug.Log(dir + "/DTS/Resources/FullName.mjs");
    }
}
