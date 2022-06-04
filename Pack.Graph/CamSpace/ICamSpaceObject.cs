using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICamSpaceObject
{
    Transform transf { get; }
    float Depth();
    float PossX ();
    float PossY ();
}
