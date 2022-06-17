using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public interface IOnAsyncLoaded
    {
        void OnLoaded();
    }
    //public abstract  class AsyncMonoBehavior : MonoBehaviour
    //{
    //    public virtual void Awake()
    //    {
    //      var tool =GetComponent<AsyncResTool>();
    //        tool.ListenOnCreat(OnCreat);
    //    }
    //    public abstract void OnCreat();
    //}
