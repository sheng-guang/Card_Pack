using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class GenFullNames 
{
    public string aaa;
    [MenuItem("Puerts/Gen FullNames")]
    public static void Gen()
    {
        Debug.Log("GenFullNames");
        string dir= Application.dataPath + "\\Assets";
        DirectoryInfo info = new DirectoryInfo(dir);

        List<string> l = new List<string>();
        List <string> js=new List<string> ();
        l.Add("declare module 'FullName'{");
        l.Add("  namespace fullName {");
        js.Add("fullName={}");
        foreach (var kind in info.GetDirectories())
        {
            if (kind.Name.StartsWith('.')) continue;
            l.Add("    class " + kind.Name + " {");
            js.Add("  fullName." + kind.Name + "={ }");
            foreach (var item in kind.GetDirectories())
            {
                var name = item.Name.Replace('\'', '_');
                name=name.Replace('-', '_');
                //string example= " public static UseMagic : string=\"UseMagic\";";
                string line= $"      public static {name} : string=\"{item.Name}\";";
                l.Add(line);

                string linejs = $"    fullName.{kind.Name}.{name}=\"{item.Name}\"";
                js.Add(linejs);
            }
            l.Add("    }");
        }
        l.Add("  }");
        l.Add("}");
        js.Add("exports.fullName=fullName;");
        File.WriteAllLines(dir + "\\DTS\\FullName.d.ts", l);
        File.WriteAllLines(dir + "\\DTS\\Resources\\FullName.mjs", js);
    }

}
