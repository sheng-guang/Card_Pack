using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface ITarget
    {
        IInputData GetData();
    }
    //Ä¿±ê
    public interface IInputData
    {
        N<Vector3> Point { get; }
        N<int> LayerId { get; }
    }
