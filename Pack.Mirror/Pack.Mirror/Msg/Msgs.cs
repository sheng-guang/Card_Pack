﻿using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public struct PassMsg : NetworkMessage
    {
        public string pass;
    }
    public struct GamePlayerIDMsg : NetworkMessage
    {
        public int ID;
    }

    //public partial struct SyncDataMsg : NetworkMessage
    //{
    //}
