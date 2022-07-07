using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public interface IBeforeAsPrefabe
    {
        void BeforeAsPrefabe1(IRes res);
        void BeforeAsPrefabe2(IRes res);
    }
    public interface IAfterAsPrefabe
    {
        void AfterAsPrefabe1(IRes res);
        void AfterAsPrefabe2(IRes res);
    }
    public class ResTool : MonoBehaviour, IBeforeAsPrefabe,IAfterAsPrefabe
    {
        //1
        public void BeforeAsPrefabe1(IRes res)
        {
            if (GetComponent<IRes>() == res) return;
            this.Ex<Transform>("par").SetIGet(transform.parent);
        }
        //2
        public void BeforeAsPrefabe2(IRes res)
        {
            if (GetComponent<IRes>() == res) return;
            transform.parent = null;
        }
        //3
        public void AfterAsPrefabe1(IRes res)
        {
            if (GetComponent<IRes>() == res) return;

        }
        //4
        public void AfterAsPrefabe2(IRes res)
        {
            if (GetComponent<IRes>() == res) return;
            var par = this.Ex<Transform>("par").Value;
            transform.parent = par;
        }
    }

