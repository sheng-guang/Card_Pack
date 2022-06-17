using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //[DefaultExecutionOrder(-1000)]
    public class UpDateVersion : MonoBehaviour
    {
        private void Awake()
        {
        }
        public static int NowUpDateVersion = int.MinValue;
        public static int LateUpDateVersion = int.MinValue;
        public static int FixedUpDateVersion = int.MinValue;
    
        void Update()
        {
            NowUpDateVersion++;
            //print("---------------------------");
            //LateUpDateVersion++;
        }
        public void LateUpdate()
        {
            LateUpDateVersion++;
            //print("^^^^^^^^^^^^^^^^^^^^");
            //NowUpDateVersion++;
        }
        


    }
