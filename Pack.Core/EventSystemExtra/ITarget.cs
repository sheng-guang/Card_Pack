using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{
    public interface ITarget
    {
        IInputData GetData();
    }
    //Ŀ��
    public interface IInputData
    {
        N<Vector3> Point { get; }
        N<int> LayerId { get; }
    }
}