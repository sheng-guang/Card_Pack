using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Setting<T>
    {
        static object obj=new object();
        public static IGetSeat<T> GetSetting(string key)
        {
            return obj.Ex_Ptr<T>(key);
        }

    }

