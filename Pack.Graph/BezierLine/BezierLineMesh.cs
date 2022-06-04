using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pack
{
    public class BezierLineMesh : MonoBehaviour
    {
        public void SetLineActive(bool b)
        {
            line.gameObject.SetActive(b);
        }
        [Header("LineRenderer")]
        public LineRenderer line;
        public int LineRendererCount =18;
        Vector3[] posses;
        public float wideStart = 0.1f;
        public float wideEnd = 0.08f;
        private void Awake()
        {
            if (LineRendererCount < 2) LineRendererCount = 2;
            posses = new Vector3[LineRendererCount];
            line.positionCount = LineRendererCount;
            bezierLine = new BezierLine(BezierLineCount);
        }
        [Header("BezierLine")]
        public int BezierLineCount=4;
        BezierLine bezierLine;

        public void SetPoint(Vector3 tar,int index)
        {
            bezierLine.SetPoint(tar, index);
        }
        public void FreshLine()
        {

            for (int i = 0; i < posses.Length; i++)
            {
                posses[i] = bezierLine.GetPoint(1f * i / LineRendererCount);
            }
            posses[posses.Length - 1] = bezierLine.GetPoint(1);
            line.SetPositions(posses);
            line.startWidth = CamSpace.GetWidthAtPoint(posses[0], wideStart);
            line.endWidth = CamSpace.GetWidthAtPoint(posses[1], wideEnd);
        }
    }

}
