using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public static class TimeExtra
    {
        public static float GetPercentage(this float NowTime, float Start, float Len)
        {
            return (NowTime - Start) / Len;
        }
        public static float IfOver1(this float t,float Over)
        {
            return (t > 1 )?Over: t;
        }

    }

    public class TimeSetting : MonoBehaviour
    {
        public static TimeSetting s;
        private void Awake()
        {
            if (s != null && s != this) { Destroy(this); return; }
            s = this;
            Physics.autoSimulation = false;
        }
        float getNearNum(float n)
        {
            float re = 1;
            if (n >= 1) re = 1;
            if (n >= 2) re = 2;
            if (n >= 2.5f) re = 2.5f;
            if (n >= 5) re = 5;
            return re;
        }
        void OnValidate()
        {
            fixedDeltaTime = getNearNum(fixTime) * 0.01f;
            Time.fixedDeltaTime = fixedDeltaTime;
            fixCount_in_50ms = (int)(0.05f / fixedDeltaTime);
            FixCount_in_1000ms = (int)(1f / fixedDeltaTime);
            _5msCount_in_1_fix = Convert.ToInt32(fixedDeltaTime / 0.005f);
        }

        public float fixTime = 0.01f;
        public float fixedDeltaTime = 0.01f;
       
        public int _5msCount_in_1_fix;
        public int fixCount_in_50ms;
        public float FixCount_in_1000ms;


        public static int n_5ms_in_1_fix => s._5msCount_in_1_fix;
        public static int n_fix_in_50ms => s.fixCount_in_50ms;
        public static float FixedDeltaTime => s.fixedDeltaTime;
        public static float AnimNormalStopTime = 0.8f;
        public static bool Test_and_UpdateTime(ref int FixedCount)
        {
            FixedCount++;
            if (FixedCount >= n_fix_in_50ms)
            {
                FixedCount = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}