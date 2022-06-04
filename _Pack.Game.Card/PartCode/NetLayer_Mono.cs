using UnityEngine;

namespace Pack
{
    //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer      //layer  
    public abstract partial class Layer:MonoBehaviour //mono/net
    {
        public virtual Unit TopUnit => null;
        public virtual Hero hero => up.hero;
    }
}
