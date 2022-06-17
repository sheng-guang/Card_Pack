using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BezierLineMeshPoints : BezierLineMesh
    {
        public Transform startPoint;
        public Transform Tail1;
        public Transform Tail2;
        //public BezierLineMesh line=>this;
        public void Fresh(bool acctive,Vector3 point)
        {
            SetLineActive(acctive);
            if (acctive == false) return;

            SetPoint(startPoint.position, 0);
            SetPoint(Tail1.position, 1);
            SetPoint(Tail2.position, 2);
            SetPoint(point, 3);

            FreshLine();
        }
    }

