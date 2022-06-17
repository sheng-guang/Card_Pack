using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static class Creater_Prefabe
    {
        public static bool CanCreate(string kind)
        {
            if (loaded.ContainsKey(kind)) return loaded[kind] != null;
            loaded[kind] = null;
            PrefabList.TryGetValue(kind, out var g);
            if (g == null) return false;
            IResGetter getter = g.GetComponent<IResGetter>();
            if (getter == null) return false;
            loaded[kind] = getter;
            return true;
        }
        static Dictionary<string, IResGetter> loaded = new Dictionary<string, IResGetter>();
        public static object GetNew(string kind, ResArgs args)
        {
            if (loaded.TryGetValue(kind, out var g) == false || g == null) return default;
            return g.GetNewObject(args);
        }
    }

    public static class Creater_Prefabe<T>
    {
        public static bool CanCreate(string kind)
        {
            if (loaded.ContainsKey(kind)) return loaded[kind] != null;
            loaded[kind] = null;
            PrefabList.TryGetValue(kind, out var g);
            if (g == null) return false;
            IResGetter<T> getter = g.GetComponent<IResGetter<T>>();
            if (getter == null) return false;
            loaded[kind] = getter;
            return true;
        }
        static Dictionary<string, IResGetter<T>> loaded = new Dictionary<string, IResGetter<T>>();
        public static T GetNew(string kind,ResArgs  args)
        {
            if (loaded.TryGetValue(kind, out var g) == false||g==null) return default;
            return g.GetNew(args);
        }
    }
