using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace Pack
{
    [CustomEditor(typeof(SaveCameraPic))]

    public class ForTakePicture : Editor<SaveCameraPic>
    {
        public override void Awake()
        {
            base.Awake();

        }
  
        public override void OnInspectorGUI()
        {
            FreshPoss();
            FreshBackGround();
            if (GUILayout.Button("take picture"))
            {
                tar.TakePhoto_SavePNG();
            }
            base.OnInspectorGUI();

     
       
        }
        void FreshPoss()
        {
            tar.ro_dis.SetAsMaxIfOverMax(ref tar.PossIndex);
            if (tar.PossIndex < 0) tar.PossIndex = 0;
            if (tar.ro_dis.Count == 0) return;

            var to = tar.ro_dis[tar.PossIndex];
            tar.ro.eulerAngles = to.ro;
            tar.dis.localPosition = to.dis;
        }
        void FreshBackGround()
        {
            tar.backgrounds.SetAsMaxIfOverMax(ref tar.BackGroundIndex);
            if (tar.BackGroundIndex < -1) tar.BackGroundIndex = -1;
            for (int i = 0; i < tar.backgrounds.Count; i++)
            {
                if (tar.backgrounds[i] != null) tar.backgrounds[i].SetActive(tar.BackGroundIndex == i);
            }

        }
    }

}
