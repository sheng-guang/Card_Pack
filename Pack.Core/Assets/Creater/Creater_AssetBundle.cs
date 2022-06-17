using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    class Creater_AssetBundle
    {
        public static bool CanCreate(string dir, string kind)
        {
            //have entered;
            if (loaded.ContainsKey(kind)) return loaded[kind] != null;
            loaded[kind] = null;
            string part = dir + "/" + kind + "/" + kind;
            string fullPath = Application.streamingAssetsPath + "/" + part;
            if (File.Exists(fullPath) == false) return false;

            GameObject go = AssetRequest<GameObject>.Load(fullPath, kind);
            if (go == null) return false;
            IResGetter getter = go.GetComponent<IResGetter>();
            if (getter == null) return false;

            LoadedGroup group = new LoadedGroup() { getter = getter };
            loaded[kind] = group;
            return true;
        }
        class LoadedGroup
        {
            public IResGetter getter;
        }
        static Dictionary<string, LoadedGroup> loaded = new Dictionary<string, LoadedGroup>();
        public static object GetNew(string dir, string kind, ResArgs args = null)
        {
            if (loaded.TryGetValue(kind, out var g) == false || g == null) return default;
            return g.getter.GetNewObject(args);
        }
    }
    class Creater_AssetBundle<T>
    {
        public static bool CanCreate(string dir,string kind)
        {
            //have entered;
            if (loaded.ContainsKey(kind) ) return loaded[kind]!=null;
            loaded[kind] = null;
            string part = dir + "/" + kind+"/"+kind;
            string fullPath = Application.streamingAssetsPath + "/" + part;
            if (File.Exists(fullPath) == false) return false;

            GameObject go = AssetRequest<GameObject>.Load(fullPath, kind);
            if (go == null) return false;
            IResGetter<T> getter = go.GetComponent<IResGetter<T>>();
            if (getter == null) return false;
            LoadedGroup group = new LoadedGroup() {  getter = getter };
            loaded[kind] = group;
            return true;
        }
        class LoadedGroup
        {
            public IResGetter<T> getter;
        }
        static Dictionary<string, LoadedGroup> loaded = new Dictionary<string, LoadedGroup>();
        public static T GetNew(string dir,string kind,ResArgs args=null)
        {
            if (loaded.TryGetValue(kind, out var g) == false||g==null) return default;
            return g.getter.GetNew(args);
        }
    }
