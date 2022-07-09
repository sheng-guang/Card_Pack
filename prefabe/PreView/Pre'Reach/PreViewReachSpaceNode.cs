using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public class PreViewReachSpaceNode:PreViewNodeMono
    {
        public override string KindName => "Reach";
        public override void Fresh()
        {
            var point = Master.PreV3(nn.Point);
            var reach = Master.PreFloat(nn.Reach);
            if (point.NoValue || reach.NoValue) { gameObject.SetActive(false); return; }
            gameObject.SetActive(true);
            transform.position = point.Value;
            transform.localScale = Vector3.one * reach.Value;
        }
    }
