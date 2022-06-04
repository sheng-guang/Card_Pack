using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack
{
    public interface ILayerLinkUp
    {
        public void Listen(ILayerLinkFollower Linstener);
    }
    public interface ILayerLinkFollower
    {
        public void SetUpLayer(LayerID layer);
    }
}
