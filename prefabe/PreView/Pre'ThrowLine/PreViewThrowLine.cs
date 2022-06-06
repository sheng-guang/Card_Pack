using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public class PreViewThrowLine : PreViewNodeMono
    {
        public override string KindName => "ThrowLine";

        public override void Awake()
        {
            base.Awake();

        }
        public GameObject pre;
        public List<Transform> Created;
        public Transform GetNew()
        {
            return Instantiate(pre, transform).transform;
        }
        public override void Fresh()
        {
            var speed = Master.GetInt(NN.speed);
            var highThrow = Master.GetBool(NN.HighThrow);
            var now = Master.GetV3(NN.Point);
            var up = Master.UPViewNode.GetV3(NN.Point);


            //Debug.Log(now.HasValue + "|" + up.HasValue + "|" + speed.HasValue + "|" + highThrow.HasValue);
            if (now.NoValue || up.NoValue || speed.NoValue || highThrow.NoValue) { gameObject.SetActive(false); return; }
            var start = up.Value;
            {
                var offsety = Master.GetFloat(NN.OffSetY);
                var offsetXZ = Master.GetFloat(NN.OffSetXZ);
                if (offsety.HasValue) 
                {
                    start += offsety.Value * Vector3.up;
                }
                Vector3 dir =( now.Value - up.Value);
                dir.y = 0;
                if (offsetXZ.HasValue) start+= dir.normalized*offsetXZ.Value ;
            }
            Vector3 v = PhyExtra.GetThrowVelocity(now.Value, start, highThrow.Value, speed.Value);
            if (float.IsNaN(v.x))
            {
                gameObject.SetActive(false);

            }
            else
            {
                gameObject.SetActive(true);
                gameObject.transform.position = start;
                Vector3 dir = now.Value - start;
                float TarXZLength = new Vector2(dir.x, dir.z).magnitude;
                Vector2 LastXZ = Vector2.zero;
                int NextIndex = 0;
                Vector3 LastPoss = Vector3.zero;

                
                while ((NextIndex > 1 ? (TestTouch(LastPoss) == false) : true) && NextIndex < 200)
                {
                    var to = Created.EnsureElement(NextIndex, GetNew);
                    to.gameObject.SetActive(true);
                    float t = NextIndex * 0.03f;
                    Vector3 poss = v * t;
                    poss += Physics.gravity * t * t;
                    //todo ΪʲôҪ���Զ�
                    poss *= 2;
                    to.localPosition = poss;
                    LastPoss = to.position;
                    NextIndex++;
                    LastXZ = new Vector2(poss.x, poss.y);

                }

                for (int i = NextIndex; i < Created.Count; i++)
                {
                    Created[i].gameObject.SetActive(false);
                }
            }
        }
        public bool TestTouch(Vector3 LastPoss)
        {
            bool ToBreak = false;
            eve.OverLapCollider(LastPoss, 0.1f,
    (c) =>
    {
        if (c.isTrigger == false) ToBreak = true;
    });

            return ToBreak;
        }
    }


}