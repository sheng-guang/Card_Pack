using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

    public class PreViewLineNode : PreViewNodeMono
    {
        public Transform body;
        

        public override string KindName => "Line";
        public float Wide = 0.05f;
        public override void Fresh()
        {
            var now = Master.PreV3(nn.Point);
            var up = Master.UPViewNode.PreV3(nn.Point);
            if (now .NoValue || up.NoValue) { gameObject.SetActive(false); return; }
            gameObject.SetActive(true);
            transform.position = now.Value;
            transform.LookAt(up.Value,Vector3.up);
            body.localScale = new Vector3(Wide, Wide, (now.Value - up.Value).magnitude);
        }
    }

