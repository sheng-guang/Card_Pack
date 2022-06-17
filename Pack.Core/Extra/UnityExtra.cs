using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Unity 扩展方法
    public static class UnityExtra
    {
        public static Vector3 ShowInNewCam(this Vector3 point, Camera OldCam, Camera newCam)
        {
            var to=newCam.transform.InverseTransformPoint(point);
            float MainFovR = OldCam.fieldOfView / 180 * Mathf.PI;
            float NewFovR = newCam.fieldOfView / 180 * Mathf.PI;
            float New_Main= Mathf.Tan(NewFovR / 2)/Mathf.Tan(MainFovR/2);
            to.x *= New_Main;
            to.y *= New_Main;
            var NewPoint=newCam.transform.TransformPoint(to);
            return NewPoint;
        }

        public static string GetChildrenPath(this Transform parent, Component child)
        {
            string re = "";
            Transform to = child.transform;
            while (to != parent)
            {
                if (to == null) return null;
                re = "/" + to.name + re;
                to = to.parent;
            }
            re = re.Remove(0, 1);
            return re;
        }

        public static void MoveTransfTo(this Component c, Component par, bool PossToZero = true, bool RoToZero = true, bool ScaleToOne = true)
        {
            if (c == null){ Debug.LogWarning("move null"); return; }
            //Debug.Log(c.name+"------------------>"+par.name);
            Transform t = c.transform;
            if (par == null) t.SetParent(null);
            else t.SetParent(par.transform);
            if (ScaleToOne) t.localScale = Vector3.one;
            if (RoToZero) t.localRotation = Quaternion.identity;

            if (PossToZero) { t.localPosition = Vector3.zero; }

        }
        public static void EnsureComponet<T>(this Component c)where T :Component
        {
            if (c.GetComponent<T>() == null) c.gameObject.AddComponent<T>();
        }
        public static Vector3 SetY(this Vector3 v, float y)
        {
            v.y = 0;
            return v;
        }

        public static Vector3 GetRight_normal(this Vector3 foward)
        {
            foward.y = 0;
            Vector3 x0z = foward.normalized;
            if (x0z.magnitude == 0) return Vector3.zero;
            return new Vector3(x0z.z, 0, -x0z.x);
        }
        public static T getFirstComponetInParent<T>(this Component c)
        {
            var to = c.transform;
            while (to)
            {
               var re= to.GetComponent<T>();
                if (re.IsNull_or_EqualNull() == false) return re;
                to = to.parent;
            }
            return default;
        }
    }

