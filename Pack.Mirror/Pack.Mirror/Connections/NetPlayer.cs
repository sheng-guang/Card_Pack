﻿#if MIRROR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
namespace Pack
{
    public class NetPlayer
    {
        public NetworkConnection conn;
        public string pass = null;
        public int NetPlayerID = -1;
        public int? GamePlayerID = null;
        
    }
    public static class NetPlayerExtra
    {

    }
}
#endif