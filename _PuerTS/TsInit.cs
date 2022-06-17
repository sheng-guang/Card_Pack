using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsInit : MonoBehaviour
{
   public void AddStartInitAction(Action a)
    {
        startAct += a;
    }
    Action startAct;
    void Start()
    {
        startAct?.Invoke();
    }

}
