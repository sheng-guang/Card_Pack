using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Pack
{
    public static class ImageExtra
    {
        
        static string dirPath = "Img";
        public static void LoadImage(this Image m, string FullName, int which, bool fit_HW = true, Func<bool> Continue = null,bool ClearBeforeLoaded=true)
        {
            if (ClearBeforeLoaded) m.sprite = null;
            string dir= (dirPath+"/"+ FullName).ToStreamingAssetFullPath()+"/";
            Action<Sprite> act = (s) => {
                if (Continue != null && Continue()) return;
                m.sprite = s;
                if (fit_HW == false) return;
                m.preserveAspect = true;
                var parTr= m.transform.parent.GetComponent<RectTransform>();
                var Pw_h = parTr.rect.width / parTr.rect.height;
                var Tr = m.transform.GetComponent<RectTransform>();
                var w_h = Tr.rect.width / Tr.rect.height;
                if (w_h > Pw_h)
                {
                    //比mask宽
                    //同样高度
                    //缩放宽度
                    float h = parTr.sizeDelta.y;
                    float w = h * w_h+10;
                    Tr.sizeDelta = new Vector2(w,h);
                }
                else
                {
                    //比mask 高
                    //同样宽度
                    //缩放高度
                    float w = parTr.sizeDelta.x;
                    float h = (w / w_h) + 10;
                    Tr.sizeDelta = new Vector2(w,h);
                }

            };
            ImgLoader.LoadSprite(act, dir, which);
        }
        public static void LoadTexture(this Renderer m, string TextureName, float Pw_h, string FullName, int which,string OffSetV4Name= "_MainTex_ST", bool fit_HW = true, Func<bool> Continue = null)
        {
            string dir = (dirPath + "/" + FullName).ToStreamingAssetFullPath() + "/";
            Action<Texture2D> act = (s) => {
                if (Continue != null && Continue()) return;


                MaterialPropertyBlock block = new MaterialPropertyBlock();
                m.GetPropertyBlock(block);
                block.SetTexture(TextureName, s);
                if (fit_HW)
                {
                    Vector2 tilling = Vector2.one;
                    Vector2 offset = Vector2.zero;
                    var w_h = 1f*s.width / s.height;
                    if (w_h > Pw_h)
                    {
                        //比背景宽
                        //同样高度
                        //缩放宽度
                        tilling.y = 1;//高度撑满
                        tilling.x = Pw_h / w_h;//x<1
                        offset.x = (1 - tilling.x) / 2;
                    }
                    else
                    {
                        //比mask 高
                        //同样宽度
                        //缩放高度
                        tilling.x = 1;
                        tilling.y = w_h / Pw_h;//y<1
                        offset.y = (1 - tilling.y) / 2;
                    }
                   block.SetVector(OffSetV4Name, new Vector4(tilling.x, tilling.y, offset.x, offset.y));
                }


                m.SetPropertyBlock(block);


             

            };
            ImgLoader.LoadTexture(act, dir, which);
        }
    }
}
  


