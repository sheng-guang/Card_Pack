using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public static class PhyExtra 
    {
        public static Vector3 GetThrowVelocity(Vector3 target, Vector3 start, bool highThrow, float speed)
        {

            float angle = GetThrowAngle(target, highThrow, start, speed);
            float deltax = target.x - start.x;
            float deltaz = target.z - start.z;

            float dis = Mathf.Sqrt(deltax * deltax + deltaz * deltaz);

            Vector3 re = Vector3.zero;
            //Debug.Log(" ange   "+angle);
            re.y = speed * Mathf.Sin(angle);
            var xz = speed * Mathf.Cos(angle);
            //Debug.Log("[delta x  " + deltax + "       "+ (deltax / dis) + "][     dz   " + deltaz +"         "+ (deltaz / dis) + " ]                     dis    " + dis);
            //Debug.Log("  " + xz);
            re.x = (deltax / dis) * xz;
            re.z = (deltaz / dis) * xz;
            //Debug.Log(re);
            return re;
        }
        //result 0-1
        public static float GetThrowAngle(Vector3 target, bool highThrow, Vector3 start, float speed)
        {
            float angleX;
            float distX = Vector2.Distance(new Vector2(target.x, target.z), new Vector2(start.x, start.z));
            float distY = target.y - start.y;
            float posBase = (Physics.gravity.y * Mathf.Pow(distX, 2.0f)) / (2.0f * Mathf.Pow(speed, 2.0f));
            float posX = distX / posBase;
            float posY = (Mathf.Pow(posX, 2.0f) / 4.0f) - ((posBase - distY) / posBase);
            if (posY >= 0.0f)
            {
                if (highThrow) 
                    angleX = /*Mathf.Rad2Deg * */Mathf.Atan(-posX / 2.0f + Mathf.Pow(posY, 0.5f));
                else
                    angleX = /*Mathf.Rad2Deg **/ Mathf.Atan(-posX / 2.0f - Mathf.Pow(posY, 0.5f));
            }
            else
            {
                angleX = Mathf.Deg2Rad * 45.0f;
            }
            //todo 低空抛射时 可能会超过45度 为什么
            if (highThrow == false && angleX / pi > 0.25f) angleX = 0.25f * pi;
            return angleX;
        }


        const float pi = 3.1415926535f;

        public static float ToAngle(this Vector3 foward)
        {
           var Piangle= Mathf.Atan2(foward.x, foward.z);
            return Piangle / pi * 180;
        }
        public static float GetCloestAngle(this float start,float tar)
        {
            var re = tar;
            while (re-start>180)
            {
                re -= 360;
            }
            while (start - re > 180)
            {
                re += 360;
            }
            return re;
        }
        static float acc1=10;
        static float acc2 = 3;
        public static Vector3 GetNextV(this Vector3 NowV,Vector3 TarV,float acc,float deltaT,MovementData data)
        {
            float accTime = 1;
            if (TarV.NearZero())            {                accTime = acc1;            }
            else
            {
                bool sameDir = SameDir(NowV, TarV,out _);
                accTime = sameDir ? acc1 : acc2;
                //accTime = acc1;
                //Debug.Log(sameDir);
            }

            //delta
            var disV =TarV - NowV;
            float step =accTime* acc * deltaT;
            float dis = disV.magnitude;
            if (step > dis) step = dis;

            var re = NowV += disV.normalized * step;
            return re;
        }
        public static bool SameDir(Vector3 TarV,Vector3 NowV,out float ToRo)
        {
            float NowAngle = NowV.ToAngle();
            float TarAngle = NowAngle.GetCloestAngle(TarV.ToAngle());
             ToRo =TarAngle - NowAngle;
            return Mathf.Abs (ToRo) < 160;
        }
        public static float? GetAnimFoward(Vector3 TarV,Vector3 NowV,MovementData data)
        {
            data.HardTurnPercentage = 1.1f;
            //如果目标方向为0 则保持原来方向
            if (TarV.NearZero())
            {
                return null;
            }

            bool sameDir = SameDir(TarV, NowV, out var toRo);


            //同方向 或者 几乎相同
            if (sameDir)
            {
                data.MaxL = NowV.magnitude;
                //Debug.Log(11);

                //if (NowV.magnitude < (TarV.magnitude *0.8f)) return null;
                return NowV.ToAngle();
            }

            else
            {
                //Debug.Log(33);
                //急转弯
                var ProjLen = Vector3.Project(NowV, TarV.normalized).magnitude;
                float AngleDis = ProjLen / data.MaxL;
                if (AngleDis > 1) AngleDis = 1;
                float Percentage = 1 - ProjLen / data.MaxL;
                //达到高速移动则触发转身
                if(data.MaxL>=TarV.magnitude*1.5f) data.HardTurnPercentage = Percentage;


                if (toRo <= -175) toRo = 180;
                float TurnDirection = toRo >= 0 ? 1 : -1;
                data.MirrorTurn = toRo < 0;
                float re = TarV.ToAngle() - AngleDis * 180*TurnDirection;

                return re;
            }
        
        }

        public static bool NearZero(this Vector3 v)
        {
            return v.magnitude < 0.00001f;
        }
        public static bool NearZero(this float f)
        {
            return f < 0.00001f;
        }
        public static void EnsureNotZero(ref float f)
        {
            if (f < 0.00001f) f = 0.00001f;
        }
    }

    public class MovementData
    {
        public bool lastTurning = false;
        public float TurnStartSpeed = 0;
        public float LastAngle;
        public Vector3 LastV;

        public float HardTurnPercentage = 1.1f;
        public float MaxL;
        public bool MirrorTurn=false;
    }

}
