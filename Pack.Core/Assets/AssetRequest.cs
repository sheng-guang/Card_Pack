using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    public class AssetRequest<T>where T : UnityEngine.Object
    {
        static Dictionary<string, AssetRequest<T>> dic = new Dictionary<string, AssetRequest<T>>();
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static T Load(string fullPath,string name)
        {
            string key = fullPath + "|" + name;
            if (dic.TryGetValue(key, out var re)) return re.asset;
            {
                re = new AssetRequest<T>();
                dic[key] = re;
                var ab = AssetBundleReq.Load(fullPath);
                if (ab == null) return null;
                re.asset = ab.LoadAsset<T>(name);
            }
            if (re.asset == null)
            {
                Debug.LogWarning("error: ab empty   " + fullPath);
                if (re.assetRequest != null) Debug.LogWarning("its async");
            }
            return re.asset;
        }
        T asset;
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void LoadAsync(string fullPath,string name,Action<T>act)
        {
            string key = fullPath + "|" + name;
            if (dic.TryGetValue(key, out var request)) { request.ListenOnLoad(act); return; }
            {
                request = new AssetRequest<T>();
                dic[key] = request;
                request.assetName = name;
            }
            AssetBundleReq.LoadAsync(fullPath, request.OnAssetBundleLoaded);
            request.ListenOnLoad(act);
        }
        string assetName;
        public void OnAssetBundleLoaded(AssetBundle ab)
        {
            assetRequest = ab.LoadAssetAsync<T>(assetName);
            assetRequest.completed += OnAssetLoaded;
        }
        AssetBundleRequest assetRequest;
        private void OnAssetLoaded(AsyncOperation obj)
        {
            OnComplete?.Invoke(assetRequest.asset as T);
        }
        Action<T> OnComplete;
        public void ListenOnLoad(Action<T> a)
        {
            if (assetRequest != null && assetRequest.isDone) a(assetRequest.asset as T);
            else OnComplete += a;
        }
    }















