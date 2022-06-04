using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Pack
{
   public class PreViewPointNode: PreViewNodeMono
    {
        public override string KindName => "Point";
        public GameObject UseFulPoint;
        public override void Fresh()
        {
            //print("point haveValue   " + Master.Point.HasValue);
            var p = Master.GetV3(NN.Point);
            if (p.NoValue) { gameObject.SetActive(false); return; }
            gameObject.SetActive(true);
            var useful = Master.GetBool(NN.Useful);
            UseFulPoint.SetActive(useful.HasValue?useful.Value:false);
            transform.position = p.Value;
        }
       
    }
}
