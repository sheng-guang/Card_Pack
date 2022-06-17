using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    public class ASyncCreater<T> where T : UnityEngine.Object
    {
        public static void Load(string PartDirPath, string name, Action<T> call)
        {
            var FullPath = Application.streamingAssetsPath + "/" + PartDirPath;
            Debug.Log(FullPath + "   name      " + name);
            AssetRequest<T>.LoadAsync(FullPath, name,call);
        }
    }



