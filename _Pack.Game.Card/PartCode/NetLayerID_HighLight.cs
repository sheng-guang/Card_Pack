using System;
using UnityEngine;


    //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID      //layerID  
    public abstract partial class LayerID //highlight
    {
       
        //hightlight
        public void FreshAllUsefulForm()
        {
            int UsefulCount = 0;
            for (int i = 0; i < InputSkills.Count; i++)
            {
                if (InputSkills[i] == null) continue;
                if (InputSkills[i].HighLightTest() == false) continue;

                HighLightSet<IHighLightInput>.TurnOnHightLight(InputSkills[i].IsHighLightInput);

                UsefulCount++;
            }
            if (UsefulCount != 0) HighLightSet<IHighLightInput>.TurnOnHightLight(IsHighLightInput);
        }
       public HighLightStruct IsHighLightInput { get; private set; }=new HighLightStruct();
        public HighLightStruct IsHighLightTarget { get; private set; } = new HighLightStruct();

    }


    public class HighLightStruct : IHighLightInput
    {
        public bool Value { get; set; }=false;
        public void SetHighLight(bool b)
        {
            Value = b;
        }
    }

