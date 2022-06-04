using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pack 
{
    //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID  
    public abstract partial class LayerID //awake
    {
        public override Transform transf => transform;
        public  virtual void Awake() 
        {
            eve.AwakeLoad(this); 
        }
    }
}
