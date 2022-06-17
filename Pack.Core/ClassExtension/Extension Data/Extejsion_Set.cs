using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface ISetObj
    {
        void setValue(object value);
    }
    public static class Extension_ISetObj
    {
        public static void Ex_TrySetExtension(this object obj, string DataName, object value)
        {
            var to = ExtensionRecorder<ISetObj>.Get(obj, DataName);
            if (to == null) { Debug.Log(obj + "  has no extension data:  " + DataName); return; }
            to.setValue(value);
        }
    }
