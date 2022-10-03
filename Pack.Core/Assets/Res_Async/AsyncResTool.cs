using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif


    public class AsyncResTool : MonoBehaviour,IBeforeAsPrefabe,IAfterAsPrefabe
    {
        
//#if UNITY_EDITOR
//        public Transform child { get; set; }
//#endif
        public string AsyncAsetName;
        public string ABAsyncDir;
        public string GetKey => ABAsyncDir + "|" + AsyncAsetName;

         //IOnAsyncLoaded[] Listener;
        private void Awake()
        {
            var Listeners = GetComponentsInChildren<IOnAsyncLoaded>();
            foreach (var item in Listeners) { ListenOnCreat(item.OnLoaded); }
#if UNITY_EDITOR
            PrefabList.TryGetValue(GetKey, out var pre);
            if (pre)
            {
                OnLoaded(pre.gameObject);
                return;
            }
#endif
            ASyncCreater<GameObject>.Load(ABAsyncDir, AsyncAsetName, OnLoaded);


        }
        public void OnLoaded(GameObject g)
        {
           var c= Instantiate(g, transform);
           c.name= c.name.Remove(c.name.LastIndexOf('('));
            OnLoadCall?.Invoke();
        }
        Action OnLoadCall;
        public void ListenOnCreat(Action a)
        {
            OnLoadCall += a;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        public void BeforeAsPrefabe1(IRes res)
        {
           var first = this.getFirstComponetInParent<IRes>();
            this.Bool("do",first==res);
            if (this.Bool("do").Value == false) return;

            var c = transform.GetChild(transform.childCount - 1);
            this.Ex<Transform>("child").SetIGet(c);
            AsyncAsetName = c.name;
            ABAsyncDir = res.ABAsyncPath();
        }
        public void BeforeAsPrefabe2(IRes res)
        {
            if (this.Bool("do").Value == false) return;
            var c = this.Ex<Transform>("child").Value;
            c.parent = null;
        }
        public void AfterAsPrefabe1(IRes res)
        {
            if (this.Bool("do").Value == false) return;
            var c = this.Ex<Transform>("child").Value;
            var p = this.PrefabeAsyncPath(res);
#if UNITY_EDITOR
            var saved = PrefabUtility.SaveAsPrefabAssetAndConnect(c.gameObject, p, InteractionMode.UserAction);
            saved.transform.position = Vector3.zero;
            PrefabList.Add(saved,GetKey);
#endif
        }

        public void AfterAsPrefabe2(IRes res)
        {
            if (this.Bool("do").Value == false) return;
            var c = this.Ex<Transform>("child").Value;
            c.parent = transform;
            c.SetAsLastSibling();
        }
    }

