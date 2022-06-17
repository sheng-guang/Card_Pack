using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Pack
{
    public class PreViewReachSpaceNode:PreViewNodeMono
    {
        public override string KindName => "Reach";
        public override void Fresh()
        {
            var point = Master.GetV3(nn.Point);
            var reach = Master.GetFloat(nn.Reach);
            if (point.NoValue || reach.NoValue) { gameObject.SetActive(false); return; }
            gameObject.SetActive(true);
            transform.position = point.Value;
            transform.localScale = Vector3.one * reach.Value;
        }
    }
}
