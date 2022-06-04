using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
namespace Pack
{
   public class Creater_Class
    {
        public static bool CanCreate(string kind)
        {
            if (loaded.ContainsKey(kind)) return loaded[kind] != null;
            loaded[kind] = null;
            Type ToType=null;
            try
            {
                //type = Type.GetType(kind, true, true);
                ToType = Assembly.Load("Assembly-CSharp").GetType(kind, true, true);
            }
            catch (Exception) { return false; }
            //Debug.Log("can GetNew type  " + type);
            if (ToType == null) return false;
            loaded[kind] = new TypeInfos() 
            { t = ToType ,IsISetResArgs=InterfaceType.IsAssignableFrom(ToType)};
            return true;

        }
        static Type InterfaceType = typeof(ISetResArgs);
        public class TypeInfos
        {
            public Type t;
            public bool IsISetResArgs = false;
        }
        static Dictionary<string, TypeInfos> loaded = new Dictionary<string, TypeInfos>();
        public static object GetNew(string kind, ResArgs args)
        {
            //Debug.Log("GetNew class  " + kind);
            if (loaded.TryGetValue(kind, out var t) == false || t == null) return default;
            var re= Activator.CreateInstance(t.t);
            if (t.IsISetResArgs) (re as ISetResArgs).SetResArgs(args);
            return re;
        }
    }
    public interface ISetResArgs
    {
         void SetResArgs(ResArgs r);
    }

    public class Creater_Class<T>
    {
        public static bool CanCreate(string kind)
        {
            return Creater_Class.CanCreate(kind);
        }
        public static T GetNew(string kind,ResArgs args)
        {
            return (T)Creater_Class.GetNew(kind, args);
        }
    }
}
