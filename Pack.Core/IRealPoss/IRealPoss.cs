﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Pack
{
    public interface IRealPoss
    {
        Vector3 RealPoss { get; }
        Vector3 VisualPoss { get; }
    }

}
