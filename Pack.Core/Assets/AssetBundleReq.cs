using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class AssetBundleReq
    {
        static Dictionary<string, AssetBundleReq> Requests = new Dictionary<string, AssetBundleReq>();
        public static AssetBundle Load(string fullPath)
        {
            if (Requests.TryGetValue(fullPath, out var request)) return request.ab;
            {
                request = new AssetBundleReq();
                Requests[fullPath] = request;
                request.ab = AssetBundle.LoadFromFile(fullPath);
            }
            if (request.ab == null)
            {
                Debug.LogWarning("error: ab empty   " + fullPath);
                if (request.req != null) Debug.LogWarning("its async");
            }
            return request.ab;
        }
        AssetBundle ab;
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void LoadAsync(string fullPath, Action<AssetBundle> act)
        {
            if (Requests.TryGetValue(fullPath, out var request)) { request.ListenOnLoad(act); return; }
            {
                request = new AssetBundleReq();
                Requests[fullPath] = request;
                request.req = AssetBundle.LoadFromFileAsync(fullPath);
                request.req.completed += request.OnAssetLoaded;
            }
            request.ListenOnLoad(act);
        }

        AssetBundleCreateRequest req;
        void OnAssetLoaded(AsyncOperation operation)
        {
            ab = req.assetBundle;
            WaitLoad?.Invoke(ab);
        }
        Action<AssetBundle> WaitLoad;



        void ListenOnLoad(Action<AssetBundle> act)
        {
            if (ab != null) { act(ab); return; }
            WaitLoad += act;
        }
    }
