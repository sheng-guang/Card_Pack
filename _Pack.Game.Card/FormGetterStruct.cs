using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public struct FormGetterStruct : IFormGetter
    {
        public void SetFormGetter(LayerID l, int k)
        {
            layer = l;
            kind = k;
        }
        public LayerID layer;
        public int kind;

        public bool Useful => layer.GetInputSkill(kind).HighLightTest();

        public InputForm GetCopyForm()
        {
            var to = layer.GetInputSkill(kind);//.GetEmptyForm();
            Debug.Log(to);
            return to.GetEmptyForm();
        }
    }
}
