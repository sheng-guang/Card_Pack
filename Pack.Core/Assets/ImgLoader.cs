using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


    
    public class ImgLoader:MonoBehaviour
    {
        static ImgLoader go_;
        static ImgLoader Loader()
        {
            if (go_) return go_;
            go_ = new GameObject().AddComponent<ImgLoader>();
            DontDestroyOnLoad(go_);
            return go_;
        }
        static Sprite defSprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height), Vector2.zero);

        public static void LoadSprite(Action<Sprite> act,string pathBegin,int which)
        {
            var seat = AssetsDB<Sprite>.getSeat(pathBegin+"/"+which);
            seat.ListenOnLoad(act);
            if(seat.state!= AssetState.Empty) { return; }
            seat.state = AssetState.Loading;
            Action<Texture2D> OnGet = (t) =>
            {
                Sprite s = null;
                if (t == defTexture2D) s = defSprite;
                else s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero);
                seat.OnLoaded(s);
            };
            LoadTexture(OnGet, pathBegin, which);
        }

   
    
        static Texture2D defTexture2D = Texture2D.whiteTexture;
       
        public static void LoadTexture(Action<Texture2D>act, string pathBegin, int which)
        {
            
            var seat = AssetsDB<Texture2D>.getSeat(pathBegin+"/"+which);
            seat.ListenOnLoad(act);
            if (seat.state !=  AssetState.Empty) { return; }
            seat.state = AssetState.Loading;

            string fullpath = "";
            bool useful = false;
            if (useful == false) { fullpath = pathBegin + which + ".jpg"; useful = File.Exists(fullpath); }
            if (useful == false) { fullpath = pathBegin + which + ".png"; useful = File.Exists(fullpath); }
            if (useful == false) { fullpath = pathBegin + 0 + ".png"; useful = File.Exists(fullpath); }
            if (useful == false) { fullpath = pathBegin + 0 + ".jpg"; useful = File.Exists(fullpath); }
            //print(fullpath);
            if (useful == false) { seat.OnLoaded(defTexture2D); return; }

            Loader().StartCoroutine(LoadTextureFile(seat.OnLoaded, fullpath));
        }

        //只负责加图片
        static IEnumerator LoadTextureFile(Action<Texture2D> act, string path)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture("file://" + path))
            {
                yield return www.SendWebRequest();
                //todo
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                    act(defTexture2D);
                }
                else
                {
                    Texture2D ret = DownloadHandlerTexture.GetContent(www);
                    act(ret);
                }
            }
        }
    }

