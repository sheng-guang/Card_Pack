using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class BezierLine
    {
        public BezierLine(int count=2)
        {
            ReSetPointCount(count);
        }
        public int Count { get; private set; }
        public void ReSetPointCount(int count)
        {
            if (count < 2) count = 2;
            this.Count = count;
            for (int i = count; i>=1; i--)
            {
                var ne = new Vector3[i];
                levels.Add(ne);
            }
        }
        Vector3[] points => levels[0];
        List<Vector3[]> levels=new List<Vector3[]>();
        public void SetPoint(Vector3 v,int index)
        {
            if (index < 0) return;
            if(index < points.Length==false) return;
            if(points[index]==v) return;
            points[index] = v;
            //Debug.Log("set  " + index + "    " + v);
        }

        public Vector3 GetPoint(float d)
        {
            Vector3[] results = new Vector3[Count];
            for (int i = 0; i < Count; i++)
            {
                results[i] = points[i];
            }
            //Debug.Log(d+"-------------------------------");
            for (int i = Count-1; i >0; i--)
            {
                //Debug.Log(d + "------");
                for (int j = 0; j < i; j++)
                {

                    results[j] = Vector3.Lerp(results[j], results[j + 1], d);
                //Debug.Log(j+"   "+(j+1)+"   =>    "+j+"  "+results[j]);
                }
            }
            return results[0];
        }

    }
    //public class BezierLine2
    //{
    //    public BezierLine2(int count)
    //    {
    //        ReSetPointCount(count);
    //    }
    //    public int Count;
    //    public void ReSetPointCount(int count)
    //    {
    //        if (count < 2) count = 2;
    //        this.Count = count;
    //        for (int i = count; i >= 1; i--)
    //        {
    //            var ne = new Vector3[i];
    //            levels.Add(ne);
    //        }
    //    }
    //    Vector3[] points => levels[0];
    //    List<Vector3[]> levels = new List<Vector3[]>();
    //    public void SetPoint(Vector3 v, int index)
    //    {
    //        if (index < 0) return;
    //        if (index < points.Length == false) return;
    //        if (points[index] == v) return;
    //        points[index] = v;
    //        //Debug.Log("set  " + index + "    " + v);
    //    }

    //    public Vector3 GetPoint(float d)
    //    {
    //        Vector3[] results = new Vector3[Count];
    //        for (int i = 0; i < Count; i++)
    //        {
    //            results[i] = points[i];
    //        }
    //        //Debug.Log(d+"-------------------------------");
    //        for (int i = Count - 1; i > 0; i--)
    //        {
    //            //Debug.Log(d + "------");
    //            for (int j = 0; j < i; j++)
    //            {

    //                results[j] = Vector3.Lerp(results[j], results[j + 1], d);
    //                //Debug.Log(j+"   "+(j+1)+"   =>    "+j+"  "+results[j]);
    //            }
    //        }
    //        return results[0];
    //    }

    //}
