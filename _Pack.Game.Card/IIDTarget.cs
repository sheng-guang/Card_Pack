using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIDTarget 
{
    int NetID { get; }
    Vector3 RealPoss { get; }
    Vector3 VisualPoss { get;}
}
