﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

   public class PreViewPointNode: PreViewNodeMono
    {
        public override string KindName => "Point";
        public GameObject UseFulPoint;
        public override void Fresh()
        {
            //print("point haveValue   " + Master.Point.HasValue);
            var p = Master.PreV3(nn.Point);
            if (p.NoValue) { gameObject.SetActive(false); return; }
            gameObject.SetActive(true);
            var useful = Master.PreBool(nn.Useful);
            UseFulPoint.SetActive(useful.HasValue?useful.Value:false);
            transform.position = p.Value;
        }
       
    }
